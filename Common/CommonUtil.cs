using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CommonUtil
    {
        /// <summary>
        /// 创建16个字符长度的GUID
        /// </summary>
        /// <returns></returns>
        public static string NewShortGuid()
        {
            long i = 1;
            byte[] guid = Guid.NewGuid().ToByteArray();
            foreach (byte b in guid)
            {
                i += ((int)b + 1);
            }
            string rlt = string.Format("{0:x}", i - DateTime.Now.Ticks);
            return rlt;
        }
    }
}
