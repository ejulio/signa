using Signa.Dominio.Sinais;

namespace Signa.Dados.Repositorio
{
    public class RepositorioFactory : IRepositorioFactory
    {
        private readonly string caminhoDoArquivoDeDados;

        private static IRepositorio<Sinal> repositorioDeSinaisEstaticos; 

        public RepositorioFactory(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisEstaticos()
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