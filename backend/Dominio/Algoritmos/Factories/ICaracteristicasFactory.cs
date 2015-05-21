using Dominio.Algoritmos.Caracteristicas;

namespace Dominio.Algoritmos.Factories
{
    public interface ICaracteristicasFactory
    {
        ICaracteristicasSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico();
        ICaracteristicasSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico();
        ICaracteristicasSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
    }
}