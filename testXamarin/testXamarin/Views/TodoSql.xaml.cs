using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoSql : ContentPage
    {
        public ObservableCollection<Todo> AllTodo { get; set; }
        
        public TodoSql()
        {
            InitializeComponent();

            var db = new SQLiteConnection(Constants.SqlPath);
            //db.CreateTable<Todo>();

            AllTodo = new ObservableCollection<Todo>(db.Table<Todo>().ToList());
            foreach (var item in AllTodo)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }

            AllTodo.CollectionChanged += AllTodo_CollectionChanged;
            
            BindingContext = this;

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
            var db = new SQLiteConnection(Constants.SqlPath);

            db.UpdateAll(AllTodo);
        }

        private async void btnSubmitTodo_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTodoText.Text))
            {
                await DisplayAlert("Error", "Enter todo item.", "OK");
                return;
            }

            var db = new SQLiteConnection(Constants.SqlPath);
            db.CreateTable<Todo>();

            var todo = new Todo()
            {
                Done = true,
                TodoText = txtTodoText.Text,
                TodoTime = txtTime.Time.ToString()
            };

            db.Insert(todo);

            AllTodo.Add(todo);

            txtTodoText.Text = "";

            //BindingContext = AllTodo;
            await DisplayAlert("Success", $"Added Successfully", "OK");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("m", AllTodo.First().Done.ToString(), "OK");
        }
    }
}