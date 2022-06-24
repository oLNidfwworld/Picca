using Firebase.Database;
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
    class OrderService
    {
        FirebaseClient client;
        public OrderService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<Order>> GetOrder()
        {
            var card = (await client.Child("Order").OnceAsync<Order>())
                .Select(c => new Order
                {
                    user_id = c.Object.user_id,
                    order_number = c.Object.order_number,
                    id_order = c.Object.id_order,
                    date = c.Object.date,
                    price = c.Object.price,
                    status = c.Object.status,
                    adress = c.Object.adress,
                    card = c.Object.card



                }).ToList();
            return card;

        }
        public async Task<ObservableCollection<Order>> GetOrderByUserId()
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty));

            var collection = new ObservableCollection<Order>();
            var items = (await GetOrder()).Where(p => p.user_id == user.id_user).ToList();
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
        public async Task<bool> AddOrder(string date, int Price, Adreses adres, Cards cards)
        {
            var user = await new UserService().GetUserByLogin(Preferences.Get("Login", string.Empty));
            var orders = await GetOrder();

            await client.Child("Order").PostAsync(new Order()
            {
                id_order = orders.Count + 1,
                user_id = user.id_user,
                status = "Готовится",
                date = date,
                order_number = new Random().Next(0, int.MaxValue),
                price = Price,
                adress = adres.Adres,
                card = cards.NumberCard

            });
            return true;
        }
    }
}
