using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty IsRightToLeftProperty =
            BindableProperty.Create(nameof(IsRightToLeft), typeof(bool), typeof(CustomEntry), false);

        public bool IsRightToLeft
        {
            get => (bool)GetValue(IsRightToLeftProperty);
            set => SetValue(IsRightToLeftProperty, value);
        }
    }
}
