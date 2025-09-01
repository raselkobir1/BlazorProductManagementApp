using BlazorProducts.Server.Entities;

namespace BlazorProducts.Server.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}
