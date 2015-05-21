namespace Dominio.Algoritmos.Treinamento
{
    public interface IDadosAlgoritmoClassificacaoSinais
    {
        int[] IdentificadoresSinais { get; }
        int QuantidadeClasses { get; } 
    }
}