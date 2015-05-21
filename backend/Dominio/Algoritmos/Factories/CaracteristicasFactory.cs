using Dominio.Algoritmos.Caracteristicas;

namespace Dominio.Algoritmos.Factories
{
    public class CaracteristicasFactory : ICaracteristicasFactory
    {
        private static ICaracteristicasSinalDinamico caracteristicasSinalDinamico;
        private static ICaracteristicasSinalEstatico caracteristicasSinalEstatico;
        private static ICaracteristicasSinalEstaticoComTipoFrame caracteristicasSinalEstaticoComTipoFrame;

        public CaracteristicasFactory()
        {
            caracteristicasSinalDinamico = new CaracteristicasSinalDinamico();
            caracteristicasSinalEstatico = new CaracteristicasSinalEstatico();
            caracteristicasSinalEstaticoComTipoFrame = new CaracteristicasSinalEstaticoComTipoFrame(caracteristicasSinalEstatico);
        }

        public ICaracteristicasSinalDinamico CriarGeradorDeCaracteristicasDeSinalDinamico()
        {
            return caracteristicasSinalDinamico;
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