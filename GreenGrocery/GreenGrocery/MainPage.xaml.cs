using GreenGrocery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenGrocery
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void ActionClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
        private void Action1Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductsList());
        }
    }
}
