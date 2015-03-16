using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame : IGeradorDeCaracteristicasDeSinalEstatico
    {
        TipoFrame TipoFrame { get; set; }
    }
}