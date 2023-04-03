using AppProduto.Models;
using Microsoft.Win32.SafeHandles;
using SQLite;


namespace AppProduto.Repository
{
    public class FavoritoRepository
    {
        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
            {
                return;
            }

            var path = Path.Combine(FileSystem.AppDataDirectory, "favoritos.db3");
            conn = new SQLiteAsyncConnection(path);
            //await conn.DropTableAsync<Favorito>();
            await conn.CreateTableAsync<Favorito>();
        }

        public async Task Adicionar(Favorito favorito)
        {
            try
            {
                await Init();
                await conn.InsertAsync(favorito);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public async Task Remover(Favorito favorito)
        {
            try
            {
                await Init();
                await conn.DeleteAsync(favorito);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public async Task<List<Favorito>> Listar()
        {
            try
            {
                await Init();

                return await conn.Table<Favorito>().ToListAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return new List<Favorito>();
            }
        }
    }
}
