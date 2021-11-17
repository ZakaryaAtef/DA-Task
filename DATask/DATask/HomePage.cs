using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
namespace DATask
{
    public class HomePage : ContentPage
    {
        double w;
        double h;
        public HomePage()
        {
           
            w = App.w;
            h = App.h;
            var hardware = Resolver.Resolve<IDevice>();
            if ((w == 0 || h == 0) && Device.OS != TargetPlatform.iOS)
            {
                w = hardware.Display.Width / hardware.Display.Scale;
                h = hardware.Display.Height / hardware.Display.Scale;
            }
            else if (Device.OS != TargetPlatform.iOS)
            {
                w = w / hardware.Display.Scale;
                h = h / hardware.Display.Scale;
            }
            this.PropertyChanged += Content_PropertyChanged;
            Settings.TransactionScreen = new TransactionScreen((int)w,(int) h, this);
            Settings.TransactionDetails = new TransactionDetails((int)w, (int) h, this);
            this.Content = Settings.TransactionScreen.MainLayout;
            Settings.TransactionScreen.ResetScreenAsync();
        }
        private void Content_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "Content")
                {
                    if (Content != null && Content != Settings.EmptyView)
                    {
                        if (Settings.PageStack.Count != 0)
                        {
                            if (Content != Settings.PageStack.Peek())
                            {
                                if (Content == Settings.TransactionScreen.MainLayout)
                                    Settings.PageStack.Clear();
                                Settings.PageStack.Push(Content);
                            }
                        }
                        else
                        {
                            Settings.PageStack.Push(Content);
                        }

                        if(App.TopMargin!=0)
                        {
                            Settings.homePage.BackgroundColor = Settings.AppColor;
                            Settings.homePage.Content.Margin = new Thickness(0, App.TopMargin, 0, 0);
                        }
                    }
                }
            }
            catch { }
        }
        protected override bool OnBackButtonPressed()
        {
            try 
            {
                View Current = Settings.PageStack.Pop();
                if (Current == Settings.TransactionScreen.MainLayout)
                {
                    ShowExitDialog();
                    Settings.PageStack.Clear();
                    Settings.PageStack.Push(Current);
                    return true;
                }
                try
                {
                    this.Content = Settings.PageStack.Pop();
                }
                catch { }
                return true;
            }
            catch
            {
                if (Settings.TransactionScreen != null)
                {
                    Content = Settings.TransactionScreen.MainLayout;
                    return true;
                }
                else
                    return false;
            }
        }
        private async void ShowExitDialog()
        {
            bool close = false;
            close = await DisplayAlert("DA Task", "Do you want to exit application?", "Yes", "No");
            if (close)
            {
                DependencyService.Get<IAppHider>().HideApp();
            }
        }
        public void RefreshContent()
        {
            if (Settings.PageStack.Count != 0)
            {
                ClearValue(ContentProperty); 
                Content = Settings.PageStack.Peek();
            }
        }
    }
}
