using System.Collections.Generic;
using System.IO;
using CasaDoCodigo.Models;
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
            this._contexto = contexto;
            this._produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            _contexto.Database.Migrate();
            List<Produto> produtos = new List<Produto>();
            List<Livro> livros = GetLivros();

            foreach (Livro livro in livros)
            {
                produtos.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
            }
            
            _produtoRepository.AddAll(produtos);
        }

        private static List<Livro> GetLivros()
        {
            var livrosJson = File.ReadAllText("livros.json");

            return JsonConvert.DeserializeObject<List<Livro>>(livrosJson);
        }
    }
}
