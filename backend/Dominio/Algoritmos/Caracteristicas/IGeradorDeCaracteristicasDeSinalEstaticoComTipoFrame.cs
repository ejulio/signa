using Dominio.Sinais;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame : IGeradorDeCaracteristicasDeSinalEstatico
    {
        TipoFrame TipoFrame { get; set; }
        Frame PrimeiroFrame { get; set; }
    }
}