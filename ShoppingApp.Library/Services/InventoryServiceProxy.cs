using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Library.DTO;

namespace ShoppingApp.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ProductDTO> products;

        public ReadOnlyCollection<ProductDTO> Products
        {
            get
            {
                return products.AsReadOnly();
            }
        }

        private InventoryServiceProxy()
        {
            products = new List<ProductDTO>();
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance!;
            }
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            await Task.Delay(100); // Simulate async delay
            return products;
        }

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            await Task.Delay(100); // Simulate async delay

            var existingProduct = products.FirstOrDefault(prod => prod.Id == p.Id);

            if (existingProduct != null)
            {
                // Update existing product
                existingProduct.Name = p.Name;
                existingProduct.Price = p.Price;
                existingProduct.MarkdownPercentage = p.MarkdownPercentage;
            }
            else
            {
                // Add new product
                if (!products.Any())
                {
                    p.Id = 1; // If no products exist, start with ID 1
                }
                else
                {
                    int newId = products.Max(prod => prod.Id) + 1; // Generate a new unique ID
                    p.Id = newId;
                }
                products.Add(p);
            }

            return p;
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            await Task.Delay(100); // Simulate async delay

            var productToDelete = products.FirstOrDefault(p => p.Id == id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }

            return productToDelete;
        }

        public async Task<IEnumerable<ProductDTO>> Search(Query? query)
        {
            await Task.Delay(100); // Simulate async delay

            if (query == null || string.IsNullOrEmpty(query.QueryString))
            {
                return products;
            }

            // Simulate filtering products based on query
            return products.Where(p => p.Name.Contains(query.QueryString));
        }
    }
}