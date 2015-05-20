using Dominio.Algoritmos.Caracteristicas;

namespace Dominio.Algoritmos.Factories
{
    public class GeradorDeCaracteristicasFactory : IGeradorDeCaracteristicasFactory
    {
        private static IGeradorDeCaracteristicasDeSinalDinamico geradorDeCaracteristicasDeSinalDinamico;
        private static ICaracteristicasSinalEstatico caracteristicasSinalEstatico;
        private static ICaracteristicasSinalEstaticoComTipoFrame caracteristicasSinalEstaticoComTipoFrame;

        public GeradorDeCaracteristicasFactory()
        {
            geradorDeCaracteristicasDeSinalDinamico = new GeradorDeCaracteristicasDeSinalDinamico();
            caracteristicasSinalEstatico = new CaracteristicasSinalEstatico();
            caracteristicasSinalEstaticoComTipoFrame = new CaracteristicasSinalEstaticoComTipoFrame(caracteristicasSinalEstatico);
        }

        public IGeradorDeCaracteristicasDeSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico()
        {
            return geradorDeCaracteristicasDeSinalDinamico;
        }

        public ICaracteristicasSinalEstatico CriarGeradorDeCaracteristicasDeSinalEstatico()
        {
            return caracteristicasSinalEstatico;
        }

        public ICaracteristicasSinalEstaticoComTipoFrame CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame()
        {
            return caracteristicasSinalEstaticoComTipoFrame;
        }
    }
}