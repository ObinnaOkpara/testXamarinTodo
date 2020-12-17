using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testXamarin.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void btnAdder_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"Calc");
        }

        private async void btnTodoSql_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"Todosql");
        }
    }
}