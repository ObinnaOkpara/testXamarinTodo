using SQLite;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService apiService;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            apiService = new ApiService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            var db = new SQLiteConnection(Constants.SqlPath);
            db.CreateTable<Token>();
         
            var lastToken =  db.Table<Token>().LastOrDefault();

            if (lastToken == null)
            {
                return;
            }
            else
            {
                var jwt = new JwtSecurityToken(lastToken.UserToken);

                if (jwt.ValidTo < DateTime.Now)
                {
                    return;
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }

            }


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var vm = new UserInfoViewModel { Email = Email.Text, Password = Password.Text };
           var result = await apiService.Login(vm);
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            if (result)
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
               await DisplayAlert("Error", "Please Provide valid details", "OK");
            }
            
        }

       
    }
}