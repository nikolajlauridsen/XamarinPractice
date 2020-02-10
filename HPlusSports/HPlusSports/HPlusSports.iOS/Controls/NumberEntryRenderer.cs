using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HPlusSports.Controls.NumberEntry), typeof(HPlusSports.iOS.Controls.NumberEntryRenderer))]

namespace HPlusSports.iOS.Controls
{
    public class NumberEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.KeyboardType = UIKeyboardType.NumberPad;
            }
        }
    }
}