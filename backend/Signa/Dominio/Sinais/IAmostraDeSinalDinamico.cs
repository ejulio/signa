namespace Signa.Dominio.Sinais
{
    public interface IAmostraDeSinalDinamico
    {
        double[][] ParaArray();
        IAmostraDeSinalEstatico PrimeiroFrame();
        IAmostraDeSinalEstatico UltimoFrame();
    }
}