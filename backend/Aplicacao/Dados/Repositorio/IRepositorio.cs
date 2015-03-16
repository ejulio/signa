﻿
using System.Collections.Generic;

namespace Aplicacao.Dados.Repositorio
{
    public interface IRepositorio<TEntidade> : IEnumerable<TEntidade>
    {
        int Quantidade { get; }
        void Adicionar(TEntidade sinal);
        TEntidade BuscarPorIndice(int indice);
        TEntidade BuscarPorDescricao(string id);
        void Carregar();
        void SalvarAlteracoes();
    }
}