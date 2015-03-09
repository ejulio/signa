﻿using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio.Algoritmos
{
    public class AlgoritmoDeReconhecimentoDeSinalFactory : IAlgoritmoDeReconhecimentoDeSinalFactory
    {
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos AlgoritmoDeReconhecimentoDeSinaisEstaticos;
        private static readonly IAlgoritmoDeReconhecimentoDeSinaisDinamicos AlgoritmoDeReconhecimentoDeSinaisDinamicos;

        static AlgoritmoDeReconhecimentoDeSinalFactory()
        {
            AlgoritmoDeReconhecimentoDeSinaisEstaticos = new Svm();
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
    }
}