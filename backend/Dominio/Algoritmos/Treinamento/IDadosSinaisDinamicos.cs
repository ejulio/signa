
namespace Dominio.Algoritmos.Treinamento
{
    public interface IDadosSinaisDinamicos : IDadosAlgoritmoClassificacaoSinais
    {
        double[][][] CaracteristicasSinais { get; }
    }
}
