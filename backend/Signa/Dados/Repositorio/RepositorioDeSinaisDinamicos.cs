using Newtonsoft.Json;
using Signa.Dominio.Sinais;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Signa.Dados.Repositorio
{
    public class RepositorioDeSinaisDinamicos : IRepositorio<Sinal>
    {
        private IList<Sinal> sinaisPorIndice;
        private readonly IDictionary<string, Sinal> sinaisPorId;

        private readonly string caminhoDoArquivoDeDados;

        public int Quantidade
        {
            get { return sinaisPorIndice.Count; }
        }

        public RepositorioDeSinaisDinamicos(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
            sinaisPorIndice = new List<Sinal>();
            sinaisPorId = new Dictionary<string, Sinal>();
        }

        public void Adicionar(Sinal sinal)
        {
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
                CarregarSinaisPorIndice();
            }
        }

        private void CarregarSinaisPorIndice()
        {
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