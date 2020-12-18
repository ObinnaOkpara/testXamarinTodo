using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testXamarin.Models;
using testXamarin.Services;
using testXamarin.ViewModels;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AllTodo = new ObservableCollection<Todo>();
            AllTodo.CollectionChanged += AllTodo_CollectionChanged;
            BindingContext = this;

            Task.Run(async ()=> {

                var data = await ApiService.GetAllTodos();

                Device.BeginInvokeOnMainThread(() => {

                    if (data.Any())
                    {
                        var todos = ((data).Select(x => new Todo
                        {
                            Done = x.IsDone,
                            Id = x.Id,
                            TodoText = x.TodoDescription,
                            TodoTime = x.TodoTime
                        }));

                        foreach (var item in todos)
                        {
                            //item.PropertyChanged += OnItemPropertyChanged;
                            AllTodo.Add(item);

                        }

                    }

                });

            });
        }

        private void AllTodo_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (e.NewItems != null)
            {
                foreach (Todo newItem in e.NewItems)
                {
                    //Add listener for each item on PropertyChanged event
                    newItem.PropertyChanged += this.OnItemPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (Todo oldItem in e.OldItems)
                {
                    oldItem.PropertyChanged -= this.OnItemPropertyChanged;
                }
            }

        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //var db = new SQLiteConnection(Constants.SqlPath);

            //db.UpdateAll(AllTodo);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTodoText.Text))
            {
                await DisplayAlert("Error", "Enter todo item.", "OK");
                return;
            }


            var todo = new AddTodoViewModel()
            {
                 IsDone = false,
                TodoDescription = txtTodoText.Text,
                 TodoTime = txtTime.Time.ToString()
            };

           var result = await  ApiService.AddNewTodoItem(todo);

            if (result == null)
            {
                await DisplayAlert("Error", "Error occured while posting data", "OK");
                return;
            }
            else
            {
                AllTodo.Add(new Todo
                {
                    TodoTime = todo.TodoTime,
                    Done = todo.IsDone,
                    TodoText = todo.TodoDescription,
                    Id = todo.Id
                });

                txtTodoText.Text = "";

                //BindingContext = AllTodo;
                await DisplayAlert("Success", $"Added Successfully", "OK");
            }

        }
    }
}