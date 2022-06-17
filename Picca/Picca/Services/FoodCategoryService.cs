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
    class FoodCategoryService
    {
        FirebaseClient client;
        public FoodCategoryService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<FoodCategories>> GetFoodCategories()
        {
            var categories = (await client.Child("FoodCategories").OnceAsync<FoodCategories>())
                .Select(c => new FoodCategories
                {
                    CategoryId = c.Object.CategoryId,
                    CategoryName = c.Object.CategoryName,
                    imageUrl = c.Object.imageUrl


                }).ToList();
            return categories;

        }
        public async Task<ObservableCollection<FoodCategories>> GetFoodCategoriesInfo()
        {
            var Categories = new ObservableCollection<FoodCategories>();
            var items = await GetFoodCategories();
            foreach (var item in items)
            {
                Categories.Add(item);
            }
            return Categories;
        }
    }
}
