using Firebase.Database;
using Picca.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picca.Services
{
    class BasketService
    {
        FirebaseClient client;
        public BasketService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
       
    }
}
