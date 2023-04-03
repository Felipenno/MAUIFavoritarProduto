using AppProduto.Pages;

namespace AppProduto;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("ProdutosPage", typeof(ProdutosPage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
    }
}
