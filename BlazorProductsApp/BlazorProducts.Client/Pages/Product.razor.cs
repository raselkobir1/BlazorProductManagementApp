using BlazorProducts.Client.Repository;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Components;
namespace BlazorProducts.Client.Pages
{
    public partial class Product
    {
        public List<Entities.Models.Product> ProductList { get; set; } = new List<Entities.Models.Product>();
        public MetaData MetaData { get; set; } = new MetaData();
        private ProductParameters _productParameters = new ProductParameters();

        [Inject]
        public required IProductHttpRepository ProductRepo { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await GetProducts();
        }

        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProducts();
        }
        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _productParameters.PageNumber = 1;
            _productParameters.SearchTerm = searchTerm;
            await GetProducts();
        }
        private async Task GetProducts()
        {
            //ProductList = await ProductRepo.GetProducts(); // without paging

            var pagingResponse = await ProductRepo.GetProducts(_productParameters);
            ProductList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _productParameters.OrderBy = orderBy;
            await GetProducts();
        }
    }
}
