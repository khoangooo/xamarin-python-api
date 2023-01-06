using GreenGrocery.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;

namespace GreenGrocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }
        private void LoginClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
        private async void RegisterClicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            string confirm = confirmPassword.Text.ToString();
            string pw = password.Text.ToString();
            string em = email.Text.ToString();
            if (confirm != pw)
            {
                await DisplayAlert("Error", "The password and confirmation password do not match", "OK");
                return;
            }

            if (!string.IsNullOrEmpty(em) && !new Regex("^\\S+@\\S+\\.\\S+$").IsMatch(em))
            {
                await DisplayAlert("Error", "Wrong email pattern", "OK");
                return;
            }

            User registerInfo = new User();
            registerInfo.Username = username.Text.ToString();
            registerInfo.Password = pw;
            registerInfo.ConfirmPassword = confirm;
            registerInfo.Phone = phone.Text.ToString();
            registerInfo.Email = em;
            string json = JsonConvert.SerializeObject(registerInfo);

            // Convert PascalCase to CamelCase
            var myEvent = JsonConvert.DeserializeObject<ExpandoObject>(json); // Deserialize as ExpandoObject
            var camelSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var endJson = JsonConvert.SerializeObject(myEvent, camelSettings);

            //Send json
            StringContent content = new StringContent(endJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var res = await http.PostAsync("https://python-ecommerce-api.onrender.com/sign_up", content);

            //Read response as string
            var data = await res.Content.ReadAsStringAsync();

            //Deserialize string to json
            var signupData = JsonConvert.DeserializeObject<ResponseSignup>(data);
            string message = signupData.Message;

            if (!signupData.Status)
            {
                await DisplayAlert("Error", message, "OK");
            }
            await Navigation.PushAsync(new Login());
        }
    }
}