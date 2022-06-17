using Picca.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Picca.Services;
using Xamarin.Forms;

namespace Picca.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<FoodCategories> _FoodCat;

        public ObservableCollection<FoodCategories> FoodCat
        {
            get { return _FoodCat;
                
            }
            set { _FoodCat = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Food> _Food;

        public ObservableCollection<Food> Food
        {
            get
            {
                return _Food;

            }
            set
            {
                _Food = value;
                OnPropertyChanged();
            }
        }
        private FoodCategories _SelectedCategory;

        public FoodCategories SelectedCategory
        {
            get { return _SelectedCategory; }
            set { _SelectedCategory = value;
                GetFoodCategory(_SelectedCategory);
                OnPropertyChanged();
            }
        }
        private string _filterName;

        public string FileterName
        {
            get { return _filterName; }
            set { _filterName = value;
                GetFoodCategoryName(_SelectedCategory, _filterName);
                OnPropertyChanged();
            }
        }


        public HomeViewModel()
        {
            FoodCat = new ObservableCollection<FoodCategories>();
            Food = new ObservableCollection<Food>();
            GetFood();
            GetCategories();
        }
       

        public async void GetCategories()
        {
            var data = await new FoodCategoryService().GetFoodCategoriesInfo();
            FoodCat.Clear();
            foreach (var item in data)
            {
                FoodCat.Add(item);
            }
        }
        public async void GetFood()
        {
            var data = await new FoodService().GetFoodInfo();
            Food.Clear();
            foreach (var item in data)
            {
                Food.Add(item);
            }
        }
        public async void GetFoodCategory(FoodCategories foodCategories)
        {
            var data = await new FoodService().GetFoodInfo(foodCategories);
            Food.Clear();
            foreach (var item in data)
            {
                Food.Add(item);
            }
        }
        public async void GetFoodCategoryName(FoodCategories foodCategories, string name)
        {
            var data = await new FoodService().GetFoodInfo(name, foodCategories);
            Food.Clear();
            foreach (var item in data)
            {
                Food.Add(item);
            }
        }

    }
}
