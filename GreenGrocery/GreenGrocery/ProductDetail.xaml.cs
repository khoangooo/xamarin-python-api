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
            var resProduct = await http.GetStringAsync("https://python-ecommerce-api.onrender.com/products/" + productId.ToString());
            if (resProduct == null) return;
            Product product = JsonConvert.DeserializeObject<Response<Product>>(resProduct).Data;
            
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