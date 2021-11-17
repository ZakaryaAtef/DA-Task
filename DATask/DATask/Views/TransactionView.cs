using FFImageLoading.Forms;
using System;
using Xamarin.Forms;
namespace DATask
{
    public class TransactionViewItem
    {
        public string Name { get; set; }
        public string Bank { get; set; }
        public string TransfereType { get; set; }
        public string Amount { get; set; }
        public string Date { get; set; }
        public ImageSource ImgStatus { get; set; }
        public Color Background { get; set; }
        public int Index { get; set; }
        public TransactionView Item { get; set; }
        public TransactionViewItem()
        {

        }
    }
    public class TransactionView : ViewCell
    {
        public CachedImage ImgFlag=new CachedImage ();
        public CustomLabel LBName=new CustomLabel();
        public CustomLabel LBBank=new CustomLabel();
        public CustomLabel LBTransfereType = new CustomLabel();
        public CustomLabel LBAmount = new CustomLabel();
        public CustomLabel LBDate = new CustomLabel();
        public CachedImage ImgStatus = new CachedImage();

        public TransactionView()
        {
            AbsoluteLayout Container = new AbsoluteLayout
            {
                WidthRequest = Settings.TransactionViewWidth,
                HeightRequest = Settings.TransactionViewHeight,
            };
            Container.SetBinding(AbsoluteLayout.BackgroundColorProperty, "Background");

            double x = 25;
            double ypoint = 25;


            ImgFlag = new CachedImage
            {
                WidthRequest = Settings.TransactionViewHeight-50,
                HeightRequest = Settings.TransactionViewHeight-50,
                Aspect = Aspect.Fill,
                Source=ImageSource.FromResource("DATask.Images.Flag.png")
            };
            Point ImgEditLocation = new Point(20, 25);
            Container.Children.Add(ImgFlag, ImgEditLocation);


            LBName = new CustomLabel
            {
                WidthRequest = Container.WidthRequest - (50 + ImgFlag.WidthRequest*2),
                HeightRequest = Settings.CustomLabelheight*1.5,
                TextColor = Settings.TextColor,
                FontSize = Settings.FontSize + 4,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment=TextAlignment.Center
            };
            LBName.SetBinding(CustomLabel.TextProperty, "Name");
            Point LBNameLocation = new Point(x+ImgFlag.WidthRequest, ypoint);
            Container.Children.Add(LBName, LBNameLocation);
         
            LBBank = new CustomLabel
            {
                WidthRequest = Container.WidthRequest - (50 + ImgFlag.WidthRequest * 2),
                HeightRequest = Settings.CustomLabelheight * 1.5,
                TextColor = Color.Black,
                FontSize = Settings.FontSize + 3,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center

            };
            LBBank.SetBinding(CustomLabel.TextProperty, "Bank");
            ypoint += LBName.HeightRequest;
            Point LBBankLocation = new Point(x+ImgFlag.WidthRequest, ypoint);
            Container.Children.Add(LBBank, LBBankLocation);


            LBTransfereType = new CustomLabel
            {
                WidthRequest = Container.WidthRequest - (50 + ImgFlag.WidthRequest * 2),
                HeightRequest = Settings.CustomLabelheight * 1.5,
                TextColor = Color.Black,
                FontSize = Settings.FontSize + 2,
                VerticalTextAlignment = TextAlignment.Center
            };
            LBTransfereType.SetBinding(CustomLabel.TextProperty, "TransfereType");
            ypoint += LBName.HeightRequest;
            Point LBTransfereTypeLocation = new Point(x + ImgFlag.WidthRequest, ypoint);
            Container.Children.Add(LBTransfereType, LBTransfereTypeLocation);

            ypoint = 25;

            LBAmount = new CustomLabel
            {
                WidthRequest =  ImgFlag.WidthRequest,
                HeightRequest = Settings.CustomLabelheight * 1.5,
                TextColor = Settings.TextColor,
                FontSize = Settings.FontSize + 4,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment=TextAlignment.Center
            };
            LBAmount.SetBinding(CustomLabel.TextProperty, "Amount");
            Point LBAmountLocation = new Point(Container.WidthRequest-(25+LBAmount.WidthRequest), ypoint);
            Container.Children.Add(LBAmount, LBAmountLocation);

            LBDate = new CustomLabel
            {
                WidthRequest = ImgFlag.WidthRequest,
                HeightRequest = Settings.CustomLabelheight * 1.5,
                TextColor = Color.Black,
                FontSize = Settings.FontSize + 2,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center
            };
            LBDate.SetBinding(CustomLabel.TextProperty, "Date");
            ypoint += LBName.HeightRequest;
            Point LBDateLocation = new Point(Container.WidthRequest - (25 + LBDate.WidthRequest), ypoint);
            Container.Children.Add(LBDate, LBDateLocation);



            ImgStatus = new CachedImage 
            {
                WidthRequest = LBName.HeightRequest,
                HeightRequest = LBName.HeightRequest,
                Aspect = Aspect.Fill,
            };
            ImgStatus.SetBinding( CachedImage.SourceProperty, "ImgStatus");
            ypoint += LBName.HeightRequest;
            Point ImgStatusLocation = new Point(Container.WidthRequest-(25+ImgStatus.WidthRequest),ypoint);
            Container.Children.Add(ImgStatus, ImgStatusLocation);
            Settings.SetFont(ref Container);
            View = Container;           
        }
    }
}
