using System.Collections.Generic;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        void SaveProdutos(List<Livro> livros);
    }
}