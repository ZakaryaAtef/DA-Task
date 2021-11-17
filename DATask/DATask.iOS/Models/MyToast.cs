using System;

using Foundation;
using UIKit;
using DATask.iOS;
using ToastIOS;
[assembly: Xamarin.Forms.Dependency(typeof(MyToast))]
namespace DATask.iOS
{
    public class MyToast : IMyToast
    {
        public void MakeText(String Text, bool Short)
        {
            Toast.MakeText(Text, (Short) ? Toast.LENGTH_SHORT : Toast.LENGTH_LONG).Show();
        }
    }
}