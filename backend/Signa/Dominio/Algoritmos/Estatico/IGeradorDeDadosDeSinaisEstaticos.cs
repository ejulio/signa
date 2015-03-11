
namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IGeradorDeDadosDeSinaisEstaticos
    {
        double[][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
