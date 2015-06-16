using Dominio.Sinais;
using System;
using System.Text;

namespace Testes.Comum.Util
{
    public class Relatorio
    {
        private const string TemplateAcerto = "{0} (id: {1}{2})";

        private const string TemplateErro =
            "Esperado: {1} (id: {2}){0}Reconhecido: {3} (id: {4}){0}Índice: {5}{6}{0}------------";

        private int acertos;
        private int erros;
        private double totalTempos;

        private StringBuilder sinaisCorretos;
        private StringBuilder sinaisErrados;

        public Relatorio()
        {
            sinaisCorretos = new StringBuilder();
            sinaisErrados = new StringBuilder();
        }

        public void AdicionarAcerto(Sinal sinal, double tempoMs, string informacoes = "")
        {
            totalTempos += tempoMs;
            acertos++;
            sinaisCorretos.AppendLine(string.Format(TemplateAcerto, sinal.Descricao, sinal.IdNoAlgoritmo, informacoes));
        }

        public void AdicionarErro(Sinal esperado, Sinal reconhecido, int indiceDaAmostra, double tempoMs, string informacoes = "")
        {
            totalTempos += tempoMs;
            erros++;
            sinaisErrados.AppendLine(string.Format(TemplateErro,
                Environment.NewLine,
                esperado.Descricao, esperado.IdNoAlgoritmo,
                reconhecido.Descricao, reconhecido.IdNoAlgoritmo,
                indiceDaAmostra, informacoes));
        }

        public void AdicionarObservacao(string texto)
        {
            sinaisErrados
                .AppendLine("-----")
                .AppendLine(texto)
                .AppendLine("-----");
        }

        public void Imprimir()
        {
            Console.WriteLine("Total acertos: {0}", acertos);
            Console.WriteLine("Total erros: {0}", erros);
            Console.WriteLine("Tempo médio para reconhecimento: {0} ms", totalTempos / (acertos + erros));
            Console.WriteLine(sinaisCorretos.ToString());
            Console.WriteLine(sinaisErrados.ToString());
        }
    }
}