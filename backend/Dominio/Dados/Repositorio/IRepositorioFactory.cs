using Dominio.Sinais;

namespace Dominio.Dados.Repositorio
{
    public interface IRepositorioFactory
    {
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisEstaticos();
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisDinamicos();
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinais();
    }
}