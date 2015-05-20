using Dominio.Algoritmos.Caracteristicas;

namespace Dominio.Algoritmos.Factories
{
    public interface IGeradorDeCaracteristicasFactory
    {
        ICaracteristicasSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico();
        ICaracteristicasSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico();
        ICaracteristicasSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
    }
}