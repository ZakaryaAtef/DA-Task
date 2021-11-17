using System;
using Android.Widget;
using DATask.Droid;
using DATask;
using Android.App;

[assembly: Xamarin.Forms.Dependency(typeof(MyToast))]
namespace DATask.Droid
{ 
    public class MyToast : IMyToast
    {
        public void MakeText(String Text, bool Short)
        {
            Toast.MakeText(Application.Context, Text, (Short) ? ToastLength.Short : ToastLength.Long).Show();
        }
    }
}