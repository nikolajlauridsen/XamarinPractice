using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseTuner.Pages;
using TunerLib;
using Xamarin.Forms;

namespace BaseTuner
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private Tune tunerPage;
        private SavedTunes savedPage;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
            tunerPage = new Tune() {Title = "Tune"};
            savedPage = new SavedTunes() {Title = "Saved Tunes"};

            this.Children.Add(tunerPage);
            this.Children.Add(savedPage);
        }

        public void SwitchToTuner(Tuner tuner)
        {
            tunerPage.ChangeContext(tuner);
            CurrentPage = tunerPage;
        }
    }
}
