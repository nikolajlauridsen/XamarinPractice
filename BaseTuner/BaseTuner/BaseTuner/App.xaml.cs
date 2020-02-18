using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TunerLib;

namespace BaseTuner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        private static TunerDatabase database;
        public static TunerDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TunerDatabase();
                }

                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
