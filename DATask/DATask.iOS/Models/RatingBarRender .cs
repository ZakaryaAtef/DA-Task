using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;
using ePMSFM.iOS;
using ePMSFM;
[assembly: ExportRenderer(typeof(CustomRatingBar), typeof(RatingRender))]
namespace ePMSFM.iOS
{
    public class RatingRender : ViewRenderer<CustomRatingBar, RatingBar>
    {

        private RatingBar RB = null;

        private bool IsDisposed = false;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomRatingBar> e)
        {
            base.OnElementChanged(e);

            if (this.RB != null)
            {
                this.RB.RateChanged -= this.RateChanged;
            }
            if (RB == null)
            {
                this.RB = new RatingBar();
                this.RB.RateChanged += this.RateChanged;
                this.SetNativeControl(this.RB);
                this.Control.SizeToFit();
                this.Update();
            }
        }

        private void RateChanged(object sender, RatingBarRateChangeEventArgs e)
        {
            if (this.Element != null)
            {
                this.Element.Rate = e.Rate;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(CustomRatingBar.IsIndicatorProperty.PropertyName) ||
                e.PropertyName.Equals(CustomRatingBar.StarCountProperty.PropertyName) ||
                e.PropertyName.Equals(CustomRatingBar.RateProperty.PropertyName) ||
                e.PropertyName.Equals(CustomRatingBar.StepProperty.PropertyName) ||
                e.PropertyName.Equals(CustomRatingBar.SelectedColorProperty.PropertyName) ||
                e.PropertyName.Equals(CustomRatingBar.UnSelectedColorProperty.PropertyName)
                )
                {

                    this.Update();
                }
        }


        private void Update()
        {
            this.RB.IsIndicator = this.Element.IsIndicator;
            this.RB.StarNum = this.Element.StarCount;
            this.RB.Rate = this.Element.Rate;
            this.RB.Step = this.Element.Step;

            //CGColor c1 = null;
            //if (!this.Element.SelectedColor.Equals(Color.Default))
            //    c1 = this.Element.SelectedColor.ToCGColor();
            //CGColor c2 = null;
            //if (!this.Element.UnSelectedColor.Equals(Color.Default))
            //    c2 = this.Element.UnSelectedColor.ToCGColor();

            //this.SetColor(c1, c2);
        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this.Control.UpdateLayout(this.Bounds.Width, this.Bounds.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                if (this.RB != null)
                {
                    this.RB.Dispose();
                    this.RB = null;
                }
            }
            base.Dispose(disposing);
        }

    }
}