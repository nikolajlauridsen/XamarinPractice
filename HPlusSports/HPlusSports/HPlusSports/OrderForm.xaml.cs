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
    // Since we're using platform specific controls in this view we can't precompile the xaml
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class OrderForm : TabbedPage
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        public OrderForm(Order target)
        {
            InitializeComponent();
            BindingContext = target;
        }

        public void Handle_Clicked(object sender, EventArgs e)
        {
            Order o = BindingContext as Order;
            DisplayAlert("Order Placed", $"Order placed for {o.Quantity} of {o.ProductName}", "OK");
        }
    }
}