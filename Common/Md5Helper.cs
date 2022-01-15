using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common
{
    public static class Md5Helper
    {

        

        /// <summary>
        /// 16或32位MD5加密
        /// </summary>
        /// <param name="t">待加密字符串</param>
        /// <param name="bit">加密长度：16或32</param>
        /// <returns>返回MD5字符串16或32位或者为空字符串</returns>
        public static string GetMd5(string str,int bit) 
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(str)) 
            {
                return string.Empty.ToString();
            }

            using (MD5 md5 = MD5.Create()) 
            {
                byte[] bytes = Encoding.Default.GetBytes(str.ToString());

                byte[] md5Byte = md5.ComputeHash(bytes);
                for (int i = 0; i < md5Byte.Length; i++) 
                {
                    sb.Append(md5Byte[i].ToString("x2"));
                }

            }
            if (bit == 16) 
            {               
                return sb.ToString(8, 16);
            }
            else if (bit == 32)
            {
                return sb.ToString();
            }
            else
            {
                return string.Empty.ToString();
            }

                       
        }


        /// <summary>
        /// 用gb2312方式32对字符串进行MD5加密
        /// </summary>
        /// <param name="pwd">待加密字符</param>
        /// <returns>返回Md5加密字符</returns>
        public static string GetMd5(string str) 
        {
            StringBuilder temp = new StringBuilder();
            using (MD5 md5 = MD5.Create()) 
            {
                byte[] bytes=Encoding.GetEncoding("gb2312").GetBytes(str);

                byte[] md5Byte = md5.ComputeHash(bytes);

                foreach (byte item in md5Byte) 
                {
                    temp.Append(item.ToString("x2"));
                }
                return temp.ToString();
            }
        }

        /// <summary>
        /// 用base64加密方式加密
        /// </summary>
        /// <param name="str">被加密字符串</param>
        /// <returns>返回被加密内容</returns>
        public static string Md64(string str) 
        {
            using (MD5 md5 = MD5.Create()) 
            {
                return Convert.ToBase64String(md5.ComputeHash(Encoding.Default.GetBytes(str)));
            }
        }



        

        


        
        
    }
}
