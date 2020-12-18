using System;
using System.Collections.Generic;
using testXamarin.ViewModels;
using testXamarin.Views;
using Xamarin.Forms;

namespace testXamarin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute("Calc", typeof(Adder));
            Routing.RegisterRoute("Todosql", typeof(TodoSql));
            Routing.RegisterRoute("TodoApi", typeof(TodoApi));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
