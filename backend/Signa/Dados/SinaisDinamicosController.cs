using Signa.Dominio.Sinais;
using System;

namespace Signa.Dados
{
    public class SinaisDinamicosController
    {
        public int Reconhecer(Frame[] amostra)
        {
            throw new NotImplementedException("Implementar para reconhecer o sinal utilizando HMM ou HCRF");
        }

        public int ReconhecerPrimeiroFrame(Frame[] amostra)
        {
            throw new NotImplementedException("Implemetar para reconhecer o frame utilizando SVM");
        }

        public int ReconhecerUltimoFrame(Frame[] amostra)
        {
            throw new NotImplementedException("Implemetar para reconhecer o frame utilizando SVM");
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, Frame[] amostra)
        {
            throw new NotImplementedException("Implementar para salvar o sinal e treinar o algoritmo HMM ou HCRF");
        } 
    }
}