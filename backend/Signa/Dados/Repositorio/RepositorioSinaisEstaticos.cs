using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Signa.Domain.Signs.Static;

namespace Signa.Dados.Repositorio
{
    public class RepositorioSinaisEstaticos : IRepositorio<SinalEstatico>
    {
        private IList<SinalEstatico> sinaisPorIndice;
        private readonly IDictionary<string, SinalEstatico> sinaisPorId;

        private readonly string caminhoDoArquivoDeDados;

        public int Quantidade
        {
            get { return sinaisPorIndice.Count; }
        }

        public RepositorioSinaisEstaticos(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
            sinaisPorIndice = new List<SinalEstatico>();
            sinaisPorId = new Dictionary<string, SinalEstatico>();
        }

        public void Adicionar(SinalEstatico sinal)
        {
            sinaisPorId.Add(sinal.Description, sinal);
            sinaisPorIndice.Add(sinal);
        }

        public SinalEstatico BuscarPorIndice(int indice)
        {
            if (indice == Quantidade)
                return null;

            return sinaisPorIndice[indice];
        }

        public SinalEstatico BuscarPorId(string id)
        {
            SinalEstatico sinalEstatico;
            if (sinaisPorId.TryGetValue(id, out sinalEstatico))
                return sinalEstatico;

            return null;
        }

        public void Carregar()
        {
            if (!File.Exists(caminhoDoArquivoDeDados))
                return;

            using (var reader = new StreamReader(caminhoDoArquivoDeDados))
            {
                var sinaisEmFormatoJson = reader.ReadToEnd();
                var sinais = JsonConvert.DeserializeObject<List<SinalEstatico>>(sinaisEmFormatoJson);
                sinaisPorIndice = sinais ?? sinaisPorIndice;
                CarregarSinaisPorId();
            }
        }

        private void CarregarSinaisPorId()
        {
            foreach (var sinal in sinaisPorIndice)
            {
                sinaisPorId.Add(sinal.Description, sinal);
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

        public IEnumerator<SinalEstatico> GetEnumerator()
        {
            return sinaisPorIndice.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sinaisPorIndice.GetEnumerator();
        }
    }
}