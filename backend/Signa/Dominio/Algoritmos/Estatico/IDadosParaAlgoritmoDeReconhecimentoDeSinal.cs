
namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IDadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        double[][] Entradas { get; }
        int[] Saidas { get; }
        int QuantidadeDeClasses { get; }
    }
}
