using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class ProductTable
    {
        [Parameter]
        public List<Entities.Models.Product> Products { get; set; }
    }
}
