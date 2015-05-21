
namespace Dominio.Algoritmos.Dados
{
    public interface IDadosSinaisDinamicos
    {
        double[][][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
