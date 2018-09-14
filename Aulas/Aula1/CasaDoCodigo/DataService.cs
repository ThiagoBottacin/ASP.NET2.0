using System.Collections.Generic;
using System.IO;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _contexto;
        private readonly IProdutoRepository _produtoRepository;

        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            _contexto = contexto;
            _produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            _contexto.Database.Migrate();
            
            List<Livro> livros = GetLivros();

            _produtoRepository.SaveProdutos(livros);
        }

        private static List<Livro> GetLivros()
        {
            var livrosJson = File.ReadAllText("livros.json");

            return JsonConvert.DeserializeObject<List<Livro>>(livrosJson);
        }
    }
}
