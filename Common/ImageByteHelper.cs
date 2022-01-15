using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ImageByteHelper
    {
        /// <summary>
        /// 图片转化为字节流
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(Image img)
        {
            ImageConverter imgconv = new ImageConverter();
            byte[] b = (byte[])imgconv.ConvertTo(img, typeof(byte[]));
            return b;
        }

        /// <summary>
        /// 字节流转化为图片
        /// </summary>
        /// <param name="byt"></param>
        /// <returns></returns>
        public static Image BytToImg(byte[] byt)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byt);
                Image img = Image.FromStream(ms);
                return img;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

    }
}
