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

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = await App.Database.GetTunersAsync();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Tuner tune = e.Item as Tuner;
            if (DeleteMode.IsToggled)
            {
                bool confirmation = await DisplayAlert("Delete", "Are you sure you want to delete this?\nThis is permanent", "Yes", "No");
                if (confirmation)
                {
                    await App.Database.DeleteItemAsync(tune);
                    // I know this isn't very effective...
                    MyListView.ItemsSource = await App.Database.GetTunersAsync();
                }
            }
            else
            {
                MainPage root = this.Parent as MainPage;
                root.SwitchToTuner(tune);
            }
            
            ////Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
