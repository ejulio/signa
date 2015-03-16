using Aplicacao.Dominio.Algoritmos.Caracteristicas;

namespace Aplicacao.Dominio.Algoritmos.Factories
{
    public interface IGeradorDeCaracteristicasFactory
    {
        IGeradorDeCaracteristicasDeSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico();
        IGeradorDeCaracteristicasDeSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico();
        IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
    }
}