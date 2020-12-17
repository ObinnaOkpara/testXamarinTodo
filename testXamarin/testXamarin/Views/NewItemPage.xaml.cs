using System;
using System.Collections.Generic;
using System.ComponentModel;
using testXamarin.Models;
using testXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testXamarin.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}