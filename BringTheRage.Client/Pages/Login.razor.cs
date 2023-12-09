using BringTheRage.Client.Services;
using BringTheRage.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BringTheRage.Client.Pages;

public partial class Login : ComponentBase {
  [Inject]
  private IAuthenticationService AuthenticationService { get; set; }
  [Inject]
  private NavigationManager NavigationManager { get; set; }

  public LoginModel loginModel = new LoginModel();
  public bool ShowErrors;
  public string Error = "";

  public async Task HandleLogin() {
    ShowErrors = false;

    var result = await AuthenticationService.Login(loginModel);

    if (result.Successful) {
      NavigationManager.NavigateTo("/");
    }
    else {
      Error = result.Error;
      ShowErrors = true;
    }
  }
}