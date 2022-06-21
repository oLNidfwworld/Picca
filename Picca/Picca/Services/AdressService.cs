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
    class AdressService
    {
        FirebaseClient client;
        public AdressService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<Adress>> GetAdresses()
        {
            var food = (await client.Child("Adreses").OnceAsync<Adress>())
                .Select(c => new Adress
                {
                    user_id = c.Object.user_id,
                    adress = c.Object.adress
            

                }).ToList();
            return food;

        }
        public async Task<ObservableCollection<Adress>> GetAdresesByUserId(int id)
        {
            var collection = new ObservableCollection<Adress>();

            var items = (await GetAdresses()).Where(p => p.user_id == id).ToList();
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
