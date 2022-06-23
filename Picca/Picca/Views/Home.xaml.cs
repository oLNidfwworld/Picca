using Firebase.Database;
using Picca.Models;
using Picca.Services;
using Picca.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Picca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        HomeViewModel home;
        public Home()
        {
            InitializeComponent();
            home = new HomeViewModel();
            this.BindingContext = home;
            ggggg.TabIndex = 1;

        }
        public static Food food { get; set; }
        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (FoodItems.SelectedItem != null)
            {
                food = FoodItems.SelectedItem as Food;
                FoodItems.SelectedItem = null;
                Shell.Current.Navigation.PushPopupAsync(new AboutFoodPage(food));

            }
        }

        private async void Cart_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new Cart());
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            var incartcount = await new BasketService().GetBasketAsync();
            if(incartcount.Count != 0)
            {
                CountInCart.Text = Convert.ToString(incartcount.Count);
                CountInCart.IsVisible = true;
            }
        }
    }
}