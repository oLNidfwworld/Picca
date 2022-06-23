using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Picca.Services;
using Xamarin.Essentials;
using Picca.Models;
using Picca.Views.Popups;


namespace Picca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class profile : ContentPage
    {
        public static Adreses Adres;
        public static Cards Card;

        public profile()
        {
            InitializeComponent();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty)) as Users;
            CoollAdress.ItemsSource = await new AdressService().GetAdresesByUserId();

            CoolCards.ItemsSource = await new CardsService().GetCardsByUserId();
            var incartcount = await new BasketService().GetBasketAsync();
            if (incartcount.Count != 0)
            {
                CountInCart.Text = Convert.ToString(incartcount.Count);
                CountInCart.IsVisible = true;
            }
        }
        
        private void Edit_Clicked(object sender, EventArgs e)
        {
            Adres = CoollAdress.SelectedItem as Adreses;
            pancakeadress.IsVisible = true;
            pancakeadressadd.IsVisible = false;
            adressEdit.Text = Adres.Adres;

            if (pancakeadress.TranslationY == 0)
            {
                pancakeadress.TranslateTo(0, 320, 200);
            }
            else
            {
                pancakeadress.TranslateTo(0, 0, 200);
            }
        }
        private void Add_Clicked(object sender, EventArgs e)
        {
            pancakeadress.IsVisible = false;
            pancakeadressadd.IsVisible = true;
            adressEdit.Text = null;
            if (pancakeadressadd.TranslationY == 0)
            {
                pancakeadressadd.TranslateTo(0, 320, 200);
            }
            else
            {
                pancakeadressadd.TranslateTo(0, 0, 200);
            }
        }

        private async void EditbtnAdress_Clicked(object sender, EventArgs e)
        {
            var selectedadres = CoollAdress.SelectedItem as Adreses;
            if (string.IsNullOrWhiteSpace(adressEdit.Text))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Введите адрес", "Ок");
            }
            else
            {
                await new AdressService().UpdateAdres(adressEdit.Text, Adres.Adres);
                await Shell.Current.DisplayAlert("Успешно", "Адрес обновлен", "Ок");
            }
        }

        private async void AddAdress_Clicked_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(adressAdd.Text))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Введите адрес", "Ок");
            }
            else
            {
                await new AdressService().AddAdres(adressAdd.Text);
                await Shell.Current.DisplayAlert("Успешно", "Новый адрес добавлен", "Ок");
            }
        }
        private void EditCard_Clicked(object sender, EventArgs e)
        {
            Card = CoolCards.SelectedItem as Cards;
            PanCakeCardEdit.IsVisible = true;
            PanCakeCardAdd.IsVisible = false;
            EditCardEntry.Text = Card.NumberCard;
            if (PanCakeCardEdit.TranslationY == 0)
            {
                PanCakeCardEdit.TranslateTo(0, 320, 200);
            }
            else
            {
                PanCakeCardEdit.TranslateTo(0, 0, 200);
            }
        }
        private void AddCard_Clicked(object sender, EventArgs e)
        {
            PanCakeCardEdit.IsVisible = false;
            PanCakeCardAdd.IsVisible = true;
            EditCardEntry.Text = null;
            if (PanCakeCardAdd.TranslationY == 0)
            {
                PanCakeCardAdd.TranslateTo(0, 320, 200);
            }
            else
            {
                PanCakeCardAdd.TranslateTo(0, 0, 200);
            }
        }
        private async void EditbtnCard_Clicked(object sender, EventArgs e)
        {
            var selectedadres = CoolCards.SelectedItem as Cards;
            if (string.IsNullOrWhiteSpace(EditCardEntry.Text))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Введите номер карты", "Ок");
            }
            else
            {
                await new AdressService().UpdateAdres(EditCardEntry.Text, Card.NumberCard);
                await Shell.Current.DisplayAlert("Успешно", "Адрес обновлен", "Ок");
            }
        }

        private async void AddCard_Clicked_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddCardEntry.Text))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Введите номер карты", "Ок");
            }
            else
            {
                await new CardsService().AddCard(AddCardEntry.Text);
                await Shell.Current.DisplayAlert("Успешно", "Новый карта добавлена", "Ок");
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (pancakeadress.TranslationY == 0)
            {
                pancakeadress.TranslateTo(0, 320, 200);

            }
            else if (pancakeadressadd.TranslationY == 0)
            {
                pancakeadressadd.TranslateTo(0, 320, 200);

            }
            else if (PanCakeCardAdd.TranslationY == 0)
            {
                PanCakeCardAdd.TranslateTo(0, 320, 200);

            }
            else if(PanCakeCardEdit.TranslationY == 0){
                PanCakeCardEdit.TranslateTo(0, 320, 200);

            }
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
        private async void Cart_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new Cart());
        }
    }
}