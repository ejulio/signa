using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dados.Repositorio
{
    public interface IRepositorioFactory
    {
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisEstaticos();
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisDinamicos();
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinais();
    }
}