using Newtonsoft.Json;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DATask
{
    public class Settings
    {
        public static HomePage homePage;
        public static TransactionScreen TransactionScreen;
        public static TransactionDetails TransactionDetails;
        public static string BaseWebServiceUrl = "https://61769aed03178d00173dad89.mockapi.io/api/v1/";
        public static Color MainColor = Color.White;//new Color(242.0 / 255, 242.0 / 255, 244.0 / 255);
        public static Color AppColor = Color.FromRgb(31, 107, 180);//Color.FromRgb(0, 148, 116); //new Color(53.0 / 255, 85.0 / 255, 103.0 / 255); //new Color(93.0 / 255, 188.0 / 255, 210.0 / 255);
        public static Color DarkColor = Color.Gray;
        public static Color UnFocusColor = new Color(153.0 / 255, 153.0 / 0255, 153.0 / 255); //new Color(159.0/255, 159.0 / 255, 159.0 / 255);//new Color(0, 80.0 / 255, 80.0 / 255);
        public static Color LightShadowColor = new Color(235.0 / 255, 235.0 / 255, 224.0 / 255);
        public static Color ShadowColor = new Color(225.0 / 255, 225.0 / 255, 208.0 / 255);
        public static Color LightGreyColor = Color.FromRgb(223, 223, 223);
        public static IMyToast myToast = DependencyService.Get<IMyToast>();
        private static String message = "";
        public static String Message
        {
            get { return message; }
            set
            {
                message = value;
                if (value != null && value != "")
                    Settings.myToast.MakeText(value, false);
            }
        }

        public static Color TextColor = Color.FromRgb(55, 126, 196);

        public static double TransactionViewWidth;
        public static double TransactionViewHeight;
        public static Stack<View> PageStack = new Stack<View>();
        public static double FontSize = Device.OnPlatform
        (
            13,
            13,
            13
        );
        public static String Text;
        public static double ButtonHeight = 45;
        public static double CustomLabelheight = 23;
        public static string EnglishFontFamily = Device.OnPlatform
        (
            "Arial Black",
            "arial-unicode-ms.ttf#Droid Arabic Kufi",
            "Transactions/Fonts/arial-unicode-ms.ttf#Droid Arabic Kufi"
        );
        public static View EmptyView = new AbsoluteLayout { BackgroundColor = Settings.MainColor };
        public static String AppKey = "fsdjhbgkjdfhs@#$DSAFSDFG4r58";
        public static double width;
        public static double height;
        public static HttpClient httpClient = new HttpClient
        {
            Timeout = new TimeSpan(0, 0, 40)
        };
        public static List<TransactionView> TransactionViewItems = new List<TransactionView>();
        public static double TechnicianViewWidth;
        public static double TechnicianViewHeight;
        public static string GetJson(String ServerResult)
        {
            return ServerResult.Replace("\\\"","'").Replace("\"","");
        }
        public static T JsonClone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        public static async Task<Transaction[]> GetTransactionsAsync()
        {
            try
            {
                var json = await Settings.httpClient.GetStringAsync(new Uri(Settings.BaseWebServiceUrl + "transactions"));
                Transaction[] Transactions;
                try
                {
                    Transactions = JsonConvert.DeserializeObject<Transaction[]>(json);
                    return Transactions;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static async Task<Transaction> GetTransactionDetails(string id)
        {
            try
            {
                var json = await Settings.httpClient.GetStringAsync(new Uri(Settings.BaseWebServiceUrl + "transactions/"+id));
                Transaction Transaction;
                try
                {
                    Transaction = JsonConvert.DeserializeObject<Transaction>(json);
                    return Transaction;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public static void SetFont(ref AbsoluteLayout Container)
        {
            for (int i = 0; i < Container.Children.Count; i++)
            {
                Type type = Container.Children[i].GetType();
                if (type == typeof(CustomLabel))
                {
                    CustomLabel LB = (CustomLabel)Container.Children[i];
                    LB.FontFamily = Settings.EnglishFontFamily;
                }
                else if (type == typeof(Button))
                {
                    Button BT = (Button)Container.Children[i];
                    BT.FontFamily = Settings.EnglishFontFamily;
                }
                else if (type == typeof(Entry))
                {
                    Entry E = (Entry)Container.Children[i];
                    E.FontFamily = Settings.EnglishFontFamily;
                }
            }
        }
        public static void SetFont(ref StackLayout Container)
        {
            for (int i = 0; i < Container.Children.Count; i++)
            {
                Type type = Container.Children[i].GetType();
                if (type == typeof(CustomLabel))
                {
                    CustomLabel LB = (CustomLabel)Container.Children[i];
                    LB.FontFamily = Settings.EnglishFontFamily;
                }
                else if (type == typeof(Button))
                {
                    Button BT = (Button)Container.Children[i];
                    BT.FontFamily = Settings.EnglishFontFamily;
                }
                else if (type == typeof(Entry))
                {
                    Entry E = (Entry)Container.Children[i];
                    E.FontFamily = Settings.EnglishFontFamily;
                }
                else if(type==typeof(AbsoluteLayout))
                {
                    AbsoluteLayout A = (AbsoluteLayout)Container.Children[i];
                    Settings.SetFont(ref A);
                }
                else if (type == typeof(StackLayout))
                {
                    StackLayout S = (StackLayout)Container.Children[i];
                    Settings.SetFont(ref S);
                }
            }
        }
        public static FormattedString SetFormattedText(String Head,String Text,Color HeadColor,Color TextColor)
        {

            FormattedString FText = new FormattedString();
            Span STextHead = new Span
            {
                FontSize = Settings.FontSize + 1,
                FontFamily = Settings.EnglishFontFamily,
                FontAttributes = FontAttributes.Bold,
                ForegroundColor = HeadColor,
                Text ="  "+ Head
            };
            Span STextText = new Span
            {
                FontSize = Settings.FontSize+3,
                FontFamily = Settings.EnglishFontFamily,
                FontAttributes = FontAttributes.Bold,
                ForegroundColor = TextColor,
                Text = "\n  "+Text
            };
            FText.Spans.Add(STextHead); FText.Spans.Add(STextText);
            return FText;
        }
    }
}
