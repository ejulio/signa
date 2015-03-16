using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame : IGeradorDeCaracteristicasDeSinalEstatico
    {
        TipoFrame TipoFrame { get; set; }
    }
}