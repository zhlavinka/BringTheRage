﻿using Blazored.LocalStorage;
using BringTheRage.Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace BringTheRage.Client.Services;

public class AuthenticationService : IAuthenticationService {
  private readonly HttpClient _httpClient;
  private readonly AuthenticationStateProvider _authenticationStateProvider;
  private readonly ILocalStorageService _localStorage;

  public AuthenticationService(
    HttpClient httpClient,
    AuthenticationStateProvider authenticationStateProvider,
    ILocalStorageService localStorage
  ) {
    _httpClient = httpClient;
    _authenticationStateProvider = authenticationStateProvider;
    _localStorage = localStorage;
  }

  public async Task<RegisterResult> Register(RegisterModel registerModel) {
    var response = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);
    var result = JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    return result;
  }

  public async Task<LoginResult> Login(LoginModel loginModel) {
    var loginAsJson = JsonSerializer.Serialize(loginModel);
    var response = await _httpClient.PostAsync("api/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
    var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (!response.IsSuccessStatusCode) {
      return loginResult;
    }

    await _localStorage.SetItemAsync("authToken", loginResult.Token);
    ((HostAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

    return loginResult;
  }

  public async Task Logout() {
    await _localStorage.RemoveItemAsync("authToken");
    ((HostAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
    _httpClient.DefaultRequestHeaders.Authorization = null;
  }
}