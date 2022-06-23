using Firebase.Database;
using Firebase.Database.Query;
using Picca.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace Picca.Services
{
    class AdressService
    {
        FirebaseClient client;
        public AdressService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<Adreses>> GetAdresses()
        {
            var food = (await client.Child("Adreses").OnceAsync<Adreses>())
                .Select(c => new Adreses
                {
                    user_id = c.Object.user_id,
                    Adres = c.Object.Adres
            

                }).ToList();
            return food;

        }
        public async Task<ObservableCollection<Adreses>> GetAdresesByUserId()
        {
            var collection = new ObservableCollection<Adreses>();
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login",string.Empty)) as Users;

            var items = (await GetAdresses()).Where(p => p.user_id == user.id_user).ToList();
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
        public async Task<bool> AddAdres(string adress)
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty));
            await client.Child("Adreses").PostAsync(new Adreses()
            {
                Adres = adress,
                user_id = user.id_user
            });
            return true;
        }
        public async Task<bool> UpdateAdres(string newadres, string oldadres)
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty));

            var keytema = (await client.Child("Adreses")
                .OnceAsync<Adreses>())
                .FirstOrDefault
                (a => a.Object.Adres == oldadres);

            Adreses tema = new Adreses() { Adres = newadres, user_id = user.id_user };
            await client.Child("Adreses")
                .Child(keytema.Key)
                .PutAsync(tema);

            return true;
        }
        public async Task<bool> DeleteAdres(string adres)
        {
            var keytodelete = (await client.Child("Adreses").OnceAsync<Adreses>()).FirstOrDefault(a => a.Object.Adres == adres);
            await client.Child("Adreses").Child(keytodelete.Key).DeleteAsync();
            return true;
        }
    }
}
