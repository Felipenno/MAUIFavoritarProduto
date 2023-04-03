using AppProduto.Models;
using System.Text.Json;

namespace AppProduto.Services
{
    internal class ApiProdutos
    {
        private readonly string apiUrl = "http://10.0.2.2:5251/api/";
        private HttpClient _client;

        public async Task<List<Produto>> ObterProdutos()
        {
            try
            {
                if (LoginService.TokenExpirado())
                {
                    await Application.Current.MainPage.DisplayAlert("Login Expirado!", "Você sera levado a tela de login.", "Ok");

                    await Shell.Current.GoToAsync("LoginPage");
                    return new List<Produto>();
                }

                _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("token_app", LoginService.ObterToken());
                _client.DefaultRequestHeaders.Add("Accept", "application/json");

                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert("Não foi possivel se conectar", "Verifique se o dispositivo possui internet.", "Ok");
                    return new List<Produto>();
                }

                string result = await _client.GetStringAsync($"{apiUrl}getprodutos");
                return JsonSerializer.Deserialize<List<Produto>>(result);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Login Expirado!", "Você sera levado a tela de login.", "Ok");
                LoginService.RemoverToken();
                await Shell.Current.GoToAsync("LoginPage");
                return new List<Produto>();
            }            
        }
    }
}
