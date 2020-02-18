using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TunerLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseTuner.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedTunes : ContentPage
    {
        public ObservableCollection<Tuner> Tunes { get; set; }

        public SavedTunes()
        {
            InitializeComponent();

            Tunes = new ObservableCollection<Tuner>();
            MyListView.ItemsSource = Tunes;
            LoadTunes();
        }

        async void LoadTunes()
        {
            List<Tuner> tunes = await App.Database.GetTunersAsync();
            foreach (Tuner tune in tunes)
            {
                Tunes.Add(tune);
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            Tuner tune = e.Item as Tuner;

            MainPage root = this.Parent as MainPage;
            root.SwitchToTuner(tune);

            ////Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
