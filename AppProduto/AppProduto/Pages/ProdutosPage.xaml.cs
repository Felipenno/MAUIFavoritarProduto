using AppProduto.Models;
using AppProduto.Services;

namespace AppProduto.Pages;

public partial class ProdutosPage : ContentPage
{
	private ApiProdutos _apiProdutos;

    public ProdutosPage()
	{
		InitializeComponent();

        _apiProdutos = new ApiProdutos();

		OnGetProdutos();
    }

	public async void OnGetProdutos()
	{
		var produtos = await _apiProdutos.ObterProdutos();
		if(produtos == null || produtos.Count == 0)
		{
			return;
        }

        var favoritos = await App._FavoritoRepo.Listar();
        if(favoritos != null && favoritos.Count > 0)
        {
            produtos.ForEach(p =>
            {
                if(favoritos.Any(x => x.Id == p.id))
                {
                    p.cor = Colors.Yellow;
                }
            });
        }

        produtosList.ItemsSource = produtos;
    }

    private async void OnSelectId(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        var produto = btn.BindingContext as Produto;

        var favorito = new Favorito() { Id = produto.id };

        if (btn.TextColor == null || btn.TextColor == Colors.White)
        {
            await App._FavoritoRepo.Adicionar(favorito);
            btn.TextColor = Colors.Yellow;
        }
        else
        {
            await App._FavoritoRepo.Remover(favorito);
            btn.TextColor = Colors.White;
        }

    }
}