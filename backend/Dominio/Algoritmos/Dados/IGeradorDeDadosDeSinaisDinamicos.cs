
namespace Dominio.Algoritmos.Dados
{
    public interface IGeradorDeDadosDeSinaisDinamicos
    {
        double[][][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
