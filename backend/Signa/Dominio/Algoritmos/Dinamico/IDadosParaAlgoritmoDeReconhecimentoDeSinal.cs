
namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IDadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        double[][][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
