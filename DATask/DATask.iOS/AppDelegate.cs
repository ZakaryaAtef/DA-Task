
using Foundation;
using UIKit;
// New Xlabs
using XLabs.Ioc; // Using for SimpleContainer
using XLabs.Platform.Services.Geolocation; // Using for Geolocation
using XLabs.Platform.Device; // Using for Device
using XLabs.Platform.Services.Media;
using UserNotifications;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace DATask.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static bool NotificationRecieved = false;
        public static bool IsRunning = false;
        bool firstRun = true;
        public int NotificationId=1;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            System.Net.ServicePointManager.DnsRefreshTimeout = 0;
            var container = new XLabs.Ioc.SimpleContainer();
            container.Register<IDevice>(t => AppleDevice.CurrentDevice);
            container.Register<IGeolocator, Geolocator>();
            container.Register<IMediaPicker, MediaPicker>();
            Resolver.SetResolver(container.GetResolver());
            // End new Xlabs
            global::Xamarin.Forms.Forms.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            double top = 0;// UIApplication.SharedApplication.StatusBarFrame.Height;
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            {
                double Height = UIScreen.MainScreen.NativeBounds.Height;
                if (Height == 2436 || Height == 2688 || Height == 1792)
                {
                    top = UIApplication.SharedApplication.StatusBarFrame.Height;
                }
            }
            App.setScreenSize((int)UIScreen.MainScreen.Bounds.Width, (int)UIScreen.MainScreen.Bounds.Height, (int)top);
            LoadApplication(new App());
            UIApplication.SharedApplication.StatusBarHidden = true;
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound | UNAuthorizationOptions.Badge, (approved, err) => {
                // Handle approval
            });
            UNUserNotificationCenter.Current.GetNotificationSettings((settings) => {
                var alertsAllowed = (settings.AlertSetting == UNNotificationSetting.Enabled);
            });

            return base.FinishedLaunching(app, options);
        }
        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
        }
    }
}
