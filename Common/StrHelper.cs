using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StrHelper
    {
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="n">截取的长度</param>
        /// <returns></returns>
        public static string GetStr(string str,int n) 
        {

            if (n >= str.Length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, n) + "......";
            }
            
        }

        /// <summary>
        /// 字符串被截取为数组的操作
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="chars">char类型的字符</param>
        /// <returns>返回一个数组</returns>
        public static string[] GetStrs(string str,char chars) 
        {
            string[] strs = str.Split(chars);
            return strs;
        }

        /// <summary>
        /// 字符串截取为字符串后把数组内容添加到一个list<string>中
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="chars">char类型字符</param>
        /// <returns>返回一个List<string> </returns>
        public static List<string> GetLists(string str,char chars) 
        {
            string[] strs = str.Split(chars);
            List<string> lists = new List<string>();
            foreach (var item in strs) 
            {
                lists.Add(item);
            }
            return lists;
        }
    }
}
