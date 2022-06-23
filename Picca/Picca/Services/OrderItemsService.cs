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
    class OrderItemsService
    {
        FirebaseClient client;
        public OrderItemsService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<OrderItems>> GetOrderItems()
        {
            var card = (await client.Child("OrderItems").OnceAsync<OrderItems>())
                .Select(c => new OrderItems
                {
                   id_order = c.Object.id_order,
                   imgFood = c.Object.imgFood,
                   Name = c.Object.Name,
                   price = c.Object.price,
                   count = c.Object.count



                }).ToList();
            return card;

        }
        public async Task<ObservableCollection<OrderItems>> GetOrderItemsByOrderId(Order order)
        {

            var collection = new ObservableCollection<OrderItems>();
            var items = (await GetOrderItems()).Where(p => p.id_order == order.id_order).ToList();
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
        public async Task<bool> AddOrderItems(Basket basket)
        {

            var orders = await new OrderService().GetOrder();

            await client.Child("OrderItems").PostAsync(new OrderItems()
            {
               id_order = orders.Count,
               imgFood = basket.imgFood,
               Name = basket.Name,
               price = basket.price,
               count = basket.count
              

            });
            return true;
        }
    }
}
