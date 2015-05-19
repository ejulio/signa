using Dominio.Sinais;

namespace Dominio.Persistencia
{
    public interface IRepositorioFactory
    {
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisEstaticos();
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisDinamicos();
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinais();
    }
}