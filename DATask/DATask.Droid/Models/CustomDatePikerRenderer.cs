using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DATask;
using DATask.Droid;
using Android.Runtime;
using Android.Views;
using Android.Graphics;
using System.Threading.Tasks;
using System;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace DATask.Droid
{   
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer() : base()
        { }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Gravity = GravityFlags.CenterHorizontal;
            }
        }
    }
}