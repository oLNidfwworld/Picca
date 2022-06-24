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
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
        }
        public static int Summa;
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            Summa = 0;
            var listbasket = await new BasketService().GetBasketAsync();
            foreach(var item in listbasket)
            {
                Summa = Summa + item.count * item.price;
            }
            summa.Text = $"Общая сумма: {Summa.ToString()} Р";
            CollItems.ItemsSource = null;
            CollItems.ItemsSource = await new BasketService().GetBasketAsync();
            CombAdress.ItemsSource = null;
            CombAdress.ItemsSource = await new AdressService().GetAdresesByUserId();
            CombСard.ItemsSource = null;
            CombСard.ItemsSource = await new CardsService().GetCardsByUserId();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CombAdress.SelectedItem == null || CombСard.SelectedItem == null)
            {
                await Shell.Current.DisplayAlert("Ошибка", "Выберите адресс и карту оплаты", "Ок");
            }
            else
            {
                DateTime date = DateTime.Now;
                string ate = date.ToString();
                await new OrderService().AddOrder(ate, Summa, CombAdress.SelectedItem as Adreses, CombСard.SelectedItem as Cards);
                var listbasket = await new BasketService().GetBasketAsync();
                foreach(var item in listbasket)
                {
                    await new OrderItemsService().AddOrderItems(item);
                    await new BasketService().RemoveCartItemAsync(item);
                }
                await Shell.Current.DisplayAlert("Оплата", "Оплата прошла успешно", "Ок");
                await Shell.Current.Navigation.PopModalAsync();

            }
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopModalAsync();
        }
    }
}