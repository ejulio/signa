﻿using Microsoft.AspNet.SignalR;
using Signa.Dados.Repositorio;
using Signa.Dominio.Sinais;
using System;

namespace Signa.Hubs
{
    public class Sinais : Hub
    {
        private readonly IRepositorio<Sinal> repositorio;

        public Sinais(IRepositorio<Sinal> repositorio)
        {
            this.repositorio = repositorio;
        }

        public InformacoesDoSinal ProximoSinal(int previousSignIndex)
        {
            var random = new Random();
            int indice = random.Next(repositorio.Quantidade);
            var sinal = repositorio.BuscarPorIndice(indice);

            var informadoesDoSinal = new InformacoesDoSinal
            {
                Id = indice,
                Descricao = sinal.Descricao,
                CaminhoParaArquivoDeExemplo = sinal.CaminhoParaArquivoDeExemplo,
                Tipo = sinal.Tipo
            };

            return informadoesDoSinal;
        }
    }
}