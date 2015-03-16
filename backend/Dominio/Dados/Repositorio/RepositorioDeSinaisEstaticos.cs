using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais;

namespace Dominio.Dados.Repositorio
{
    public class RepositorioDeSinaisEstaticos : IRepositorio<Sinal>
    {
        private readonly IRepositorio<Sinal> repositorioDeSinais;
        private IList<Sinal> sinaisPorIndice;
        private IDictionary<string, Sinal> sinaisPorId;

        public int Quantidade
        {
            get { return sinaisPorIndice.Count; }
        }

        public RepositorioDeSinaisEstaticos(IRepositorio<Sinal> repositorioDeSinais)
        {
            this.repositorioDeSinais = repositorioDeSinais;
            sinaisPorIndice = new List<Sinal>();
            sinaisPorId = new Dictionary<string, Sinal>();
        }

        public void Adicionar(Sinal sinal)
        {
            sinaisPorId.Add(sinal.Descricao, sinal);
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
            Sinal sinal;
            if (sinaisPorId.TryGetValue(id, out sinal))
                return sinal;

            return null;
        }

        public void Carregar()
        {
            repositorioDeSinais.Carregar();
            sinaisPorIndice = repositorioDeSinais.Where(s => s.Tipo == TipoSinal.Estatico).ToList();
            CarregarSinaisPorId();
        }

        private void CarregarSinaisPorId()
        {
            foreach (var sinal in sinaisPorIndice)
            {
                sinaisPorId.Add(sinal.Descricao, sinal);
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