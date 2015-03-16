using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dados.Repositorio
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
                repositorioDeSinaisEstaticos = new RepositorioDeSinaisEstaticos(InstanciaUnicaDeReposiotioDeSinais());
                repositorioDeSinaisEstaticos.Carregar();    
            }
            
            return repositorioDeSinaisEstaticos;
        }

        public IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisDinamicos()
        {
            if (repositorioDeSinaisDinamicos == null)
            {
                repositorioDeSinaisDinamicos = new RepositorioDeSinaisDinamicos(InstanciaUnicaDeReposiotioDeSinais());
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
                repositorioDeSinais = new RepositorioDeSinais(caminhoDoArquivoDeDados);
            }

            return repositorioDeSinais;
        }
    }
}