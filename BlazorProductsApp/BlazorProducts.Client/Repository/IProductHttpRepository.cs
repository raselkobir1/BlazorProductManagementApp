using Entities.Models;

namespace BlazorProducts.Client.Repository
{
    public interface IProductHttpRepository
    {
        Task<List<Product>> GetProducts();
    }
}
