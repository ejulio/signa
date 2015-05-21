
namespace Dominio.Algoritmos.Dados
{
    public interface IDadosSinaisEstaticos
    {
        double[][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
