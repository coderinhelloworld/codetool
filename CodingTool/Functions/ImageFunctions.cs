using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Functions
{
    public static class ImageFunctions
    {

        public static void ImageToIco(string filePath)
        {

            string inputImagePath = filePath;
            //获取文件名@
            var imgFileName = filePath.Substring(filePath.LastIndexOf("\\"), filePath.Length - filePath.LastIndexOf("\\"));
            var fileName = imgFileName.Substring(0, imgFileName.LastIndexOf(".")) + ".ico";
            string outputIconPath = Directory.GetCurrentDirectory() + @"\ImageOutPut\" + fileName;
            var outPutDic = Directory.GetCurrentDirectory() + @"\ImageOutPut";
            if (!Directory.Exists(outPutDic))
            {
                Directory.CreateDirectory(outPutDic);
            }
            int iconWidth = 128;  // Custom icon width
            int iconHeight = 128; // Custom icon height

            Size size = new Size();
            size.Width = 128;
            size.Height = 128;
            ConvertImageToIcon(inputImagePath, outputIconPath, size);

        }

        /// <summary>
        /// 图片转换为ico文件
        /// </summary>
        /// <param name="origin">原图片路径</param>
        /// <param name="destination">输出ico文件路径</param>
        /// <param name="iconSize">输出ico图标尺寸，不可大于255x255</param>
        /// <returns>是否转换成功</returns>
        public static bool ConvertImageToIcon(string origin, string destination, Size iconSize)
        {
            if (iconSize.Width > 255 || iconSize.Height > 255)
            {
                return false;
            }
            Image image = new Bitmap(new Bitmap(origin), iconSize); //先读取已有的图片为bitmap，并缩放至设定大小
            MemoryStream bitMapStream = new MemoryStream(); //存原图的内存流
            MemoryStream iconStream = new MemoryStream(); //存图标的内存流
            image.Save(bitMapStream, ImageFormat.Png); //将原图读取为png格式并存入原图内存流
            BinaryWriter iconWriter = new BinaryWriter(iconStream); //新建二进制写入器以写入目标图标内存流
            /**
             * 下面是根据原图信息，进行文件头写入
             */
            iconWriter.Write((short)0);
            iconWriter.Write((short)1);
            iconWriter.Write((short)1);
            iconWriter.Write((byte)image.Width);
            iconWriter.Write((byte)image.Height);
            iconWriter.Write((short)0);
            iconWriter.Write((short)0);
            iconWriter.Write((short)32);
            iconWriter.Write((int)bitMapStream.Length);
            iconWriter.Write(22);
            //写入图像体至目标图标内存流
            iconWriter.Write(bitMapStream.ToArray());
            //保存流，并将流指针定位至头部以Icon对象进行读取输出为文件
            iconWriter.Flush();
            iconWriter.Seek(0, SeekOrigin.Begin);
            Stream iconFileStream = new FileStream(destination, FileMode.Create);
            Icon icon = new Icon(iconStream);
            icon.Save(iconFileStream); //储存图像
            /**
             * 下面开始释放资源
             */
            iconFileStream.Close();
            iconWriter.Close();
            iconStream.Close();
            bitMapStream.Close();
            icon.Dispose();
            image.Dispose();
            return File.Exists(destination);
        }
    }
}
