using System;
using testXamarin.Services;
using testXamarin.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testXamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
        }
    }
}
