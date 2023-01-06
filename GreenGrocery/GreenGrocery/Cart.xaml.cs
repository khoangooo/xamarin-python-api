using GreenGrocery.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace GreenGrocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        readonly Database db;
        string str;
        void InitCart()
        {
            List<Product> products = db.GetCartItems();
            ProductsCart newCart = new ProductsCart();
            newCart.TotalPrice = 0;
            newCart.TotalQuantity = 0;
            newCart.Items = new List<Product>();
            str = "";
            if (products != null)
            {
                products.ForEach(item =>
                {
                    newCart.TotalPrice += item.PriceMultiplyQuantity;
                    newCart.TotalQuantity += (int)item.Quantity;
                    newCart.Items.Add(item);
                    string strMoney = string.Format("${0:#.00}", Convert.ToDecimal(item.PriceMultiplyQuantity));
                    str += $"{item.Name} - quantity:{item.Quantity} - total price:{strMoney}\n";
                });

                str += $"Subtotal: {string.Format("${0:#.00}", Convert.ToDecimal(newCart.TotalPrice))}\n";
                this.BindingContext = newCart;
                cart.ItemsSource = newCart.Items;
            }
        }

        public Cart()
        {
            InitializeComponent();
            db = new Database();
            InitCart();
        }

        private void RemoveCartItem(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Product item = (Product)btn.BindingContext;
            bool result = db.RemoveAnItemInCart(item);
            if (result)
            {
                DisplayAlert("Success", "Remove successful", "OK");
                InitCart();
            }
            else
                DisplayAlert("Error", "Remove failed", "OK"); 
        }

        private void OnRecalculatePrice(object sender, EventArgs e)
        {
            Product cartItem = (Product)((Entry)sender).BindingContext;
            var item = ((Entry)sender);
            int quantity = 0;

            if (!string.IsNullOrEmpty(item.Text))
            {
                quantity = int.Parse(item.Text);
            }

            cartItem.Quantity = quantity;
            cartItem.PriceMultiplyQuantity = quantity * cartItem.Price;

            db.UpdateCart(cartItem);
            InitCart();
        }
        private async void CheckoutCart(object sender, EventArgs e)
        {
            await DisplayAlert("Success", $"Hi, {customerName.Text}.\nYour cart is: \n{str}\nWe will delivery to you soon <3", "OK");
            bool result = db.RemoveCart();
            if (result) await Navigation.PushAsync(new ProductsList());
        }
    }
}