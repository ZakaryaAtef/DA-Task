using System;
using Xamarin.Forms;
namespace DATask
{
    public interface IImageResource
    {
        Size GetSize(string fileName);
    }
}
