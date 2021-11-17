using CoreGraphics;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;
using DATask.iOS;
using DATask;
[assembly: Xamarin.Forms.Dependency(typeof(ImageResource))]
namespace DATask.iOS
{
    public class ImageResource : IImageResource
    {
        public Size GetSize(string fileName)
        {
            UIImage image = UIImage.FromFile(fileName);
            return new Size((double)image.Size.Width, (double)image.Size.Height);
        }
    }
}