using Dominio.Algoritmos.Caracteristicas;

namespace Dominio.Algoritmos.Factories
{
    public interface IGeradorDeCaracteristicasFactory
    {
        IGeradorDeCaracteristicasDeSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico();
        ICaracteristicasSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico();
        ICaracteristicasSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
    }
}