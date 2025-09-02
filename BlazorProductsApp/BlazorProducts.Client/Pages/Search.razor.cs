using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class Search
    {
        private Timer _timer;
        public string? SearchTerm { get; set; }

        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        // Below field and function is used for every time not http api call the server. Debouncing Technique
        //কিন্তু Timer থাকলে, ইউজার টাইপ করা শেষ করার ৫০০ মিলিসেকেন্ড পরে একবারই API কল হবে। otherwise api call every single key pres/type.
        private void SearchChanged()
        {
            if (_timer != null)
                _timer.Dispose();
            _timer = new Timer(OnTimerElapsed, null, 500, 0);
        }
        private void OnTimerElapsed(object sender)
        {
            OnSearchChanged.InvokeAsync(SearchTerm);
            _timer.Dispose();
        }
    }
}
