using Dominio.Algoritmos.Caracteristicas;

namespace Dominio.Algoritmos.Factories
{
    public class GeradorDeCaracteristicasFactory : IGeradorDeCaracteristicasFactory
    {
        private static IGeradorDeCaracteristicasDeSinalDinamico geradorDeCaracteristicasDeSinalDinamico;
        private static IGeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicasDeSinalEstatico;
        private static IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicasDeSinalEstaticoComTipoFrame;

        public GeradorDeCaracteristicasFactory()
        {
            geradorDeCaracteristicasDeSinalDinamico = new GeradorDeCaracteristicasDeSinalDinamico();
            geradorDeCaracteristicasDeSinalEstatico = new GeradorDeCaracteristicasDeSinalEstatico();
            geradorDeCaracteristicasDeSinalEstaticoComTipoFrame = new GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(geradorDeCaracteristicasDeSinalEstatico);
        }

        public IGeradorDeCaracteristicasDeSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico()
        {
            return geradorDeCaracteristicasDeSinalDinamico;
        }

        public IGeradorDeCaracteristicasDeSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico()
        {
            return geradorDeCaracteristicasDeSinalEstatico;
        }

        public IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame()
        {
            return geradorDeCaracteristicasDeSinalEstaticoComTipoFrame;
        }
    }
}