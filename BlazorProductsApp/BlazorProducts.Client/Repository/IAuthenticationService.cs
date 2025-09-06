using Entities.Dtos;

namespace BlazorProducts.Client.Repository
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
