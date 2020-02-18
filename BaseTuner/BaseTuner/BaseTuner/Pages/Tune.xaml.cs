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

        public Tune(Tuner tune)
        {
            InitializeComponent();

            _tuner = tune;
            BindingContext = _tuner;
        }

        public void ChangeContext(Tuner tune)
        {
            _tuner = tune;
            BindingContext = tune;
        }

        public void SaveTune(object sender, EventArgs e)
        {
            if (_tuner.Name != null)
            {
                App.Database.SaveTunerAsync(_tuner);
                DisplayAlert("Saved", "Tune Saved", "Ok");
            }
            else
            {
                DisplayAlert("No name", "Please specify a name", "Ok");
            }
        }
    }
}