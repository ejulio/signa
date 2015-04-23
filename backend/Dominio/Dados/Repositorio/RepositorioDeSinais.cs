﻿using Dominio.Sinais;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dominio.Dados.Repositorio
{
    public class RepositorioDeSinais : IRepositorio<Sinal>
    {
        private bool carregado;
        private IList<Sinal> sinaisPorIndice;
        private IDictionary<string, Sinal> sinaisPorId;

        private readonly string caminhoDoArquivoDeDados;

        public int Quantidade
        {
            get { return sinaisPorIndice.Count; }
        }

        public RepositorioDeSinais(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
            sinaisPorIndice = new List<Sinal>();
            sinaisPorId = new Dictionary<string, Sinal>();
            carregado = false;
        }

        public void Adicionar(Sinal sinal)
        {
            sinal.Id = Quantidade;
            sinaisPorId.Add(sinal.Descricao, sinal);
            sinaisPorIndice.Add(sinal);
        }

        public Sinal BuscarPorIndice(int indice)
        {
            if (indice == Quantidade)
                return null;

            return sinaisPorIndice[indice];
        }

        public Sinal BuscarPorDescricao(string id)
        {
            Sinal sinalDinamico;
            if (sinaisPorId.TryGetValue(id, out sinalDinamico))
                return sinalDinamico;

            return null;
        }

        public void Carregar()
        {
            if (!File.Exists(caminhoDoArquivoDeDados) || carregado)
                return;

            using (var reader = new StreamReader(caminhoDoArquivoDeDados))
            {
                var sinaisEmFormatoJson = reader.ReadToEnd();
                var sinais = JsonConvert.DeserializeObject<List<Sinal>>(sinaisEmFormatoJson);
                sinaisPorIndice = sinais ?? sinaisPorIndice;
                CarregarSinaisPorId();
                carregado = true;
                string str = "";
                foreach (var sinal in this)
                {
                    str += string.Format("{0}: {1}{2}", sinal.Descricao, sinal.Amostras.Count, Environment.NewLine);
                }
                str += string.Format("Total amostras: {0}{1}", this.Sum(s => s.Amostras.Count), Environment.NewLine);
                str += string.Format("Dinâmicos: {0}{1}", this.Where(s => s.Tipo == TipoSinal.Dinamico).Sum(s => s.Amostras.Count), Environment.NewLine);
                str += string.Format("Estáticos: {0}{1}", this.Where(s => s.Tipo == TipoSinal.Estatico).Sum(s => s.Amostras.Count), Environment.NewLine);
            }
        }

        private void CarregarSinaisPorId()
        {
            sinaisPorId = new Dictionary<string, Sinal>();
            foreach (var sinal in sinaisPorIndice)
            {
                sinaisPorId.Add(sinal.Descricao, sinal);
            }
        }

        public void SalvarAlteracoes()
        {
            using (StreamWriter writer = new StreamWriter(caminhoDoArquivoDeDados))
            {
                var sinaisEmFormatoJson = JsonConvert.SerializeObject(sinaisPorId.Values);
                writer.Write(sinaisEmFormatoJson);
            }
        }

        public IEnumerator<Sinal> GetEnumerator()
        {
            return sinaisPorIndice.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sinaisPorIndice.GetEnumerator();
        }
    }
}