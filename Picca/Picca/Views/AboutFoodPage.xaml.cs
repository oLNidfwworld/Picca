using Picca.Models;
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
    public partial class AboutFoodPage : ContentPage
    {
        public AboutFoodPage(Food food)
        {
            InitializeComponent();
            NameProduct.Text = food.Name;
            ImageProduct.Source = food.imgFood;
            DescriptionProduct.Text = food.Description;
            PriceProduct.Text = food.price.ToString();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }
    }
}