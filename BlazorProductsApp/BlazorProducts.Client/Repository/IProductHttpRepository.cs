using BlazorProducts.Client.Features;
using Entities.Models;
using Entities.RequestFeatures;

namespace BlazorProducts.Client.Repository
{
    public interface IProductHttpRepository
    {
        Task<List<Product>> GetProducts();
        Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters);
    }
}
