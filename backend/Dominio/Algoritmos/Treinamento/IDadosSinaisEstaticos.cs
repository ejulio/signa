
namespace Dominio.Algoritmos.Treinamento
{
    public interface IDadosSinaisEstaticos
    {
        double[][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
