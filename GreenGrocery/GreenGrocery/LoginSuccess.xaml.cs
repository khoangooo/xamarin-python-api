using GreenGrocery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenGrocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginSuccess : ContentPage
    {
        readonly Database db;
        public LoginSuccess()
        {
            InitializeComponent();
            db = new Database();
            User userLogin = db.GetUserLoggedIn();
            this.BindingContext = userLogin;
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            bool logout = db.UserLogout();
            if (logout)
            {
                Navigation.RemovePage(new LoginSuccess());
                App.Current.MainPage = new PageShell();
            }
        }
    }
}