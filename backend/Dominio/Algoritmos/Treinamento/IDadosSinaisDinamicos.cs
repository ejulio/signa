
namespace Dominio.Algoritmos.Treinamento
{
    public interface IDadosSinaisDinamicos
    {
        double[][][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
