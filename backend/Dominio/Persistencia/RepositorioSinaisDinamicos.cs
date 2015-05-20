using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais;

namespace Dominio.Persistencia
{
    public class RepositorioSinaisDinamicos : IRepositorio<Sinal>
    {
        private readonly IRepositorio<Sinal> repositorioDeSinais;
        private IList<Sinal> sinaisPorIndice;
        private readonly IDictionary<string, Sinal> sinaisPorDescricao;


        public int Quantidade
        {
            get { return sinaisPorIndice.Count; }
        }

        public RepositorioSinaisDinamicos(IRepositorio<Sinal> repositorioDeSinais)
        {
            this.repositorioDeSinais = repositorioDeSinais;
            sinaisPorIndice = new List<Sinal>();
            sinaisPorDescricao = new Dictionary<string, Sinal>();
        }

        public void Adicionar(Sinal sinal)
        {
            sinaisPorDescricao.Add(sinal.Descricao, sinal);
            sinaisPorIndice.Add(sinal);
            repositorioDeSinais.Adicionar(sinal);
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
            if (sinaisPorDescricao.TryGetValue(id, out sinalDinamico))
                return sinalDinamico;

            return null;
        }

        public void Carregar()
        {
            repositorioDeSinais.Carregar();
            sinaisPorIndice = repositorioDeSinais.Where(s => s.Tipo == TipoSinal.Dinamico).ToList();
            CarregarSinaisPorDescricao();
        }

        private void CarregarSinaisPorDescricao()
        {
            foreach (var sinal in sinaisPorIndice)
            {
                sinaisPorDescricao.Add(sinal.Descricao, sinal);
            }
        }

        public void SalvarAlteracoes()
        {
            repositorioDeSinais.SalvarAlteracoes();
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