using Xamarin.Forms;
using Android.Graphics;
using DATask.Droid;
[assembly: Xamarin.Forms.Dependency(typeof(ImageResource))]
namespace DATask.Droid
{
    public class ImageResource : Java.Lang.Object, IImageResource
    {
        public Size GetSize(string fileName)
        {
            var options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };
            BitmapFactory.DecodeFile(fileName, options);

            return new Size((double)options.OutWidth, (double)options.OutHeight);
        }
    }
}