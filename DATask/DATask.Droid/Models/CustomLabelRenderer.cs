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

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace DATask.Droid
{   
    public class CustomLabelRenderer : LabelRenderer
    {
        public CustomLabelRenderer() : base()
        { }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {

            }
        }
        public override void ChildDrawableStateChanged(Android.Views.View child)
        {
            try
            {
                base.ChildDrawableStateChanged(child);
                if (Control != null && Control.Text != null)
                {
                    if (Control.TextFormatted.Length() > 0)
                        Control.TextFormatted = Control.TextFormatted;
                    else
                    {
                        Control.Text = Control.Text;
                    }
                }
            }
            catch { }
        }
    }
}