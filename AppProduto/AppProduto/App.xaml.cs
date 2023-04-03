using AppProduto.Repository;
using System.Globalization;

namespace AppProduto;

public partial class App : Application
{
    public static FavoritoRepository _FavoritoRepo { get; private set; }

    public App(FavoritoRepository repo)
	{
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

		InitializeComponent();

		MainPage = new AppShell();

        _FavoritoRepo = repo;

    }

}
