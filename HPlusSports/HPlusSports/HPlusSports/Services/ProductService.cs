using System;
using System.Collections.Generic;
using System.IO;

using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HPlusSports.Services
{
	public static class ProductService
    {
        private const string WISHLIST_FILE = "wishlist.json";
		static HttpClient client;

        private static List<Product> WishList
        {
            get;
            set;
        }

        public static void AddFavorite(Product p)
        {
            // Main create new product objects every time it's opened
            // Therefore we need to check on something else than just List.Contains
            if (WishList.Find(pr=> pr.Id == p.Id) == null)
            {
                WishList.Add(p);
            }
        }

        public static void ClearFavorites()
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, WISHLIST_FILE);
            File.Delete(path);
        }

        public static List<Product> GetFavorites()
        {
            // Return a copy of the list to make sure it doesn't get tampered with
            return new List<Product>(WishList);
        }

        static ProductService()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri("https://hplussport.com/api/");

		}

        public static async Task<List<Product>> GetProductsAsync()
		{
            var productsRaw = await client.GetStringAsync("products/");

            var serializer = new JsonSerializer();
            using(var tReader = new StringReader(productsRaw))
            {
                using(var jReader = new JsonTextReader(tReader))
                {
                    var products = serializer.Deserialize<List<Product>>(
                        jReader);

                    return products;
                }
            }
		}

        public static async Task SaveWishList()
        {
            if (WishList != null && WishList.Count > 0)
            {
                //Save Products to Wish List
                string path = Path.Combine(
                    // Use Xamarin.Essential to find the platform specific path
                    FileSystem.AppDataDirectory, WISHLIST_FILE);

                using (StreamWriter sWriter = new StreamWriter(path))
                {
                    using (var jWriter = new JsonTextWriter(sWriter))
                    {
                        JsonSerializer.CreateDefault().Serialize(jWriter, WishList);
                    }
                }
            }
        }

        public static async Task LoadWishList()
        {
            //Load items from wish list
            WishList = new List<Product>();
            
            string path = Path.Combine(
                // Use Xamarin.Essential to find the platform specific path
                FileSystem.AppDataDirectory, WISHLIST_FILE);

            if (File.Exists(path))
            {
                using (StreamReader sReader = new StreamReader(path))
                {
                    using (var jReader = new JsonTextReader(sReader))
                    {
                        try
                        {
                            WishList = JsonSerializer.CreateDefault().Deserialize<List<Product>>(jReader);
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e.Message);
                        }

                    }
                }
            }

        }
    }
}
