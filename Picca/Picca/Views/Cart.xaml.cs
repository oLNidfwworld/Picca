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

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            wm.ItemsCart.Clear();
            wm.GetItems();
            var listbasket = await new BasketService().GetBasketAsync();
            if(listbasket.Count == 0)
            {
                korzinapusta.IsVisible = true;
                coolvisible.IsVisible = false;
            }
            else
            {
                korzinapusta.IsVisible = false;
                coolvisible.IsVisible = true;
            }

        }

        private void cw_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (COLLCart.SelectedItems != null)
            {
                list = e.CurrentSelection.ToList();
            }
            var item = COLLCart.SelectedItem as Basket;
           
            
            
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            
        }

       

        private async void plus_Clicked(object sender, EventArgs e)
        {
            await new BasketService().UpdateBasket(COLLCart.SelectedItem as Basket, "plus");
            var selecteditem = COLLCart.SelectedItem as Basket;
            selecteditem.count = selecteditem.count + 1;
            COLLCart.SelectedItem = selecteditem;
            wm.ItemsCart.Clear();
            wm.GetItems();

        }
        private async void minus_Clicked_1(object sender, EventArgs e)
        {
            await new BasketService().UpdateBasket(COLLCart.SelectedItem as Basket, "minus");
            var selecteditem = COLLCart.SelectedItem as Basket;
            selecteditem.count = selecteditem.count - 1;
            COLLCart.SelectedItem = selecteditem;
            if(selecteditem.count == 0)
            {
                wm.ItemsCart.Clear();
                wm.GetItems();
            }
            wm.ItemsCart.Clear();
            wm.GetItems();

        }


        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new OrderPage());
        }

        private async void trash_Clicked_1(object sender, EventArgs e)
        {
            await new BasketService().RemoveCartItemAsync(COLLCart.SelectedItem as Basket);
            wm.ItemsCart.Clear();
            wm.GetItems();
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}