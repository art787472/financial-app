using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Models;
using 記帳程式.Services;

namespace 記帳程式.MVP
{
    internal class AddFormPresenter : IAddFormPresenter
    {
        private IRepository repository;
        private IAddFormView view;

        public void AddRecord(ViewItem item)
        {
            string filePath = ConfigurationManager.AppSettings["filePath"];
            string directoryName = item.dateTime.ToString("yyyy-MM-dd");

            string fileName1 = Guid.NewGuid().ToString();
            string fileName2 = Guid.NewGuid().ToString();

            string imagePath1 = Path.Combine(filePath, $@"{directoryName}\{fileName1}.jpeg");
            string imagePath2 = Path.Combine(filePath, $@"{directoryName}\{fileName2}.jpeg");

            string smallImgPath1 = Path.Combine(filePath, $@"{directoryName}\{fileName1}-small.jpeg");
            string smallImgPath2 = Path.Combine(filePath, $@"{directoryName}\{fileName2}-small.jpeg");
            

            Item newItem = new Item();
            newItem.dateTime = item.dateTime.ToString("yyyy-MM-dd HH:mm");
            newItem.price = item.price;
            newItem.category = item.category;
            newItem.reason = item.reason;
            newItem.account = item.account;
            newItem.picPath1 = imagePath1;
            newItem.picPath2 = imagePath2;
            newItem.smallPicPath1 = smallImgPath1;
            newItem.smallPicPath2 = smallImgPath2;

            repository.AddRecord(newItem);

            Image image1 = item.image1;
            Image image2 = item.image2;

            Bitmap bitmap1 = CompressImage.Compress(image1, 25, CompressCategory.NormalCompress);
            bitmap1.Save(imagePath1, ImageFormat.Jpeg);
            bitmap1.Dispose();
            Bitmap bitmap2 = CompressImage.Compress(image2, 25, CompressCategory.NormalCompress);
            bitmap2.Save(imagePath2, ImageFormat.Jpeg);
            bitmap2.Dispose();

            Bitmap smallBitmap1 = CompressImage.Compress(image1, 1, CompressCategory.SizeCompress);
            smallBitmap1.Save(smallImgPath1, ImageFormat.Jpeg);
            smallBitmap1.Dispose();

            Bitmap smallBitmap2 = CompressImage.Compress(image2, 1, CompressCategory.SizeCompress);
            smallBitmap2.Save(smallImgPath2, ImageFormat.Jpeg);
            smallBitmap2.Dispose();

            view.AddRecordFinish(true);
        }

        public void SetView(IAddFormView view)
        {
            this.view = view;
            this.repository = DIContainer.GetInstance<IRepository>();
        }
    }
}
