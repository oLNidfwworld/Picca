using Picca.Models;
using Picca.Services;
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
    public partial class OrdersDetail : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static Order order_1; 
        public OrdersDetail(Order order)
        {
            InitializeComponent();
            order_1 = order;
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            Number.Text = $"{order_1.order_number}";
            DateOrder.Text = $"Дата заказа: {order_1.date}";
            SummaTXB.Text = $"Общая сумма: {order_1.price} Р";
            Status.Text = $"Cтатус: {order_1.status}";
            CoolOrderItems.ItemsSource = null;
            CoolOrderItems.ItemsSource = await new OrderItemsService().GetOrderItemsByOrderId(order_1);
        }
    }
}