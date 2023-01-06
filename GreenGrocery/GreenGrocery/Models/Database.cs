using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GreenGrocery.Models
{
    public class Database
    {
        readonly string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                cnn.CreateTable<User>();
                cnn.CreateTable<Product>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUserLoggedIn()
        {
            var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
            return cnn.Table<User>().FirstOrDefault();
        }

        public bool UserLogin(User user)
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                cnn.Insert(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UserLogout()
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                cnn.DeleteAll<User>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Product> GetCartItems()
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                return cnn.Table<Product>().ToList();
                
            }
            catch
            {
                return null;
            }
        }

        public Product GetACartItem(int id)
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                return cnn.Table<Product>().Where(product => product.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }


        public bool RemoveAnItemInCart(Product product)
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                cnn.Delete(product);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCart(Product product)
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                cnn.Update(product);
                return true;
            } catch
            {
                return false;
            }
        }

        public bool AddToCart(Product product)
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                cnn.Insert(product);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckItemExistedInCart(Product product)
        {
            try {
                return product.Id > 0;
            }catch
            {
                return false;
            }
        }

        public bool RemoveCart()
        {
            try
            {
                var cnn = new SQLiteConnection(System.IO.Path.Combine(path, "ecommerce_store.db"));
                cnn.DeleteAll<Product>();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
