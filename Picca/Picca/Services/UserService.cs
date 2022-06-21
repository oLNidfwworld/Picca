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
    class UserService
    {

        FirebaseClient client;
        public UserService()
        {
            client = new FirebaseClient("https://piccad30-default-rtdb.firebaseio.com/");

        }
        public async Task<List<Users>> GetUsers()
        {
            var users = (await client.Child("Users").OnceAsync<Users>())
                .Select(c => new Users
                {
                    id_user = c.Object.id_user,
                    Login = c.Object.Login,
                    Password = c.Object.Password,
                    Name = c.Object.Name,
                    PhoneNumber = c.Object.PhoneNumber


                }).ToList();
            return users;

        }
        public async Task<bool> RegisterUser(string login, string password, string name, string phone)
        {

            var users = (await client.Child("Users").OnceAsync<Users>())
                .Select(c => new Users
                {
                    id_user = c.Object.id_user,
                    Login = c.Object.Login,
                    Password = c.Object.Password,
                    Name = c.Object.Name,
                    PhoneNumber = c.Object.PhoneNumber


                }).ToList();
            int count = users.Count();
            if (await IsUserExists(login) == false)
            {
                await client.Child("Users").PostAsync(new Users()
                {
                    Login = login,
                    Password = password,
                    Name = name,
                    PhoneNumber = phone,
                    id_user = count++
                });
                return true;

            }
            else
            {
                return false;
            }
        }
        public async Task<bool> IsUserExists(string login)
        {
            var user = (await client.Child("Users").OnceAsync<Users>()).Where(u => u.Object.Login == login).FirstOrDefault();
            return user != null;
        }
        public async Task<bool> LoginUser(string login, string password)
        {
            var user = (await client.Child("Users").OnceAsync<Users>()).Where(u => u.Object.Login == login)
                .Where(u => u.Object.Password == password).FirstOrDefault();
            return (user != null);
        }
        public async Task<Users> GetUserByLogin(string userlogin)
        {
            var item = (await GetUsers()).Where(p => p.Login == userlogin).FirstOrDefault();
           
            return item;
        }
    }
}
