using Dominio.Sinais;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface ICaracteristicasSinalEstaticoComTipoFrame : ICaracteristicasSinalEstatico
    {
        TipoFrame TipoFrame { get; set; }
        Frame PrimeiroFrame { get; set; }
    }
}