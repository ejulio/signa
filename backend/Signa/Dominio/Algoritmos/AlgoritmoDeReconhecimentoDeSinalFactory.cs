using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos
{
    public class AlgoritmoDeReconhecimentoDeSinalFactory : IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos AlgoritmoDeReconhecimentoDeSinaisEstaticos;
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisDinamicos AlgoritmoDeReconhecimentoDeSinaisDinamicos;
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos AlgoritmoDeReconhecimentoDeFramesDeSinaisDinamicos;

        static AlgoritmoDeReconhecimentoDeSinalFactory()
        {
            AlgoritmoDeReconhecimentoDeSinaisEstaticos = new Svm();
            AlgoritmoDeReconhecimentoDeFramesDeSinaisDinamicos = new Svm();
            AlgoritmoDeReconhecimentoDeSinaisDinamicos = new Hcrf();
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeSinaisEstaticos()
        {
            return AlgoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public IAlgoritmoDeReconhecimentoDeSinaisDinamicos CriarReconhecedorDeSinaisDinamicos()
        {
            return AlgoritmoDeReconhecimentoDeSinaisDinamicos;
        }

        public IAlgoritmoDeReconhecimentoDeSinaisEstaticos CriarReconhecedorDeFramesDeSinaisDinamicos()
        {
            return AlgoritmoDeReconhecimentoDeFramesDeSinaisDinamicos;
        }
    }
}