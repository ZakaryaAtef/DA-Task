using FFImageLoading.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DATask
{
    public class TransactionScreen
    {
        int w;
        int h;
        public ScrollView Scroll = new ScrollView();
        public Grid MainLayout;
        public List<TransactionViewItem> TransactionViewItemList { get;set; }
        AbsoluteLayout Container;
        AbsoluteLayout MenuBar;
        CachedImage ImgBack;
        HomePage home;
        CustomLabel LBTop;
        ListView LVTransactions;
        private ActivityIndicator AILoading;
        private Point AILoadingLocation;
        private AbsoluteLayout ALTabs;
        private CustomLabel LBRemittance;
        private CustomLabel LBCredit;
        private CustomLabel LBTravelCard;
        private CustomLabel LBBillPayments;
        private int ButtonHeight;
        private int CustomLabelheight;
        private int width;
        private ImageSource ImgSourceTrue = ImageSource.FromResource("DATask.Images.true.png");
        private ImageSource ImgSourceFalse = ImageSource.FromResource("DATask.Images.false.png");
        private CustomLabel LBNoStrories;
        bool IsReady = true;
        private Transaction[] transactionList;
        public Transaction[] TransactionList
        {
            get { return transactionList; }
            set
            {
                transactionList = value;
                StartSearch();
            }
        }
        public TransactionScreen(int w, int h, HomePage home)
        {
            this.home = home;
            this.w = w;
            this.h = h;
            Container = new AbsoluteLayout
            {
                WidthRequest = w,
                BackgroundColor =Settings.MainColor,
            };
            InitializeComponents();

            Scroll.Content = Container;
            MainLayout = new Grid
            {
                RowSpacing = 5,
                ColumnSpacing = 0,
                BackgroundColor = Settings.MainColor
            };
            MainLayout.RowDefinitions.Add(new RowDefinition { Height = LBTop.HeightRequest });
            MainLayout.RowDefinitions.Add(new RowDefinition { Height = ALTabs.HeightRequest });
            MainLayout.RowDefinitions.Add(new RowDefinition());
            MainLayout.Margin = new Thickness(0, 0, 0, 0);
            MainLayout.Children.Add(Scroll, 0, 2);
            MainLayout.Children.Add(MenuBar, 0, 0);
            MainLayout.Children.Add(ALTabs, 0, 1);
            Settings.SetFont(ref Container);
            Settings.SetFont(ref MenuBar);
        }
        private void InitializeComponents()
        {
            double FontSize = 13;
            ButtonHeight = 45;
            width = w - 20;
            CustomLabelheight = 23;
            MenuBar = new AbsoluteLayout
            {
                BackgroundColor = Settings.MainColor,
                WidthRequest = w
            };
            LBTop = new CustomLabel
            {
                WidthRequest = w,
                HeightRequest = Settings.CustomLabelheight*2 + 30,
                TextColor = Color.White,
                FontSize = FontSize + 5,
                Text = "Transaction History",
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
            ImgBack.WidthRequest = ImgBack.HeightRequest = LBTop.HeightRequest-50;
            MenuBar.Children.Add(ImgBack, new Point(25, 25));
 
            double ypoint = 0;

            ALTabs = new AbsoluteLayout
            {
                HeightRequest = ButtonHeight*1.5,
                WidthRequest = w,  
                Padding=new Thickness(0),
                Margin=new Thickness(0),
                BackgroundColor=Color.White
            };

            LBRemittance = new CustomLabel
            {
                WidthRequest = w / 4,
                HeightRequest = ALTabs.HeightRequest,
                TextColor = Color.Black,
                FontSize = FontSize + 1,
                Text = "Remittance",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            ALTabs.Children.Add(LBRemittance,new Point(0,0));

            LBCredit = new CustomLabel
            {
                WidthRequest = w / 4,
                HeightRequest = ALTabs.HeightRequest,
                TextColor = Color.Black,
                FontSize = FontSize + 1,
                Text = "Credit Card Payment",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            ALTabs.Children.Add(LBCredit,new Point(ALTabs.WidthRequest/4,0));

            LBTravelCard = new CustomLabel
            {
                WidthRequest = w / 4,
                HeightRequest = ALTabs.HeightRequest,
                TextColor = Color.Black,
                FontSize = FontSize + 1,
                Text = "Travel Card Reload",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            ALTabs.Children.Add(LBTravelCard,new Point(ALTabs.WidthRequest/2,0));

            LBBillPayments = new CustomLabel
            {
                WidthRequest = w / 4,
                HeightRequest = ALTabs.HeightRequest,
                TextColor = Color.Black,
                FontSize = FontSize + 1,
                Text = "Bill Payments",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };
            ALTabs.Children.Add(LBBillPayments,new Point(ALTabs.WidthRequest*3/4,0));

            CustomLabel LBFullLine = new CustomLabel
            {
                WidthRequest = ALTabs.WidthRequest,
                HeightRequest = 1,
                BackgroundColor = Settings.LightShadowColor
            };
            ALTabs.Children.Add(LBFullLine, new Point(0, ALTabs.HeightRequest-LBFullLine.HeightRequest));

            CustomLabel LBMark = new CustomLabel
            {
                WidthRequest = LBRemittance.WidthRequest,
                HeightRequest = 3,
                BackgroundColor = Settings.AppColor
            };
            ALTabs.Children.Add(LBMark, new Point(0, ALTabs.HeightRequest-LBMark.HeightRequest));
            LBNoStrories = new CustomLabel
            {
                WidthRequest = width + 10,
                FontSize = FontSize,
                Text = "There is no Transactions yet.",
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                IsVisible = false
            };
            Container.Children.Add(LBNoStrories, new Point(5, 5));

            ypoint = 0;
            LVTransactions = new ListView
            {
                IsVisible = true,
                WidthRequest = width + 10,
                RowHeight = (int)(3 * CustomLabelheight*1.5 + 50),
                BackgroundColor=Color.Transparent,
                SeparatorColor=Color.Transparent
            };
            Settings.TransactionViewWidth = width + 10;
            Settings.TransactionViewHeight = LVTransactions.RowHeight;
            LVTransactions.Margin = new Thickness(0, 0, 0, 0);
            LVTransactions.ItemTemplate = new DataTemplate(typeof(TransactionView));            
            Container.Children.Add(LVTransactions, new Point(5, ypoint));

            ImgBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => ImgBackClick()),
            }
            );

            Scroll.Scrolled += Scroll_Scrolled;
            AILoading = new ActivityIndicator
            {
                Color = Color.Blue,
                IsRunning = false,
                WidthRequest = w
            };
            AILoadingLocation = new Point(0, (h / 2 - (LBTop.HeightRequest + 60)));
            Container.Children.Add(AILoading, AILoadingLocation);
        }
        private void Scroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            AILoading.TranslationY = e.ScrollY;
        }

        public async Task ResetScreenAsync()
        {
            TransactionList = new Transaction[0];
            await Settings.TransactionScreen.LoadTransactions();
        }
        private void StartSearch()
        {
            TransactionViewItemList = new List<TransactionViewItem>();
            for (int i = 0; i < TransactionList.Length; i++)
            {
                if (true)
                {
                    int index = i;
                    

                    TransactionViewItemList.Add
                    (
                    new TransactionViewItem
                    {
                        ImgStatus = (TransactionList[i].status=="false")?ImgSourceFalse:ImgSourceTrue,
                        Name= transactionList[i].name.ToUpper(),
                        Bank = transactionList[i].bank_name.ToUpper(),
                        Amount=transactionList[i].paid_amount+" PKR",
                        Date=DateTime.FromBinary(transactionList[i].createdAt).ToString("yyyy-MM-dd"),
                        TransfereType=transactionList[i].transfer_type,
                        Background=(i%2==0)?Color.FromRgb(248, 249, 250) :Color.White,
                        Index = index,
                
                    });
                }
            }
            {
                Container.Children.Remove(LVTransactions);
                Double ypoint = 0;
                LVTransactions = new ListView
                {
                    WidthRequest = width + 10,
                    RowHeight = (int)(3 * CustomLabelheight * 1.5 + 50),
                    SeparatorColor = Color.Transparent,
                    BackgroundColor = Color.Transparent
                };
                Settings.TransactionViewWidth = width + 10;
                Settings.TransactionViewHeight = LVTransactions.RowHeight;
                LVTransactions.Margin = new Thickness(0, 0, 0, 0);
                LVTransactions.ItemTemplate = new DataTemplate(typeof(TransactionView));
                LVTransactions.ItemSelected += LVTransactions_ItemSelected;
                LVTransactions.ItemTapped += LVTransactions_ItemTapped;
                Container.Children.Add(LVTransactions, new Point(5, ypoint));
            }
            if (TransactionViewItemList.Count>0)
            LVTransactions.ItemsSource = TransactionViewItemList;
            LVTransactions.Margin = new Thickness(0, 0, 0, 20);
            AILoading.TranslationY = 0;
            Scroll.ScrollToAsync(0, 0, false);
        }

        private async void LVTransactions_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (IsReady)
            {
                IsReady = false;
                AILoading.IsRunning = true;
                TransactionViewItem item = (TransactionViewItem)e.Item;
                int index = item.Index;
                string id = TransactionList[index].id;
                Transaction transaction = await Settings.GetTransactionDetails(id);
                Settings.TransactionDetails.Transaction = TransactionList[index];
                home.Content = Settings.TransactionDetails.MainLayout;
                IsReady = true;
                AILoading.IsRunning = false;

            }

        }

        private void LVTransactions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void ImgBackClick()
        {
            home.SendBackButtonPressed();
        }
        public async Task<bool> LoadTransactions()
        {
            if (IsReady)
            {
                IsReady = false;
                AILoading.IsRunning = true;
                LBNoStrories.IsVisible = false;
                Transaction[] Transactions = new Transaction[0];
                Transactions = await Settings.GetTransactionsAsync();
                if (Transactions != null)
                {
                    Settings.TransactionScreen.TransactionList = Transactions;
                    if (Transactions.Length == 0)
                    {

                        LBNoStrories.IsVisible = true;
                        LVTransactions.IsVisible = false;
                        LBNoStrories.Text = "There is no Transactions yet.";
                    }
                    else
                    {
                        LVTransactions.IsVisible = true;
                        LBNoStrories.IsVisible = false;
                    }
                    IsReady = true;
                    AILoading.IsRunning = false;
                    return true;
                }
                else
                {
                    Settings.Message = "Please check your internet connection";
                    IsReady = true;
                    AILoading.IsRunning = false;
                    return false;
                }
            }
            return false;
        }
    }
}
