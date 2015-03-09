
namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IDadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        double[][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
