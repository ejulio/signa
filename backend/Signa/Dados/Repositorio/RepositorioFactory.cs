using Signa.Domain.Sinais.Estatico;

namespace Signa.Dados.Repositorio
{
    public class RepositorioFactory : IRepositorioFactory
    {
        private readonly string caminhoDoArquivoDeDados;

        private static IRepositorio<SinalEstatico> repositorioDeSinaisEstaticos; 

        public RepositorioFactory(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
        }

        public IRepositorio<SinalEstatico> CriarECarregarRepositorioDeSinaisEstaticos()
        {
            if (repositorioDeSinaisEstaticos == null)
            {
                repositorioDeSinaisEstaticos = new RepositorioSinaisEstaticos(caminhoDoArquivoDeDados);
                repositorioDeSinaisEstaticos.Carregar();    
            }
            
            return repositorioDeSinaisEstaticos;
        }
    }
}