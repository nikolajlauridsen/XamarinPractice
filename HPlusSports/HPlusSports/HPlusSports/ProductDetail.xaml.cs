using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPlusSports.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HPlusSports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        public ProductDetail()
        {
            InitializeComponent();
        }

        public ProductDetail(Product product)
        {
            InitializeComponent();
            BindingContext = product;
        }

        public void Handle_Click(object sender, EventArgs e)
        {
            Product p = BindingContext as Product;
            Navigation.PushAsync(new OrderForm(
                new Order
                {
                    ProductName = p.Name,
                    Quantity = 1
                }));
        }

        public void Handle_Favorite(object sender, EventArgs e)
        {
            Product p = BindingContext as Product;
            ProductService.WishList.Add(p);
        }
    }
}