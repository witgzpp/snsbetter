using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Common
{
    public static class ComHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly string registImgPath = ConfigurationManager.AppSettings["RegistImg"].ToString();
        /// <summary>
        /// 
        /// </summary>
        private static readonly string loginImgPath = ConfigurationManager.AppSettings["LoginImg"].ToString();


        private static readonly string flags = ConfigurationManager.AppSettings["flag"].ToString();
        /// <summary>
        /// 
        /// </summary>
        private static readonly string morningImgPath = ConfigurationManager.AppSettings["morning"].ToString();
        /// <summary>
        /// 
        /// </summary>
        private static readonly string noonImgPath = ConfigurationManager.AppSettings["noon"].ToString();
        /// <summary>
        /// 
        /// </summary>
        private static readonly string nightImgPath = ConfigurationManager.AppSettings["night"].ToString();


        private static readonly string address = ConfigurationManager.AppSettings["address"].ToString();


        /// <summary>
        /// 注册页面加载图片
        /// </summary>
        /// <returns>返回图片路径</returns>
        public static string GetRegistImgPath() 
        {
            return registImgPath;
        }

        /// <summary>
        /// 登录页面加载图片
        /// </summary>
        /// <returns>返回图片路径</returns>
        public static string GetLoginImgPath() 
        {
            return loginImgPath;
        }

        /// <summary>
        /// 用户早上登录主页显示图片
        /// </summary>
        /// <returns>返回相应图片路径</returns>
        public static string GetMorning() 
        {
            return morningImgPath;
        }
        
        /// <summary>
        /// 用户中午到下午登录主页显示图片
        /// </summary>
        /// <returns>返回相应图片路径</returns>
        public static string GetNoon() 
        {
            return noonImgPath;
        }

        /// <summary>
        /// 用户晚上登录主页显示图片
        /// </summary>
        /// <returns>返回相应图片路径</returns>
        public static string GetNight() 
        {
            return nightImgPath;
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns>返回早上、下午、晚上3种类型的字符串</returns>
        public static string GetDate()
        {
            string result = "";
            int hours = DateTime.Now.Hour;
            if (hours >= 6 && hours <= 12)
            {
                result = "早上";
            }
            else if (hours > 12 && hours <= 18)
            {
                result = "下午";
            }
            else
            {
                result = "晚上";
            }
            return result;


        }


        public static int PageSize() 
        {
            string pagesize = ConfigurationManager.AppSettings["PageSize"];
            return Convert.ToInt32(pagesize);
        }



        /// <summary>
        /// 检查长字符串中是否包含list集合中的一个词语
        /// </summary>
        /// <param name="lists">string集合</param>
        /// <param name="str">长字符串</param>
        /// <returns>true：包含    false：不包含</returns>
        public static bool ValidateStrs(List<string> lists,string str) 
        {
            bool flag = false;
            for (int i = 0; i < lists.Count; i++) 
            {
                if (str.Contains(lists[i])) 
                {
                    flag = true;
                }
            }
                return flag;

        }


        public static string GetAddress() 
        {
            return address;
        }

        /// <summary>
        /// 获取随机显示字符串
        /// </summary>
        /// <returns></returns>
        public static string GetFlag() 
        {
            Random ran=new Random();
            string[] flag = flags.ToString().Split(';');
            int n = ran.Next(flag.Length);
            return flag[n];
        }
       

    }
}
