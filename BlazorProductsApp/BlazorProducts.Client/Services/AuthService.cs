namespace BlazorProducts.Client.Services
{
    public class AuthService
    {
        private bool _isLoggedIn = false;

        public bool IsLoggedIn => _isLoggedIn;

        public void Login()
        {
            _isLoggedIn = true;
        }

        public void Logout()
        {
            _isLoggedIn = false;
        }
    }
}
