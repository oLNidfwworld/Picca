using Picca.Models;
using Picca.Services;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Picca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrders : ContentPage
    {
        public MyOrders()
        {
            InitializeComponent();
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            CollOrders.ItemsSource = null;
            CollOrders.ItemsSource = await new OrderService().GetOrderByUserId();
        }
        public static Order order;
        private void CollOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CollOrders.SelectedItem != null)
            {
                order = CollOrders.SelectedItem as Order;
                CollOrders.SelectedItem = null;
                Shell.Current.Navigation.PushPopupAsync(new OrdersDetail(order));

            }
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}