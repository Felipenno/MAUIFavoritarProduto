using AppProduto.Models;
using AppProduto.Services;

namespace AppProduto.Pages;

public partial class LoginPage : ContentPage
{
	private LoginService _loginService;
	public LoginPage()
	{
		InitializeComponent();

        if (!LoginService.TokenExpirado())
        {
           Shell.Current.GoToAsync("ProdutosPage");
        }

        _loginService = new LoginService();

        btnLogin.IsEnabled = false;
        loginNome.TextChanged += (e, s) => DesabilitarBotao();
        loginSenha.TextChanged += (e, s) => DesabilitarBotao();
    }

    void OnLogin(object sender, EventArgs e)
	{
        _loginService.LogarUsuario(new Login(loginNome.Text, loginSenha.Text));
    }

    private void DesabilitarBotao()
    {
        btnLogin.IsEnabled = !(string.IsNullOrEmpty(loginNome.Text) || string.IsNullOrWhiteSpace(loginNome.Text))
                           && !(string.IsNullOrEmpty(loginSenha.Text) || string.IsNullOrWhiteSpace(loginSenha.Text));
    }
}