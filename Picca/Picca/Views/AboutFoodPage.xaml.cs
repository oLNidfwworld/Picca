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
    public partial class AboutFoodPage : Rg.Plugins.Popup.Pages.PopupPage
    {
       
        public static string food_name;
        public AboutFoodPage(Food food)
        {
            InitializeComponent();
            NameProduct.Text = food.Name;
            ImageProduct.Source = food.imgFood;
            DescriptionProduct.Text = food.Description;
            PriceProduct.Text = food.price.ToString();
            food_name = food.Name;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopPopupAsync();   
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await new BasketService().AddToCart(food_name);
            await Shell.Current.DisplayAlert("Успешно","Позиция успешно добавлена в корзину", "Ок");
            await Shell.Current.Navigation.PopPopupAsync();

        }
    }
}