using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using testXamarin.Models;
using testXamarin.ViewModels;

namespace testXamarin.Services
{
    public class ApiService
    {
        readonly HttpClient client;
        public ApiService()
        {
            client = new HttpClient();
        }

        public async Task<bool> Login(UserInfoViewModel vm)
        {
            var uri = new Uri(Constants.LOGIN_URL);

            var json = JsonConvert.SerializeObject(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var tokenVM = JsonConvert.DeserializeObject<TokenViewModel>(data);


                var db = new SQLiteConnection(Constants.SqlPath);

                db.CreateTable<Token>();

                db.Insert(new Token { UserToken = tokenVM.Token });

                return true;
            }
            else
            {
                return false;
            }
        }



        public async Task<List<AddTodoViewModel>> GetAllTodos()
        {
            var uri = new Uri(Constants.ALL_TODO_URL);

            var db = new SQLiteConnection(Constants.SqlPath);

           var tokenObj = db.Table<Token>().LastOrDefault();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenObj.UserToken);
            var response = await client.GetAsync(uri);
            

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var todoList = JsonConvert.DeserializeObject<List<AddTodoViewModel>>(data);

                return todoList;
            }
            else
            {
                return null;
            }


        }
        public async Task<AddTodoViewModel> AddNewTodoItem(AddTodoViewModel vm)
        {
            var uri = new Uri(Constants.ADD_TODO_URL);

            var json = JsonConvert.SerializeObject(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var todo = JsonConvert.DeserializeObject<AddTodoViewModel>(data);
                return todo;
            }
            else
            {
                return null;
            }
        }
    }
}
