using BringTheRage.Domain.Models;

namespace BringTheRage.Client.Services;

public interface IAuthenticationService {
  Task<LoginResult> Login(LoginModel loginModel);
  Task Logout();
  Task<RegisterResult> Register(RegisterModel registerModel);
}
