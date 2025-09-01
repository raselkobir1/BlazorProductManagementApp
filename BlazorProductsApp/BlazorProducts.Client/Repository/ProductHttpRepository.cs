using Entities.Models;
using System.Text.Json;

namespace BlazorProducts.Client.Repository
{
    public class ProductHttpRepository : IProductHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public ProductHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Product>> GetProducts()
        {
            var response = await _client.GetAsync("Products/get-products");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var products = JsonSerializer.Deserialize<List<Product>>(content, _options);
            return products;
        }
    }
}
