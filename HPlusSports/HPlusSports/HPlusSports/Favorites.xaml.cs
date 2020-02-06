using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HPlusSports.Services;

namespace HPlusSports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favorites : ContentPage
    {
        public Favorites()
        {
            InitializeComponent();
        }

        public Favorites(List<Product> p)
        {
            InitializeComponent();
            BindingContext = p;
        }

        public void Item_Selected(object sender, SelectionChangedEventArgs e)
        {
            Product p = e.CurrentSelection.First() as Product;
            Navigation.PushAsync(new ProductDetail(p));
        }
    }
}