using Firebase.Database;
using Picca.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ObservableCollection<Cards>> GetCardsByUserId(int user_id)
        {
            var collection = new ObservableCollection<Cards>();
            var items = (await GetCards()).Where(p => p.user_id == user_id).ToList();
            foreach(var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
