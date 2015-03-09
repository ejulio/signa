using Signa.Dominio.Sinais;

namespace Signa.Dados.Repositorio
{
    public interface IRepositorioFactory
    {
        IRepositorio<Sinal> CriarECarregarRepositorioDeSinaisEstaticos();
    }
}