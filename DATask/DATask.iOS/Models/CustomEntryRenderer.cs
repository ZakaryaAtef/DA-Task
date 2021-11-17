using Xamarin.Forms;
using ePMSFM;
using ePMSFM.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace ePMSFM.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer() : base()
        { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderColor = UIColor.Gray.CGColor;
            Control.Layer.BorderWidth = 0.5f;
            Control.Layer.CornerRadius = 10;
            
        }

    }
}