using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adder : ContentPage
    {
        public Adder()
        {
            InitializeComponent();
        }

        private void calculate_Clicked(object sender, EventArgs e)
        {
            int number1;

            if (!int.TryParse(num1.Text, out number1))
            {
                answerLabel.Text = "Please enter numbers only!";
                answerLabel.TextColor = Color.Red;
                return;
            }

            int number2;

            if (!int.TryParse(num2.Text, out number2))
            {
                answerLabel.Text = "Please enter numbers only!";
                answerLabel.TextColor = Color.Red;
                return;
            }

            var answer = number1 + number2;

            answerLabel.Text = $"Sum of {number1} and {number2} is {answer}";
            answerLabel.TextColor = Color.Green;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            StartAnimation((Button)sender);
        }

        private async void StartAnimation(Button btn)
        {
            await Task.Delay(200);
            await btn.FadeTo(0, 250);
            await Task.Delay(200);
            await btn.FadeTo(1, 250);
        }
    }
}