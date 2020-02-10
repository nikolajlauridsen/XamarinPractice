using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(HPlusSports.Controls.NumberEntry), typeof(HPlusSports.Droid.Controls.NumberEntryRenderer))]
namespace HPlusSports.Droid.Controls
{
    class NumberEntryRenderer : EntryRenderer
    {
        public NumberEntryRenderer(Context ctx) : base(ctx)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberVariationNormal;
            }
        }
    }
}