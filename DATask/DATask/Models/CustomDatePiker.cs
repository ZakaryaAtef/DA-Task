using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DATask
{
    public class CustomDatePicker:DatePicker
    {
        public static readonly BindableProperty TagProperty = BindableProperty.Create(nameof(Tag),typeof(String),typeof(CustomLabel),"");
        public String Tag
        {
            get { return (String)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }
    }
}
 