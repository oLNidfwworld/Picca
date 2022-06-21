using Picca.Models;
using Picca.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Picca.ViewModels
{
    internal class BasketViewModel:BaseViewModel
    {
        public ObservableCollection<Basket> ItemsCart { get; set; }

        public Command GoToConfirmCommand { get; set; }


        public BasketViewModel()
        {
            ItemsCart = new ObservableCollection<Basket>();

            GoToConfirmCommand = new Command(async () => await GoToConfirmAsync());
        }

        private async Task GoToConfirmAsync()
        {
            
        }

        public async void GetItems()
        {
            var list = await new BasketService().GetBasketAsync();
            foreach (var item in list)
            {

                ItemsCart.Add(item);
            }

        }
    }
}
