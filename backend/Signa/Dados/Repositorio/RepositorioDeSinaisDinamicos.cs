using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Signa.Domain.Signs.Dynamic;

namespace Signa.Dados.Repositorio
{
    public class RepositorioDeSinaisDinamicos : IRepositorio<SinalDinamico>
    {
        private IList<SinalDinamico> sinaisPorIndice;
        private readonly IDictionary<string, SinalDinamico> sinaisPorId;

        private readonly string caminhoDoArquivoDeDados;

        public int Quantidade
        {
            get { return sinaisPorIndice.Count; }
        }

        public RepositorioDeSinaisDinamicos(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
            sinaisPorIndice = new List<SinalDinamico>();
            sinaisPorId = new Dictionary<string, SinalDinamico>();
        }

        public void Adicionar(SinalDinamico sinal)
        {
            sinaisPorId.Add(sinal.Descricao, sinal);
            sinaisPorIndice.Add(sinal);
        }

        public SinalDinamico BuscarPorIndice(int indice)
        {
            if (indice == Quantidade)
                return null;

            return sinaisPorIndice[indice];
        }

        public SinalDinamico BuscarPorId(string id)
        {
            SinalDinamico sinalDinamico;
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
                var sinais = JsonConvert.DeserializeObject<List<SinalDinamico>>(sinaisEmFormatoJson);
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

        public IEnumerator<SinalDinamico> GetEnumerator()
        {
            return sinaisPorIndice.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sinaisPorIndice.GetEnumerator();
        }
    }
}