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

        public async Task CreateProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product, Product dbProduct)
        {
            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.Supplier = product.Supplier;

            await _context.SaveChangesAsync();
        }
        public async Task<Product> GetProduct(Guid id) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));

        public async Task DeleteProduct(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
