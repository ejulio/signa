
namespace Signa.Domain.Algorithms.Static
{
    public interface IDadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        double[][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
