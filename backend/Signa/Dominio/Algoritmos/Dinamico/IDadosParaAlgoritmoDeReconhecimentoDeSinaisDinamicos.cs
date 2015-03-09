
namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IDadosParaAlgoritmoDeReconhecimentoDeSinaisDinamicos
    {
        double[][][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
