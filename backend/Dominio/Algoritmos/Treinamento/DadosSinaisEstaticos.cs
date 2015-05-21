﻿using System.Collections.Generic;
using System.Linq;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Treinamento
{
    public class DadosSinaisEstaticos : DadosAlgoritmoClassificacaoSinais, 
        IDadosSinaisEstaticos
    {
        public double[][] Entradas { get; private set; }
        
        private LinkedList<double[]> entradas;

        private CaracteristicasSinalEstatico caracteristicas;

        public override int QuantidadeDeClassesPorSinal
        {
            get { return 1; }
        }

        public DadosSinaisEstaticos(IEnumerable<Sinal> sinais)
            : base(sinais)
        {
        }

        protected override void Inicializar(IEnumerable<Sinal> sinais)
        {
            entradas = new LinkedList<double[]>();
            caracteristicas = new CaracteristicasSinalEstatico();
        }

        protected override void GerarEntradasESaidasParaAmostra(IList<Frame> amostra, LinkedList<int> saidas, int identificadorDoSinal)
        {
            var caracteristicas = this.caracteristicas.DaAmostra(amostra);
            entradas.AddLast(caracteristicas);
            saidas.AddLast(identificadorDoSinal);
        }

        protected override void Finalizar()
        {
            Entradas = entradas.ToArray();
        }
    }
}