using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testXamarin.Models;
using testXamarin.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoApi : ContentPage
    {
        public ObservableCollection<Todo> AllTodo { get; set; }
        private readonly ApiService ApiService;

        public TodoApi()
        {
            InitializeComponent();

            ApiService = new ApiService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            AllTodo = new ObservableCollection<Todo>((await ApiService.GetAllTodos()).Select(x => new Todo
            {
                Done = x.IsDone,
                Id = x.Id,
                TodoText = x.TodoDescription,
                TodoTime = x.TodoTime
            }));
        }
    }
}