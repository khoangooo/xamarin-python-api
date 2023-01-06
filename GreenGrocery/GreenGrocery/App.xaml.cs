using GreenGrocery.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenGrocery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Database db = new Database();
            db.CreateDatabase();
            MainPage = new PageShell();
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
