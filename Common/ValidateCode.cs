using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
namespace Common
{
    class ValidateCode
    {
        public static ValidateCode Default = new ValidateCode();
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string Letters { get; set; }
        public int CodeLength { get; set; }
        private Random r = new Random();
        public ValidateCode()
        {
            ImageWidth = 200;
            ImageHeight = 60;
            CodeLength = 6;
            Letters = "abcdefghijhijklmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ1234567890";
        }
        public string GetValidationCode()
        {
            //合法随机显示字符列表
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            //将随机生成的字符串绘制到图片上
            for (int i = 0; i < CodeLength; i++)
            {
                s.Append(Letters.Substring(r.Next(0, Letters.Length - 1), 1));
            }
            return s.ToString();
        }
        public byte[] GetValidationImage(string codeString)
        {
            //设置输出流图片格式
            var b = new System.Drawing.Bitmap(ImageWidth, ImageHeight);
            var g = System.Drawing.Graphics.FromImage(b);
            int ColorR = r.Next(0, 255);
            int ColorG = r.Next(0, 255);
            int ColorB = r.Next(0, 255);

            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(ColorR, ColorG, ColorB)), 0, 0, 200, 60);
            var font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, 48, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            //合法随机显示字符列表

            //将随机生成的字符串绘制到图片上
            for (int i = 0; i < codeString.Length; i++)
            {
                int sR = r.Next(0, 255);
                int sG = r.Next(0, 255);
                int sB = r.Next(0, 255);
                while (Math.Abs(sR - ColorR) < 35) sR = r.Next(0, 255);
                while (Math.Abs(sG - ColorG) < 35) sG = r.Next(0, 255);
                while (Math.Abs(sB - ColorB) < 35) sB = r.Next(0, 255);
                g.DrawString(codeString[i].ToString(), font, new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(sR, sG, sB)), i * (200 / codeString.Length - 2), r.Next(0, 15));
            }

            //生成干扰线条
            var pen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))), 2);
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(pen, new System.Drawing.Point(r.Next(0, 199), r.Next(0, 59)), new System.Drawing.Point(r.Next(0, 199), r.Next(0, 59)));
            }

            var stream = new System.IO.MemoryStream();
            b.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            b.Dispose();
            //输出图片流
            return stream.ToArray();
        }
    }

    public class Code 
    {

        ValidateCode codes = new ValidateCode();

        /// <summary>
        /// 生成一个6位验证码(包含字符或数字)
        /// </summary>
        /// <returns>返回一个6位字符或数字字符串</returns>
        public string GetValidationCode()
        {
            return codes.GetValidationCode();
        }


        /// <summary>
        /// 生成byte[] 类型的验证码图片
        /// </summary>
        /// <param name="validateCode">验证码字符串</param>
        /// <returns>返回图片byte[]</returns>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeString"></param>
        /// <returns></returns>
        public byte[] GetValidationImage(string codeString) 
        {
            return codes.GetValidationImage(codeString);
        }
    }
}
