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
    class CardsService
    {
        FirebaseClient client;
        public CardsService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<Cards>> GetCards()
        {
            var card = (await client.Child("Cards").OnceAsync<Cards>())
                .Select(c => new Cards
                {
                    user_id = c.Object.user_id,
                    CSV = c.Object.CSV,
                    DateOver = c.Object.DateOver,
                    NameOnTheCard = c.Object.NameOnTheCard,
                    SurnameOnTheCard = c.Object.SurnameOnTheCard,
                    NumberCard = c.Object.NumberCard
                    


                }).ToList();
            return card;

        }
        public async Task<ObservableCollection<Cards>> GetCardsByUserId()
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty)) as Users;

            var collection = new ObservableCollection<Cards>();
            var items = (await GetCards()).Where(p => p.user_id == user.id_user).ToList();
            foreach(var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
        public async Task<bool> AddCard(string number)
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty));
            await client.Child("Cards").PostAsync(new Cards()
            {
                NumberCard = number,
                user_id = user.id_user
            });
            return true;
        }
        public async Task<bool> UpdateCard(string newnumber, string oldnumber)
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty));

            var keytema = (await client.Child("Cards")
                .OnceAsync<Cards>())
                .FirstOrDefault
                (a => a.Object.NumberCard == oldnumber);

            Cards tema = new Cards() { NumberCard = newnumber, user_id = user.id_user };
            await client.Child("Cards")
                .Child(keytema.Key)
                .PutAsync(tema);

            return true;
        }
        public async Task<bool> DeleteCard(string number)
        {
            var keytodelete = (await client.Child("Cards").OnceAsync<Cards>()).FirstOrDefault(a => a.Object.NumberCard == number);
            await client.Child("Cards").Child(keytodelete.Key).DeleteAsync();
            return true;
        }
    }
}
