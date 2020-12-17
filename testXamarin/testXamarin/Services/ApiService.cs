using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using testXamarin.ViewModels;

namespace testXamarin.Services
{
    public class ApiService
    {
        HttpClient client;
        private const string BASE_URL = "";
        public ApiService()
        {
            client = new HttpClient();
        }

        public async Task<List<AddTodoViewModel>> GetAllTodos()
        {
            var uri = new Uri(Constants.ALL_TODO_URL);

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
