using GreenGrocery.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenGrocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        Database db;
        public Login()
        {
            InitializeComponent();
        }
        private async void LoginClicked(object sender, EventArgs e)
        {
            db = new Database();
            HttpClient http = new HttpClient();
            User loginInfo = new User();
            loginInfo.Username = username.Text.ToString();
            loginInfo.Password = password.Text.ToString();

            string json = JsonConvert.SerializeObject(loginInfo);

            // Convert PascalCase to CamelCase
            var myEvent = JsonConvert.DeserializeObject<ExpandoObject>(json); // Deserialize as ExpandoObject
            var camelSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var endJson = JsonConvert.SerializeObject(myEvent, camelSettings);

            //Send json
            StringContent content = new StringContent(endJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var res = await http.PostAsync("https://grocery-store-api.onrender.com/login", content);

            //Read response as string
            var data = await res.Content.ReadAsStringAsync();
            
            //Deserialize string to json
            User user = JsonConvert.DeserializeObject<User>(data);
            if (user != null)
            {
                bool isLogin = db.UserLogin(user);
                if (isLogin)
                {
                    var loginSuccessPage = new LoginSuccess();
                    loginSuccessPage.BindingContext = user;
                    Navigation.RemovePage(this);
                    App.Current.MainPage = new PageShell();
                }
            } else
            {
                await DisplayAlert("Error", "Login failed", "OK");
            }

        }
        private void RegisterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }
        
    }
}