using Dominio.Algoritmos;
using Dominio.Persistencia;
using Dominio.Sinais;

namespace Dominio.Gerenciamento
{
    public class GerenciadorSinaisEstaticos : GerenciadorSinais
    {
        public GerenciadorSinaisEstaticos(IRepositorio<Sinal> repositorio, 
            IAlgoritmoClassificacaoSinais algoritmoClassificacaoSinaisEstaticos) 
            : base(repositorio, algoritmoClassificacaoSinaisEstaticos)
        {
        }
    }
}