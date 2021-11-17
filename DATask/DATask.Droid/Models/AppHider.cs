using DATask.Droid;
using Android.Content;
using Xamarin.Forms;
using DATask;
using System;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(AppHider))]
namespace DATask.Droid
{
    public class AppHider : IAppHider
    {
        public void HideApp()
        {
            try
            {
                Intent main = new Intent(Intent.ActionMain);
                main.AddCategory(Intent.CategoryHome);
                Forms.Context.StartActivity(main);
            }
            catch(Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, ex.Message, ToastLength.Long).Show();
            }
        }
    }
}