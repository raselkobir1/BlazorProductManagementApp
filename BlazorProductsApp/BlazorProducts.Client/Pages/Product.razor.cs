using BlazorProducts.Client.Repository;
using Microsoft.AspNetCore.Components;
namespace BlazorProducts.Client.Pages
{
    public partial class Product
    {
        public List<Entities.Models.Product> ProductList { get; set; } = new List<Entities.Models.Product>();
        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }
        protected async override Task OnInitializedAsync()
        {
            ProductList = await ProductRepo.GetProducts();
            //just for testing
            foreach (var product in ProductList)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
