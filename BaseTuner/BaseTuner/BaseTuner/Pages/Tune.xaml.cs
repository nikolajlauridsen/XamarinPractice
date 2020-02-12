using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TunerLib;

namespace BaseTuner.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tune : ContentPage
    {
        private Tuner _tuner;
        public Tune()
        {
            InitializeComponent();

            _tuner = new Tuner();
            BindingContext = _tuner;
        }
    }
}