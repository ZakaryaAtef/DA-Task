using System;
using Xamarin.Forms;
namespace DATask
{
    public interface IImageResizer
    {
         byte[] CropImage(byte[] photoToCropBytes, Rectangle rectangleToCrop, double outputWidth, double outputHeight);
         byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
