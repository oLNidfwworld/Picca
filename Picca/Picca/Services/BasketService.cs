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
    class BasketService
    {
        FirebaseClient client;
        public BasketService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<bool> IsItemInWishList(string foodname)
        {
            var cureent_user_id = await new UserService().GetUserByLogin(Preferences.Get("Login", "none"));
            var itemincart = (await client.Child("Basket").OnceAsync<Basket>()).Where(u => u.Object.user_id == cureent_user_id.id_user && u.Object.Name == foodname).FirstOrDefault();
            return (itemincart != null);
        }

        public async Task AddToCart(string foodname)
        {
            string xyu = Preferences.Get("Login", string.Empty);
            var cureent_user_id = await new UserService().GetUserByLogin(xyu);
            var food = await new FoodService().GetFoodByName(foodname);
            if (await IsItemInWishList(foodname))
            {
                var itemincart = (await client.Child("Basket").OnceAsync<Basket>()).Where(u => u.Object.user_id == cureent_user_id.id_user && u.Object.Name == foodname).FirstOrDefault();
                itemincart.Object.count += 1;
                await client.Child("Basket").Child(itemincart.Key).PutAsync(itemincart.Object);
            }
            else
            {
                await client.Child("Basket").PostAsync(new Basket()
                {
                    id = new Random().Next(0, int.MaxValue),
                    user_id = cureent_user_id.id_user,
                    count = 1,
                    imgFood = food.imgFood,
                    Name = food.Name,
                    price = food.price
                });
            }

        }

        public async Task<ObservableCollection<Basket>> GetBasketAsync()
        {
            var cureent_user_id = await new UserService().GetUserByLogin(Preferences.Get("Login", "none"));
            var itemslist = new ObservableCollection<Basket>();
            itemslist.Clear();
            var items = (await client.Child("Basket").OnceAsync<Basket>()).Where(i => i.Object.user_id == cureent_user_id.id_user).ToList();
            foreach (var item in items)
            {
                itemslist.Add(item.Object);
            }
            return itemslist;
        }

        public async Task RemoveCartItemAsync(string name)
        {
            var toRemoveItem = (await client.Child("Basket").OnceAsync<Basket>()).FirstOrDefault(i => i.Object.Name == name);
            await client.Child("Basket").Child(toRemoveItem.Key).DeleteAsync();
        }

    }
}
