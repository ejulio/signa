using System;
using System.Diagnostics;

namespace Dominio
{
    public class MedidorTempo : IDisposable
    {
        private string descricao;
        private Stopwatch stopwatch;

        public MedidorTempo(string descricao)
        {
            this.descricao = descricao;
            stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            Console.WriteLine("Finalizou {0}. Levou {1}ms para executar.", descricao, stopwatch.ElapsedMilliseconds);
        }
    }
}