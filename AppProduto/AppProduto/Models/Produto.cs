
namespace AppProduto.Models
{
    internal class Produto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; }
        public decimal valor { get; set; }
        public Color cor { get; set; } = Colors.White;
    }
}
