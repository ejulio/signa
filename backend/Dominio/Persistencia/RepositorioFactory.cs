using Dominio.Sinais;

namespace Dominio.Persistencia
{
    public class RepositorioFactory : IRepositorioFactory
    {
        private readonly string caminhoDoArquivoDeDados;

        private static IRepositorio<Sinal> repositorioSinais;
        private static IRepositorio<Sinal> repositorioSinaisEstaticos;
        private static IRepositorio<Sinal> repositorioSinaisDinamicos;

        public RepositorioFactory(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisEstaticos()
        {
            if (repositorioSinaisEstaticos == null)
            {
                repositorioSinaisEstaticos = new RepositorioSinaisEstaticos(InstanciaUnicaDeReposiotioDeSinais());
                repositorioSinaisEstaticos.Carregar();    
            }
            
            return repositorioSinaisEstaticos;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisDinamicos()
        {
            if (repositorioSinaisDinamicos == null)
            {
                repositorioSinaisDinamicos = new RepositorioSinaisDinamicos(InstanciaUnicaDeReposiotioDeSinais());
                repositorioSinaisDinamicos.Carregar();
            }

            return repositorioSinaisDinamicos;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinais()
        {
            var repositorio = InstanciaUnicaDeReposiotioDeSinais();
            repositorio.Carregar();

            return repositorio;
        }

        private IRepositorio<Sinal> InstanciaUnicaDeReposiotioDeSinais()
        {
            if (repositorioSinais == null)
            {
                repositorioSinais = new RepositorioSinais(caminhoDoArquivoDeDados);
            }

            return repositorioSinais;
        }
    }
}