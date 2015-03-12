using Signa.Dominio.Algoritmos.Caracteristicas;

namespace Signa.Dominio.Algoritmos.Factories
{
    public interface IGeradorDeCaracteristicasFactory
    {
        IGeradorDeCaracteristicasDeSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico();
        IGeradorDeCaracteristicasDeSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico();
        IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
    }
}