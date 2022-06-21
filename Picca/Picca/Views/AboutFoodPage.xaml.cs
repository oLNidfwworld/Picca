using Picca.Models;
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
            Shell.Current.Navigation.PopPopupAsync();   
        }
    }
}