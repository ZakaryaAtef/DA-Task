using AsNum.XFControls;
using FFImageLoading.Forms;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace DATask
{
    public class LogInScreen
    {
        int w;
        int h;
        public ScrollView Scroll = new ScrollView();
        public Grid MainLayout;
        AbsoluteLayout Container;
        public HomePage home;
        Color MainColor = Color.White;
        Color CustomLabelColor = Color.Black;
        Color ButtonColor = Settings.DarkColor;
        Entry EUserName;
        Entry EPassword;
        CustomLabel LBShowPassword;
        CustomLabel BTLogIn;
        CustomLabel LBTop;
        CachedImage ImgLogo;
        private Point BTLogInLocation;
        private bool IsReady = true;
        private ActivityIndicator AILoading;
        private Point AILoadingLocation;
        private AbsoluteLayout MenuBar;
        public LogInScreen(int w, int h, HomePage home)
        {
            //home.BackgroundColor = CustomLabelColor;
            this.home = home;
            this.w = w;
            this.h = h;
            Container = new AbsoluteLayout
            {
                WidthRequest = w,
                BackgroundColor = MainColor
            };
            InitializeComponents();
            Scroll.Content = Container;
            MainLayout = new Grid
            {
                RowSpacing = 0,
                BackgroundColor = MainColor//Settings.MainColor
            };
            MainLayout.RowDefinitions.Add(new RowDefinition { Height = LBTop.HeightRequest });
            MainLayout.RowDefinitions.Add(new RowDefinition());
            MainLayout.Children.Add(Scroll, 0, 1);
            MainLayout.Children.Add(MenuBar, 0, 0);
            //Settings.MainColor;
            Settings.SetFont(ref Container);
            LBTop.FontFamily = Settings.EnglishFontFamily;
        }
        private void InitializeComponents()
        {
            double FontSize = Settings.FontSize;
            int ButtonHeight = 45;
            double width = 2 * w / 3.0;
            int CustomLabelheight = 23;
            //Top CustomLabel
            MenuBar = new AbsoluteLayout
            {
                BackgroundColor = Settings.AppColor,
                WidthRequest = w,
                HeightRequest = CustomLabelheight * 2 + 20
            };
            LBTop = new CustomLabel
            {
                WidthRequest = w,
                HeightRequest = CustomLabelheight * 2 + 20,
                TextColor = Color.White,
                FontSize = FontSize + 2,
                Text = "",
                BackgroundColor = Settings.AppColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
            };
            MenuBar.Children.Add(LBTop, new Point(0, 0));
            ImgLogo = new CachedImage 
            {
                HeightRequest = LBTop.HeightRequest - 20
            };
            ImgLogo.WidthRequest = ImgLogo.HeightRequest * 4.4;
            ImgLogo.Aspect = Aspect.Fill;
            ImgLogo.Source = ImageSource.FromResource("DATask.Images.Logo.png");
            Point ImgLogoLocation = new Point((w - ImgLogo.WidthRequest), 13);
            MenuBar.Children.Add(ImgLogo, ImgLogoLocation);
            Settings.Message = "";
            //Main Controls
            double ypoint = (h - (LBTop.HeightRequest*2+Settings.ButtonHeight*3)) / 2;
            ypoint = (ypoint > 0) ? ypoint : 20;
            double x = (w - width) / 2;

            EUserName = new Entry
            {
                WidthRequest = width,
                HeightRequest = ButtonHeight,
                TextColor = CustomLabelColor,
                FontSize = FontSize,
                Placeholder = "User Name",
                Text = "",
                HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.White
            };
            ypoint += ButtonHeight;
            Point EEmailOrPhoneLocation = new Point(x, ypoint);
            Container.Children.Add(EUserName, EEmailOrPhoneLocation);
            EPassword = new Entry
            {
                WidthRequest = width,
                HeightRequest = ButtonHeight,
                TextColor = CustomLabelColor,
                FontSize = FontSize,
                FontFamily = Settings.EnglishFontFamily,
                Placeholder = "Password",
                Text = "",
                IsPassword = true,
                BackgroundColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ypoint += (ButtonHeight);
            Point EPasswordLocation = new Point(x, ypoint);
            Container.Children.Add(EPassword, EPasswordLocation);

            LBShowPassword = new CustomLabel
            {
                WidthRequest = 50,
                HeightRequest = ButtonHeight,
                TextColor = Settings.UnFocusColor,
                FontSize = FontSize - 2,
                Text = "Show",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            Point LBShowPasswordLocation = new Point(w - (x + 50), ypoint - 1);
            Container.Children.Add(LBShowPassword, LBShowPasswordLocation);

            BTLogIn = new CustomLabel
            {
                WidthRequest = width / 2,
                HeightRequest = ButtonHeight,
                TextColor = Color.White,
                BackgroundColor = Settings.AppColor,
                FontSize = FontSize,
                Text = "Sign In",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };


            Frame FLogin = new Frame
            {
                HeightRequest = BTLogIn.HeightRequest,
                WidthRequest = BTLogIn.WidthRequest,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                HasShadow = false,
                IsClippedToBounds = true,
                CornerRadius = 10,
                Content = BTLogIn
            };

            ypoint += (ButtonHeight);
            BTLogInLocation = new Point(x + (width / 4), ypoint);
            Container.Children.Add(FLogin, BTLogInLocation);

            BTLogIn.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => BTLogIn_Clicked())
            });
            
            LBShowPassword.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => LBShowPasswordClick())
            });
            Scroll.Scrolled += Scroll_Scrolled;
            AILoading = new ActivityIndicator
            {
                Color = Color.Blue,
                IsRunning = false,
                WidthRequest = w
            };
            AILoadingLocation = new Point(0, (h / 2 - (LBTop.HeightRequest + 30)));
            Container.Children.Add(AILoading, AILoadingLocation);
          
        }
       private void LBShowPasswordClick()
        {
            if (EPassword.IsPassword)
            {
                EPassword.IsPassword = false;
                LBShowPassword.Text = "Hide";
            }
            else
            {
                EPassword.IsPassword = true;
                LBShowPassword.Text = "Show";
            }
        }
        private void Scroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            AILoading.TranslationY = e.ScrollY;
        }
        private void RedrawAILoading()
        {
            if (Container.Children.Contains(AILoading))
            {
                Container.Children.Remove(AILoading);
                Container.Children.Add(AILoading, AILoadingLocation);
            }
        }
        private async void BTLogIn_Clicked()
        {
            bool goodData = false;
            if (EUserName.Text.Length < 1)
            {
                Settings.Message = "Please enter user name";
            }
            else if (EPassword.Text.Length < 1)
            {
                Settings.Message = "Please enter your password";
            }
            else if (EUserName.Text.Length < 2)
            {
                Settings.Message = "Please Enter Correct Email Or Phone Number";
            }
            else
            {
                goodData = true;
            }
            if (goodData && IsReady)
            {
                IsReady = false;
                AILoading.IsRunning = true;
                {
                    try
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("AppKey", Settings.AppKey),
                            new KeyValuePair<string, string>("UserName", EUserName.Text),
                            new KeyValuePair<string, string>("Password", EPassword.Text),

                        };
                        var content = new FormUrlEncodedContent(pairs);
                        var ServerResult = await Settings.httpClient.PostAsync(new Uri(Settings.BaseWebServiceUrl + "Login"), content);
                        ServerResult.EnsureSuccessStatusCode();
                        string responseBody = await ServerResult.Content.ReadAsStringAsync();
                        string json = Settings.GetJson(responseBody);
                        String Response = json;
                        Settings.Message = "";
                        if (Response == "false")
                        {
                            Settings.Message = "Invalid credentials!";
                        }
                        else
                        {
                            Settings.TransactionScreen = new TransactionScreen(w, h, home);
                            Settings.TransactionDetails = new TransactionDetails(w, h, home);
                            Settings.TransactionScreen.ResetScreenAsync();
                            await Settings.TransactionScreen.LoadTransactions();
                            Settings.homePage.Content = Settings.TransactionScreen.MainLayout;
                        }
                    }
                    catch(Exception ex)
                    {
                        Settings.Message = "Please check your internet connection";
                    }
                    IsReady = true;
                    AILoading.IsRunning = false;
                }
            }
        }
    }
}