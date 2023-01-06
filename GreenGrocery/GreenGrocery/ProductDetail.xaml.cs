using GreenGrocery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenGrocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        void BindProduct(Product product)
        {
            if (product != null)
            {
                this.BindingContext = product;
            }
        }

        public async void GetProductDetail(int productId)
        {
            HttpClient http = new HttpClient();
            var resProduct = await http.GetStringAsync("https://grocery-store-api.onrender.com/products/" + productId.ToString());
            if (resProduct == null) return;

            var resCategories = await http.GetStringAsync("https://grocery-store-api.onrender.com/products/categories");

            Product product = JsonConvert.DeserializeObject<Product>(resProduct);

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(resCategories);
            
            foreach (Category category in categories)
            {
                if (product.Kind == category.Kind)
                {
                    product.Category = category.Name;
                }
            }
            
            BindProduct(product);
            loading.IsRunning = false;
            loading.IsVisible = false;
            productDetail.IsVisible = true;
        }


        public ProductDetail()
        {
            InitializeComponent();
        }

        public ProductDetail(int productId)
        {
            InitializeComponent();
            GetProductDetail(productId); 
        }
    }
}