using Entities.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class ProductTable
    {
        [Parameter]
        public List<Product> Products { get; set; } = new();
        [Parameter]
        public EventCallback<Guid> OnDeleted { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        private void RedirectToUpdate(Guid id)
        {
            var url = Path.Combine("/updateProduct/", id.ToString());
            NavigationManager.NavigateTo(url);
        }
    }
}
