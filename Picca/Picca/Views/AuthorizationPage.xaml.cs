using Picca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Picca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            if(Preferences.ContainsKey("Login"))
            {
                Shell.Current.GoToAsync($"//{nameof(Home)}");
            }
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            bool result = await new UserService().LoginUser(Login.Text, Password.Text);
            if(result == true)
            {
                Preferences.Set("Login", Login.Text);

                await Shell.Current.GoToAsync($"//{nameof(Home)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Неправильный логин или пароль", "ОК");
            }

        }
    }
}