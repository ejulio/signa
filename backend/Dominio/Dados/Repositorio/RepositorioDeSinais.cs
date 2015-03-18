using System;
using Dominio.Sinais;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Dominio.Dados.Repositorio
{
    public class RepositorioDeSinais : IRepositorio<Sinal>
    {
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
            if (!File.Exists(caminhoDoArquivoDeDados))
                return;

            using (var reader = new StreamReader(caminhoDoArquivoDeDados))
            {
                var sinaisEmFormatoJson = reader.ReadToEnd();
                var sinais = JsonConvert.DeserializeObject<List<Sinal>>(sinaisEmFormatoJson);
                sinaisPorIndice = sinais ?? sinaisPorIndice;
                CarregarSinaisPorId();
            }
        }

        private void CarregarSinaisPorId()
        {
            sinaisPorId = new Dictionary<string, Sinal>();
            foreach (var sinal in sinaisPorIndice)
            {
                Console.WriteLine("{0}:{1}", sinal.Id, sinal.Descricao);
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