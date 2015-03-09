namespace Signa.Dominio.Sinais
{
    public interface IAmostraDeSinalDinamico : IAmostra
    {
        double[][] ParaArray();
        IAmostraDeSinalEstatico PrimeiroFrame();
        IAmostraDeSinalEstatico UltimoFrame();
    }
}