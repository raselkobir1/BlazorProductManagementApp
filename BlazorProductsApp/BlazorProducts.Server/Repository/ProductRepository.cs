using BlazorProducts.Server.Context;
using BlazorProducts.Server.Paging;
using BlazorProducts.Server.Repository.RepositoryExtensions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace BlazorProducts.Server.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts() => await _context.Products.ToListAsync();

        public async Task<PagedList<Product>> GetProducts(ProductParameters productParameters)
        {
            var products = await _context.Products
                .Search(productParameters.SearchTerm!)
                .Sort(productParameters.OrderBy)
                .ToListAsync();
            var productPageList = PagedList<Product>
                .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
            return productPageList;
        }
    }
}
