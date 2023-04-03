using AppProduto.Models;
using System.Data;
using System.Globalization;
using System.Net.Http.Json;

namespace AppProduto.Services
{
    internal class LoginService
    {
        private readonly string apiUrl = "http://10.0.2.2:5251/api/";
        private HttpClient _client;

        public static string ObterToken()
        {
            return Preferences.Get("token_app", "");
        }

        public static void ArmazenarToken(string token)
        {
            Preferences.Set("token_app", token);
        }

        public static void RemoverToken()
        {
            Preferences.Remove("token_app");
        }

        public static bool TokenExpirado()
        {
            var token = ObterToken();

            if (string.IsNullOrEmpty(token) || !token.Contains('=')) 
            {
                RemoverToken();
                return true;
            }
            
            var expireDate = token.Split("=")[1].Replace("\"", "");
            var tokenExpireDate = DateOnly.Parse(expireDate);

            var tokenInvalido = tokenExpireDate < DateOnly.FromDateTime(DateTime.Now);
            if (tokenInvalido)
            {
                
            }

            return tokenInvalido;
        }

        public async void LogarUsuario(Login login)
        {
            try
            {
                _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("Accept", "application/json");

                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert("Não foi possivel se conectar", "Verifique se o dispositivo possui internet.", "Ok");
                    return;
                }

                var msg = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}login");
                msg.Content = JsonContent.Create(login);

                var response = await _client.SendAsync(msg);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                
                ArmazenarToken(result.Trim('"'));
                await Shell.Current.GoToAsync("ProdutosPage");
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Credenciais Invalidas", "Tente novamente", "Ok");
            }

            
        }
    }
}
