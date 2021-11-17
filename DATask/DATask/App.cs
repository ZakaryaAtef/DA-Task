using Xamarin.Forms;

namespace DATask
{
    public class App : Application
    {
        public static int w = 0;
        public static int h = 0;
        public static int TopMargin = 0;
        public static double StatusBar = 0;
        public App()
        {
            if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                h = h - TopMargin;
            Settings.homePage = new HomePage();
            MainPage = Settings.homePage;
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {

        }
        public static void setScreenSize(int width, int height, int TopMargin = 0, int statusBar = 0)
        {
            App.w = width;
            App.h = height;
            App.TopMargin = TopMargin;
            App.StatusBar = statusBar;
        }
    }
}
