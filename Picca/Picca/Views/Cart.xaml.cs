using Picca.Models;
using Picca.Services;
using Picca.ViewModels;
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
    public partial class Cart : ContentPage
    {
        List<object> list;
        BasketViewModel wm;
        public Cart()
        {
            InitializeComponent();
            wm = new BasketViewModel();
            this.BindingContext = wm;
            list = new List<object>();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            wm.ItemsCart.Clear();
            wm.GetItems();
        }

        private void cw_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cw.SelectedItems != null)
            {
                list = e.CurrentSelection.ToList();
            }
            
            
            
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            await Shell.Current.DisplayAlert("ХУЙ",list.Capacity.ToString(),"OK");
            foreach (var item in list)
            {
                await new BasketService().RemoveCartItemAsync((item as Basket).Name);
                wm.ItemsCart.Remove(item as Basket);
            }
            list.Clear();
            cw.SelectedItems = null;
            await Shell.Current.DisplayAlert("Успешно", "Товары убраны из корзины", "OK");
        }
    }
}