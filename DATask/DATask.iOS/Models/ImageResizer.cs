using CoreGraphics;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;
using DATask.iOS;
using DATask;
[assembly: Xamarin.Forms.Dependency(typeof(ImageResizer))]
namespace DATask.iOS
{
    public class ImageResizer : IImageResizer
    {
        public byte[] CropImage(byte[] photoToCropBytes, Rectangle rectangleToCrop, double outputWidth, double outputHeight)
        {
            byte[] photoOutputBytes;

            using (var data = NSData.FromArray(photoToCropBytes))
            {
                using (var photoToCropCGImage = UIImage.LoadFromData(data).CGImage)
                {
                    //crop image
                    using (var photoCroppedCGImage = photoToCropCGImage.WithImageInRect(new CGRect((nfloat)rectangleToCrop.X, (nfloat)rectangleToCrop.Y, (nfloat)rectangleToCrop.Width, (nfloat)rectangleToCrop.Height)))
                    {
                        using (var photoCroppedUIImage = UIImage.FromImage(photoCroppedCGImage))
                        {
                            //create a 24bit RGB image to the output size
                            using (var cGBitmapContext = new CGBitmapContext(IntPtr.Zero, (int)outputWidth, (int)outputHeight, 8, (int)(4 * outputWidth), CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
                            {
                                var photoOutputRectangleF = new CGRect(0f, 0f, (float)outputWidth, (float)outputHeight);

                                // draw the cropped photo resized 
                                cGBitmapContext.DrawImage(photoOutputRectangleF, photoCroppedUIImage.CGImage);

                                //get cropped resized photo
                                var photoOutputUIImage = UIKit.UIImage.FromImage(cGBitmapContext.ToImage());

                                //convert cropped resized photo to bytes and then stream
                                using (var photoOutputNsData = photoOutputUIImage.AsJPEG())
                                {
                                    photoOutputBytes = new Byte[photoOutputNsData.Length];
                                    System.Runtime.InteropServices.Marshal.Copy(photoOutputNsData.Bytes, photoOutputBytes, 0, Convert.ToInt32(photoOutputNsData.Length));
                                }
                            }
                        }
                    }
                }
            }
            return ResizeImage(photoOutputBytes, 250, 250);
        }
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {

            UIImage originalImage = new UIImage(new NSData(Convert.ToBase64String(imageData), NSDataBase64DecodingOptions.None));
            UIImageOrientation orientation = originalImage.Orientation;
            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 (int)(4 * width), CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                CGRect imageRect = new CGRect(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                var arr = resizedImage.AsJPEG(0.3f).ToArray();
                return arr;
            }
        }
    }
}