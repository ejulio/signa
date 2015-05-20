using Dominio.Sinais;

namespace Dominio.Persistencia
{
    public class RepositorioFactory : IRepositorioFactory
    {
        private readonly string caminhoDoArquivoDeDados;

        private static IRepositorio<Sinal> repositorioDeSinais;
        private static IRepositorio<Sinal> repositorioDeSinaisEstaticos;
        private static IRepositorio<Sinal> repositorioDeSinaisDinamicos;

        public RepositorioFactory(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisEstaticos()
        {
            if (repositorioDeSinaisEstaticos == null)
            {
                repositorioDeSinaisEstaticos = new RepositorioSinaisEstaticos(InstanciaUnicaDeReposiotioDeSinais());
                repositorioDeSinaisEstaticos.Carregar();    
            }
            
            return repositorioDeSinaisEstaticos;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisDinamicos()
        {
            if (repositorioDeSinaisDinamicos == null)
            {
                repositorioDeSinaisDinamicos = new RepositorioSinaisDinamicos(InstanciaUnicaDeReposiotioDeSinais());
                repositorioDeSinaisDinamicos.Carregar();
            }

            return repositorioDeSinaisDinamicos;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinais()
        {
            var repositorio = InstanciaUnicaDeReposiotioDeSinais();
            repositorio.Carregar();

            return repositorio;
        }

        private IRepositorio<Sinal> InstanciaUnicaDeReposiotioDeSinais()
        {
            if (repositorioDeSinais == null)
            {
                repositorioDeSinais = new RepositorioSinais(caminhoDoArquivoDeDados);
            }

            return repositorioDeSinais;
        }
    }
}