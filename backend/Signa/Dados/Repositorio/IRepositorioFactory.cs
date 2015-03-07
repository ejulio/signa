using Signa.Domain.Signs.Static;

namespace Signa.Dados.Repositorio
{
    public interface IRepositorioFactory
    {
        IRepositorio<SinalEstatico> CriarECarregarRepositorioDeSinaisEstaticos();
    }
}