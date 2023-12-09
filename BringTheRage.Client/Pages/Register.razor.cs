using BringTheRage.Client.Services;
using BringTheRage.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BringTheRage.Client.Pages;

public partial class Register : ComponentBase {
  [Inject]
  public IAuthenticationService AuthenticationService { get; set; }
  [Inject]
  public NavigationManager NavigationManager { get; set; }

  public RegisterModel RegisterModel = new RegisterModel();
  public bool ShowErrors;
  public IEnumerable<string> Errors;

  public async Task HandleRegistration() {
    ShowErrors = false;

    var result = await AuthenticationService.Register(RegisterModel);

    if (result.Successful) {
      NavigationManager.NavigateTo("/login");
    }
    else {
      Errors = result.Errors;
      ShowErrors = true;
    }
  }
}