using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DATask
{
    public class CustomLabel:Label
    {
        public static readonly BindableProperty TagProperty = BindableProperty.Create(nameof(Tag),typeof(String),typeof(CustomLabel),"");
        public String Tag
        {
            get { return (String)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        public CustomLabel():base()
        {
            BackgroundColor = Color.Transparent;
            
        }
    }
}
 