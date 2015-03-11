﻿using System.Collections.Generic;
using System.Linq;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos
{
    public abstract class DadosParaAlgoritmoDeReconhecimentoDeSinais
    {
        public int[] Saidas { get; private set; }
        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<Sinal> sinais;
        private LinkedList<int> saidas;

        protected DadosParaAlgoritmoDeReconhecimentoDeSinais(IEnumerable<Sinal> sinais)
        {
            this.sinais = sinais;
            saidas = new LinkedList<int>();
            InicializarExtracaoDeInformacoesDosSinais();
            ExtrairInformacoesDosSinais();
            FinalizarExtracaoDeInformacoesDosSinais();
        }

        protected abstract void InicializarExtracaoDeInformacoesDosSinais();

        public void ExtrairInformacoesDosSinais()
        {
            int indiceDoSinal = 0;
            QuantidadeDeClasses = 0;
            saidas = new LinkedList<int>();

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    saidas.AddLast(indiceDoSinal);
                    GerarEntradaParaAAmostra(amostra);
                }
                QuantidadeDeClasses++;
                indiceDoSinal++;
            }

            Saidas = saidas.ToArray();
        }

        protected abstract void GerarEntradaParaAAmostra(IList<Frame> amostra);
        protected abstract void FinalizarExtracaoDeInformacoesDosSinais();
    }
}