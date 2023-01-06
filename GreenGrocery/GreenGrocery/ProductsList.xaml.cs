using GreenGrocery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenGrocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsList : ContentPage
    {
        List<Product> products;
        readonly Database db;

        public async void GetProductsList(string keyword = "", Category category = null)
        {
            HttpClient http = new HttpClient();
            string url = "https://python-ecommerce-api.onrender.com/products";

            if (!string.IsNullOrEmpty(keyword)) {
                url += $"?search={keyword}&";
            }

            if (category != null)
            {
                url += $"?kind={category.Kind}";
            }

            var matchPattern = Regex.Escape("?");
            var replacePattern = string.Format("(?<={0}.*){0}", matchPattern);
            var regex = new Regex(replacePattern);
            url = regex.Replace(url, "");

            var res = await http.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<ResponseData>(res);
            List<Product> newProducts = data.Data;
            products = newProducts;

            products.ForEach(p =>
            {
                p.Quantity = 1;
                p.PriceMultiplyQuantity = p.Price;
            });
            productsListDisplay.ItemsSource = products;

            loading.IsRunning = false;
            loading.IsVisible = false;
            productsList.IsVisible = true;
        }

        public ProductsList()
        {
            InitializeComponent();
            db = new Database();
            GetProductsList();
        }

         public ProductsList(string keyword = "", Category category = null)
         {
            InitializeComponent();
            db = new Database();
            GetProductsList(keyword, category);
         }

        private void AddToCartClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Product item = (Product)btn.BindingContext;
            if (item != null)
            {
                bool isItemExistedInCart = db.CheckItemExistedInCart(item);
                if (isItemExistedInCart)
                {
                    Product newItem = db.GetACartItem(item.Id);
                    if (newItem != null)
                    {
                        newItem.Quantity += 1;
                        newItem.PriceMultiplyQuantity = newItem.Price * newItem.Quantity;

                        bool result = db.UpdateCart(newItem);
                        if (result)
                            DisplayAlert("Success", "Update " + item.Name.ToString() + " in cart successfully", "OK");
                        else
                            DisplayAlert("Error", "Update " + item.Name.ToString() + " in cart failed", "OK");
                    }
                } else
                {
                    bool result = db.AddToCart(item);
                    if (result)
                        DisplayAlert("Success", "Add " + item.Name.ToString() + " to cart successfully", "OK");
                    else
                        DisplayAlert("Error", "Add " + item.Name.ToString() + " to cart failed", "OK");
                }

            }
        }

        private void OnShowCart(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cart());
        }

        private void OnShowProductDetail(object sender, EventArgs e)
        {
            var evt = e as TappedEventArgs;
            var product = (Product)evt.Parameter;

            if (product.Uid > 0)
            {
                Navigation.PushAsync(new ProductDetail(product.Uid));
            }
        }
        private void OnSearchProduct(object sender, EventArgs e)
        {
            loading.IsVisible = true;
            loading.IsRunning = true;
            productsList.IsVisible = false;
            GetProductsList(searchKeyword.Text);
        }
    }
}