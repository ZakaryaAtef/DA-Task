using Android.App;
using Android.Content.PM;
using Android.OS;
// New Xlabs
using XLabs.Ioc; // Using for SimpleContainer
using XLabs.Platform.Services.Geolocation; // Using for Geolocation
using XLabs.Platform.Device; // Using for Display
using XLabs.Platform.Services.Media;
using XLabs.Platform.Services;
using Android.Views;
// End new Xlabs
namespace DATask.Droid
{
    [Activity(Label = "DA Task", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Window win;
        public static int StatusBarHeight;
        protected override void OnCreate(Bundle bundle)
        {
            System.Net.ServicePointManager.DnsRefreshTimeout = 2;
            base.OnCreate(bundle);
            try
            {
                var container = new SimpleContainer();
                container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
                container.Register<IGeolocator, Geolocator>();
                container.Register<IPhoneService, PhoneService>();
                container.Register<IMediaPicker, MediaPicker>();
                Resolver.SetResolver(container.GetResolver()); // Resolving the services
            }
            catch { }
            Xamarin.Forms.Forms.Init(this, bundle);
            SetStatusBarColor(new Android.Graphics.Color(8,74,140));
            var metrics = base.Resources.DisplayMetrics;
            var w = metrics.WidthPixels;
            var h = metrics.HeightPixels;
            StatusBarHeight = 0;
            win = Window;
            int resourceId = base.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                StatusBarHeight = base.Resources.GetDimensionPixelSize(resourceId);
                h = h - StatusBarHeight;
            }
            App.setScreenSize(w, h, 0, StatusBarHeight);
            LoadApplication(new App());

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}

