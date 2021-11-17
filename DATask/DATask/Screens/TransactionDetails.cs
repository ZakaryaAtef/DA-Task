using FFImageLoading.Forms;
using Newtonsoft.Json;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace DATask
{
    public class TransactionDetails
    { 
        int w;
        int h;
        public ScrollView Scroll = new ScrollView();
        public Grid MainLayout;
        AbsoluteLayout Container;
        double RectangleHeight = Settings.CustomLabelheight * 2 + 20;
        AbsoluteLayout ALBlueBorder;
        AbsoluteLayout ALBlackBorder;
        CustomLabel LBRefNo;
        CustomLabel LBCENo;
        CustomLabel LBName;
        CustomLabel LBBank;
        CustomLabel LBLocation;
        CustomLabel LBAccountNo;
        CustomLabel LBDate;
        CustomLabel LBBackground;
        CustomLabel LBSeperator;
        CustomLabel LBRAmount;
        CustomLabel LBRAmountHead;
        CustomLabel LBPaidAmount;
        CustomLabel LBPaidAmountHead;

        Image ImgStatus;
        CustomLabel LBStatus;
        private ImageSource ImgSourceTrue = ImageSource.FromResource("DATask.Images.true.png");
        private ImageSource ImgSourceFalse = ImageSource.FromResource("DATask.Images.false.png");
        HomePage home;
        AbsoluteLayout MenuBar;
        CachedImage ImgBack;
        CustomLabel LBTop;
        private Transaction transaction;
        public Transaction Transaction
        {
            get { return transaction; }
            set
            {
                transaction = value;
                LBRefNo.FormattedText = Settings.SetFormattedText("Transaction reference #", value.reference_number, Color.Black, Settings.TextColor);
                LBCENo.FormattedText = Settings.SetFormattedText("CE number", value.cf_number, Color.Black, Color.Black);
                LBName.FormattedText = Settings.SetFormattedText("Beneficiary name", value.name, Color.Black, Color.Black);
                LBBank.FormattedText = Settings.SetFormattedText("Beneficiary bank/Agent", value.bank_name, Color.Black, Color.Black);
                LBLocation.FormattedText = Settings.SetFormattedText("Payout location", value.payout_location, Color.Black, Color.Black);
                LBAccountNo.FormattedText = Settings.SetFormattedText("Account number", value.account_number, Color.Black, Color.Black);
                LBDate.FormattedText = Settings.SetFormattedText("Payment date",DateTime.FromBinary(value.createdAt).ToString("yyyy-MM-dd"), Color.Black, Color.Black);
                ImgStatus.Source = (value.status == "false") ? ImgSourceFalse : ImgSourceTrue;
                LBRAmount.Text = value.receiving_amount;
                LBPaidAmount.Text = value.paid_amount;
                if(value.status=="false")
                {
                    LBStatus.Text = "Incomplete Transaction";
                    LBStatus.TextColor = Color.Red;
                }
                else
                {
                    LBStatus.Text = "Transaction Completed";
                    LBStatus.TextColor = Color.Green;
                }
            }
        }
        public TransactionDetails(int w, int h, HomePage home)
        {
            home.BackgroundColor = Color.Black;
            this.home = home;
            this.w = w;
            this.h = h;
            Container = new AbsoluteLayout
            {
                WidthRequest = w,
                BackgroundColor =Settings.MainColor
            };
            InitializeComponents();
            Scroll.Content = Container;
            MainLayout = new Grid
            {
                RowSpacing = 0,
                BackgroundColor = Settings.MainColor
            };
            MainLayout.RowDefinitions.Add(new RowDefinition { Height = LBTop.HeightRequest });
            MainLayout.RowDefinitions.Add(new RowDefinition());
            MainLayout.Children.Add(Scroll, 0, 1);
            MainLayout.Children.Add(MenuBar, 0, 0);
            Settings.SetFont(ref Container);
            LBTop.FontFamily = Settings.EnglishFontFamily;
        }
        private void InitializeComponents()
        {
            MenuBar = new AbsoluteLayout
            {
                BackgroundColor = Settings.MainColor,
                WidthRequest = w
            };
            LBTop = new CustomLabel
            {
                WidthRequest = w,
                HeightRequest = Settings.CustomLabelheight * 2 + 30,
                TextColor = Color.White,
                FontSize =Settings.FontSize + 5,
                Text = "Transaction Details",
                BackgroundColor = Settings.AppColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            MenuBar.Children.Add(LBTop, new Point(0, 0));
            ImgBack = new CachedImage
            {
                Aspect = Aspect.Fill,
                Source = ImageSource.FromResource("DATask.Images.Back.png")
            };
            ImgBack.WidthRequest = ImgBack.HeightRequest = LBTop.HeightRequest - 50;
            MenuBar.Children.Add(ImgBack, new Point(25, 25));

            ALBlueBorder = new AbsoluteLayout
            {
                WidthRequest = w,
                HeightRequest = 4 * RectangleHeight + 18,
                BackgroundColor = Settings.AppColor
            };
            Container.Children.Add(ALBlueBorder, new Point(0, 0));

            ALBlackBorder = new AbsoluteLayout
            {
                WidthRequest = w - 30,
                HeightRequest = 4 * RectangleHeight,
                BackgroundColor = Settings.UnFocusColor
            };
            ALBlueBorder.Children.Add(ALBlackBorder, new Point(15, 0));

            LBRefNo = new CustomLabel
            {
                WidthRequest=ALBlackBorder.WidthRequest/2-0.5,
                HeightRequest=RectangleHeight,
                BackgroundColor=Color.White,
                VerticalTextAlignment=TextAlignment.Center,
                HorizontalTextAlignment=TextAlignment.Start
            };
            ALBlackBorder.Children.Add(LBRefNo, new Point(0, 0));

            LBCENo = new CustomLabel
            {
                WidthRequest = ALBlackBorder.WidthRequest / 2 - 0.5,
                HeightRequest = RectangleHeight,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ALBlackBorder.Children.Add(LBCENo, new Point(LBRefNo.WidthRequest + 1, 0));

            LBName = new CustomLabel
            {
                WidthRequest = ALBlackBorder.WidthRequest,
                HeightRequest = RectangleHeight,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ALBlackBorder.Children.Add(LBName, new Point(0, LBRefNo.HeightRequest + 1));


            LBBank = new CustomLabel
            {
                WidthRequest = ALBlackBorder.WidthRequest / 2 - 0.5,
                HeightRequest = RectangleHeight,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ALBlackBorder.Children.Add(LBBank, new Point(0, (LBRefNo.HeightRequest + 1)*2));

            LBLocation = new CustomLabel
            {
                WidthRequest = ALBlackBorder.WidthRequest / 2 - 0.5,
                HeightRequest = RectangleHeight,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ALBlackBorder.Children.Add(LBLocation, new Point(LBRefNo.WidthRequest + 1, (LBRefNo.HeightRequest + 1)*2));

            LBAccountNo = new CustomLabel
            {
                WidthRequest = ALBlackBorder.WidthRequest / 2 - 0.5,
                HeightRequest = RectangleHeight,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ALBlackBorder.Children.Add(LBAccountNo, new Point(0, (LBRefNo.HeightRequest + 1) * 3));

            LBDate = new CustomLabel
            {
                WidthRequest = ALBlackBorder.WidthRequest / 2 - 0.5,
                HeightRequest = RectangleHeight,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ALBlackBorder.Children.Add(LBDate, new Point(LBRefNo.WidthRequest + 1, (LBRefNo.HeightRequest + 1) * 3));

            ImgStatus = new Image
            {
                WidthRequest = 80,
                HeightRequest = 80,
                Aspect = Aspect.Fill,
            };
            double YPoint = ALBlueBorder.HeightRequest + 30;
            Container.Children.Add(ImgStatus, new Point((w-ImgStatus.WidthRequest)/2, YPoint));
            LBStatus = new CustomLabel
            {
                WidthRequest = w,
                HeightRequest = Settings.ButtonHeight,
                TextColor = Color.Green,
                FontSize = Settings.FontSize + 5,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment=TextAlignment.Center
            };
            YPoint += ImgStatus.HeightRequest + 20;
            Container.Children.Add(LBStatus, new Point(0, YPoint));

            LBBackground = new CustomLabel
            {
                BackgroundColor = Color.FromRgb(230, 231, 231),
                WidthRequest = w - 40,
                HeightRequest = Settings.CustomLabelheight * 2 + 30
            };
            YPoint += Settings.ButtonHeight + 20;
            Container.Children.Add(LBBackground, new Point(20, YPoint));

            LBSeperator = new CustomLabel
            {
                BackgroundColor = Settings.UnFocusColor,
                WidthRequest = 1,
                HeightRequest = LBBackground.HeightRequest - 20
            };
            Container.Children.Add(LBSeperator, new Point((w - 1) / 2, YPoint + 10));

            LBRAmount = new CustomLabel
            {
                WidthRequest = LBBackground.WidthRequest / 2,
                HeightRequest = Settings.CustomLabelheight,
                TextColor = Color.Green,
                FontSize = Settings.FontSize + 5,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor=Color.Transparent
            };
            Container.Children.Add(LBRAmount, new Point(20,YPoint+10));

            LBPaidAmount = new CustomLabel
            {
                WidthRequest = LBBackground.WidthRequest / 2,
                HeightRequest = Settings.CustomLabelheight,
                TextColor = Color.Red,
                FontSize = Settings.FontSize + 5,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Transparent
            };
            Container.Children.Add(LBPaidAmount, new Point(20+LBRAmount.WidthRequest+1,YPoint+ 10));


            LBRAmountHead = new CustomLabel
            {
                WidthRequest = LBBackground.WidthRequest / 2,
                HeightRequest = Settings.CustomLabelheight,
                TextColor = Color.Black,
                FontSize = Settings.FontSize + 2,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Transparent,
                Text="Receiving amount"
            };
            Container.Children.Add(LBRAmountHead, new Point(20, YPoint + 10*2+Settings.CustomLabelheight));

            LBPaidAmountHead = new CustomLabel
            {
                WidthRequest = LBBackground.WidthRequest / 2,
                HeightRequest = Settings.CustomLabelheight,
                TextColor = Color.Black,
                FontSize = Settings.FontSize + 2,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Transparent,
                Text = "Total paid"
            };
            Container.Children.Add(LBPaidAmountHead, new Point(20+LBPaidAmountHead.WidthRequest + 1, YPoint + 10 * 2 + Settings.CustomLabelheight));


            CustomLabel BTNewTrans = new CustomLabel
            {
                WidthRequest = (w-60)/2,
                HeightRequest = Settings.ButtonHeight*1.3,
                TextColor = Color.White,
                BackgroundColor = Settings.AppColor,
                FontSize =Settings.FontSize,
                Text = "NEW TRANSACTION",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };

            Frame FNewTrans = new Frame
            {
                HeightRequest = BTNewTrans.HeightRequest,
                WidthRequest = BTNewTrans.WidthRequest,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                HasShadow = false,
                IsClippedToBounds = true,
                CornerRadius = 20,
                Content = BTNewTrans
            };
            YPoint += (LBBackground.HeightRequest+20);
            Point  BTNewTransLocation = new Point(20, YPoint);
            Container.Children.Add(FNewTrans, BTNewTransLocation);



            CustomLabel BTSendReceipt = new CustomLabel
            {
                WidthRequest = (w - 60) / 2,
                HeightRequest = Settings.ButtonHeight*1.3,
                TextColor = Color.Black,
                BackgroundColor = Color.FromRgb(230, 231, 231),
                FontSize = Settings.FontSize,
                Text = "SEND RECEIPT",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };

            Frame FSendReceipt = new Frame
            {
                HeightRequest = BTSendReceipt.HeightRequest,
                WidthRequest = BTSendReceipt.WidthRequest,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                HasShadow = false,
                IsClippedToBounds = true,
                CornerRadius = 20,
                Content = BTSendReceipt
            };
            Point BTSendReceiptLocation = new Point(20*2+BTNewTrans.WidthRequest, YPoint);
            Container.Children.Add(FSendReceipt, BTSendReceiptLocation);
            ImgBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => ImgBackClick()),
            }
            );
        }
        private void ImgBackClick()
        {
            home.SendBackButtonPressed();
        }
    }
}
