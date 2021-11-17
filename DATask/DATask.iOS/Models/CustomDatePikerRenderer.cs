using Xamarin.Forms;
using DATask;
using DATask.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace DATask.iOS
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
                Control.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}