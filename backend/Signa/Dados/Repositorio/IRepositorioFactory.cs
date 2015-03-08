using Signa.Dominio.Sinais.Estatico;

namespace Signa.Dados.Repositorio
{
    public interface IRepositorioFactory
    {
        IRepositorio<SinalEstatico> CriarECarregarRepositorioDeSinaisEstaticos();
    }
}