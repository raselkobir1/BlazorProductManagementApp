using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorProducts.Client.Pages
{
    public partial class ProductTable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
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
        private async Task Delete(Guid id)
        {
            var product = Products.FirstOrDefault(p => p.Id.Equals(id));

            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {product.Name} product?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }
    }
}
