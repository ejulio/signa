using Signa.Domain.Sinais.Estatico;

namespace Signa.Dados.Repositorio
{
    public interface IRepositorioFactory
    {
        IRepositorio<SinalEstatico> CriarECarregarRepositorioDeSinaisEstaticos();
    }
}