using System.ComponentModel;
using testXamarin.ViewModels;
using Xamarin.Forms;

namespace testXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}