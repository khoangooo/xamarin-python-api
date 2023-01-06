﻿using GreenGrocery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace GreenGrocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageShell : Xamarin.Forms.Shell
    {
        readonly Database db;

        public async void InitPage()
        {
            HttpClient http = new HttpClient();
            var resCategories = await http.GetStringAsync("https://grocery-store-api.onrender.com/products/categories");

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(resCategories);

            User user = db.GetUserLoggedIn();

            FlyoutItem flyoutItemLogin;
            FlyoutItem flyoutItemLoginSuccess;
            FlyoutItem flyoutItemProducts = new FlyoutItem{ Title = "Products"};

            flyoutItemProducts.Items.Add(new ShellContent { Content = new ProductsList() });

            if (user != null)
            {
                flyoutItemLoginSuccess = new FlyoutItem{ Title = "User Info"};
                var loginSuccessPage = new LoginSuccess();
                loginSuccessPage.BindingContext = user;
                flyoutItemLoginSuccess.Items.Add(new ShellContent { Content = new LoginSuccess() });
                this.Items.Add(flyoutItemLoginSuccess);
            }
            else
            {
                flyoutItemLogin = new FlyoutItem { Title = "Login" };
                flyoutItemLogin.Items.Add(new ShellContent { Content = new Login() });
                this.Items.Add(flyoutItemLogin);
            }

            this.Items.Add(flyoutItemProducts);

            categories.ForEach(category => {
                FlyoutItem flyoutItem = new FlyoutItem { Title = category.Name };
                flyoutItem.Items.Add(new ShellContent { Content = new ProductsList(null, category) });
                this.Items.Add(flyoutItem);
            });
        }

        public PageShell()
        {
            InitializeComponent();
            db = new Database();
            InitPage();
        }
    }
}