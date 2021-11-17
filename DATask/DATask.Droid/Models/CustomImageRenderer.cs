using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ePMSFM;
using ePMSFM.Droid;
using Android.Graphics.Drawables;
using System;
using Android.Util;

[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]
namespace ePMSFM.Droid
{
    public class CustomImageRenderer : ImageRenderer
    {

        public CustomImageRenderer() : base()
        { }
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            var view = (CustomImage)Element;
            if (view == null)
            {
                return;
            }
        }
        //Px to Dp Conver
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}