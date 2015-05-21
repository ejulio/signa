
namespace Dominio.Algoritmos.Treinamento
{
    public interface IDadosSinaisEstaticos : IDadosAlgoritmoClassificacaoSinais
    {
        double[][] CaracteristicasSinais { get; }
    }
}
