using HiFlyerClassLibrary.Models.Authentication;
using System.Threading.Tasks;

namespace HiFlyerWebApp.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginModel loginModel);
        Task Logout();
    }
}