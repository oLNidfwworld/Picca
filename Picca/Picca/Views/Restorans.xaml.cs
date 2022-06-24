using Picca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Picca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Restorans : ContentPage
    {
        public Restorans()
        {
            InitializeComponent();
            var positions = new Position(55.78603025811979, 37.67634453485035);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(100)));

        }

        private async void Cart_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new Cart());
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            var incartcount = await new BasketService().GetBasketAsync();
            if (incartcount.Count != 0)
            {
                CountInCart.Text = Convert.ToString(incartcount.Count);
                CountInCart.IsVisible = true;
            }
            else
            {
                CountInCart.Text = null;
                CountInCart.IsVisible = false;
            }
        }
    }
}