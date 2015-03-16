
namespace Dominio.Algoritmos.Dados
{
    public interface IGeradorDeDadosDeSinaisEstaticos
    {
        double[][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
