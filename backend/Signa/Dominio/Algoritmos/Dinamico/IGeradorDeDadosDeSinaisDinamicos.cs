
namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IGeradorDeDadosDeSinaisDinamicos
    {
        double[][][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
