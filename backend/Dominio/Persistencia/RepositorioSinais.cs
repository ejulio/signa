using Dominio.Sinais;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Dominio.Persistencia
{
    public class RepositorioSinais : IRepositorio<Sinal>
    {
        private bool carregado;
        private IList<Sinal> sinaisPorIndice;
        private IDictionary<string, Sinal> sinaisPorDescricao;

        private readonly string caminhoDoArquivoDeDados;

        public int Quantidade
        {
            get { return sinaisPorIndice.Count; }
        }

        public RepositorioSinais(string caminhoDoArquivoDeDados)
        {
            this.caminhoDoArquivoDeDados = caminhoDoArquivoDeDados;
            sinaisPorIndice = new List<Sinal>();
            sinaisPorDescricao = new Dictionary<string, Sinal>();
            carregado = false;
        }

        public void Adicionar(Sinal sinal)
        {
            sinal.Id = Quantidade;
            sinaisPorDescricao.Add(sinal.Descricao, sinal);
            sinaisPorIndice.Add(sinal);
        }

        public Sinal BuscarPorIndice(int indice)
        {
            if (indice >= Quantidade)
                return null;

            return sinaisPorIndice[indice];
        }

        public Sinal BuscarPorDescricao(string id)
        {
            Sinal sinalDinamico;
            if (sinaisPorDescricao.TryGetValue(id, out sinalDinamico))
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
                CarregarSinaisPorDescricao();
                carregado = true;
            }
        }

        private void CarregarSinaisPorDescricao()
        {
            sinaisPorDescricao = new Dictionary<string, Sinal>();
            foreach (var sinal in sinaisPorIndice)
            {
                sinaisPorDescricao.Add(sinal.Descricao, sinal);
            }
        }

        public void SalvarAlteracoes()
        {
            using (var writer = new StreamWriter(caminhoDoArquivoDeDados))
            {
                var sinaisEmFormatoJson = JsonConvert.SerializeObject(sinaisPorDescricao.Values);
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