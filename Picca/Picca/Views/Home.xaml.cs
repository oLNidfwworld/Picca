using Picca.Models;
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
                Shell.Current.Navigation.PushAsync(new AboutFoodPage(food));

            }
        }

      
    }
}