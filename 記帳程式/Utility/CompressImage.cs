using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encoder = System.Drawing.Imaging.Encoder;
using System.IO;
using 記帳程式.Models;

namespace 記帳程式.Services
{
    internal class CompressImage
    {
        public static Bitmap Compress(Image image, long quality, CompressCategory compressCategory)
        {
            MemoryStream memoryStream = new MemoryStream();
            // 获取图像编码器（JPEG在这里被使用）
            ImageCodecInfo jpegEncoder = GetEncoder(ImageFormat.Jpeg);

            // 创建Encoder参数（质量设置）
            Encoder encoder = Encoder.Quality;
            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;

            // 将图像保存到MemoryStream中，使用给定的编码器和参数
            image.Save(memoryStream, jpegEncoder, encoderParameters);


            if(compressCategory == CompressCategory.SizeCompress)
            {

                // 从MemoryStream中创建一个新的Bitmap并返回
                Bitmap bitmap = new Bitmap(memoryStream);
                

                return new Bitmap(bitmap, 50, 50);
            }
            return new Bitmap(memoryStream);



        }

        static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
