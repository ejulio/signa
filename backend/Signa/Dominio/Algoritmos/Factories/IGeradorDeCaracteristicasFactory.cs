using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos.Factories
{
    public interface IGeradorDeCaracteristicasFactory
    {
        IGeradorDeCaracteristicasDeSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico();
        IGeradorDeCaracteristicasDeSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico();
        IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
    }
}