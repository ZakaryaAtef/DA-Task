using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Graphics;
using DATask.Droid;
using DATask;
[assembly: Xamarin.Forms.Dependency(typeof(ImageResizer))]
namespace DATask.Droid
{
    class ImageResizer : IImageResizer
    { 
        public byte[] CropImage(byte[] photoToCropBytes, Rectangle rectangleToCrop, double outputWidth, double outputHeight)
        {
            using (var photoOutputStream = new MemoryStream())
            {
                // Load the bitmap
                var inSampleSize = CalculateInSampleSize((int)rectangleToCrop.Width, (int)rectangleToCrop.Height, (int)outputWidth, (int)outputHeight);
                var options = new BitmapFactory.Options();
                options.InSampleSize = inSampleSize;
                //options.InPurgeable = true;   see http://developer.android.com/reference/android/graphics/BitmapFactory.Options.html
                using (var photoToCropBitmap = BitmapFactory.DecodeByteArray(photoToCropBytes, 0, photoToCropBytes.Length, options))
                {
                    var matrix = new Matrix();
                    var martixScale = outputWidth / rectangleToCrop.Width * inSampleSize;
                    matrix.PostScale((float)martixScale, (float)martixScale);
                    using (var photoCroppedBitmap = Bitmap.CreateBitmap(photoToCropBitmap, (int)(rectangleToCrop.X / inSampleSize), (int)(rectangleToCrop.Y / inSampleSize), (int)(rectangleToCrop.Width / inSampleSize), (int)(rectangleToCrop.Height / inSampleSize), matrix, true))
                    {
                        photoCroppedBitmap.Compress(Bitmap.CompressFormat.Jpeg,50, photoOutputStream);
                    }
                }

                return photoOutputStream.ToArray();
            }
        }

        /// 
        /// Calculates the sample size value that is a power of two based on a target width and height
        /// 
        /// 
        /// 
        /// 
        /// 
        public static int CalculateInSampleSize(int inputWidth, int inputHeight, int outputWidth, int outputHeight)
        {
            //see http://developer.android.com/training/displaying-bitmaps/load-bitmap.html

            int inSampleSize = 1;       //default

            if (inputHeight > outputHeight || inputWidth > outputWidth)
            {

                int halfHeight = inputHeight / 2;
                int halfWidth = inputWidth / 2;

                // Calculate the largest inSampleSize value that is a power of 2 and keeps both
                // height and width larger than the requested height and width.
                while ((halfHeight / inSampleSize) > outputHeight && (halfWidth / inSampleSize) > outputWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return inSampleSize;
        }
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);
            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}