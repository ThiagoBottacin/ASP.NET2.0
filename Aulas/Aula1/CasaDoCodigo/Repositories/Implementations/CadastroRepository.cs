using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public class CadastroRepository : RepositoryBase<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext contexto) : base(contexto)
        {
        }
    }
}
