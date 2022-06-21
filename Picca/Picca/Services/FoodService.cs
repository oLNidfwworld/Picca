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
    class FoodService
    {
        FirebaseClient client;
        public FoodService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<Food>> GetFood()
        {
            var food = (await client.Child("Food").OnceAsync<Food>())
                .Select(c => new Food
                {
                    id_category = c.Object.id_category,
                    imgFood = c.Object.imgFood,
                    Description = c.Object.Description,
                    Name = c.Object.Name,
                    price = c.Object.price


                }).ToList();
            return food;

        }
        public async Task<ObservableCollection<Food>> GetFoodInfo()
        {
            var food = new ObservableCollection<Food>();
            var items = await GetFood();
            foreach (var item in items)
            {
                food.Add(item);
            }
            return food;
        }
        public async Task<ObservableCollection<Food>> GetFoodInfo(FoodCategories foodCategories)
        {
            var food = new ObservableCollection<Food>();
            if(foodCategories.CategoryId == 1)
            {
                var items = await GetFood();
                foreach (var item in items)
                {
                    food.Add(item);
                }
            }
            else
            {
                var items = (await GetFood()).Where(p => p.id_category == foodCategories.CategoryId);
                foreach (var item in items)
                {
                    food.Add(item);
                }
               
            }
            return food;
        }
        public async Task<ObservableCollection<Food>> GetFoodInfo(string name, FoodCategories foodCategories)
        {

            
            var food = new ObservableCollection<Food>();
            if (foodCategories != null)
            {
                var items = (await GetFood()).Where(p => p.id_category == foodCategories.CategoryId && p.Name.Contains(name));
                foreach (var item in items)
                {
                    food.Add(item);
                }

            }
            else if (foodCategories == null)
            {
                var items = (await GetFood()).Where(p => p.Name.Contains(name));
                foreach (var item in items)
                {
                    food.Add(item);
                }

            }
            else if (string.IsNullOrEmpty(name) && foodCategories == null)
            {
                var items = await GetFood();
                foreach (var item in items)
                {
                    food.Add(item);
                }
            }
            else
            {
                var items = await GetFood();
                foreach (var item in items)
                {
                    food.Add(item);
                }
            }
            return food;

        }

        public async Task<Food> GetFoodByName(string name)
        {
            var item = (await GetFood()).Where(p => p.Name == name).FirstOrDefault();

            return item;
        }

    }
}
