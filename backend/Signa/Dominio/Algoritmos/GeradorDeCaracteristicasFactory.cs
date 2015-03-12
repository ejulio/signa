using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos
{
    public class GeradorDeCaracteristicasFactory
    {
        private static IGeradorDeCaracteristicasDeSinalDinamico geradorDeCaracteristicasDeSinalDinamico;
        private static IGeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicasDeSinalEstatico;
        private static GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicasDeSinalEstaticoComTipoFrame;

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

        public GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame()
        {
            return geradorDeCaracteristicasDeSinalEstaticoComTipoFrame;
        }
    }
}