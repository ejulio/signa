using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dominio
{
    public class AlgorithmInitializerFacade
    {
        private readonly ISignRecognitionAlgorithmFactory signRecognitionAlgorithmFactory;
        private readonly IRepositorioFactory repositorioFactory;

        public AlgorithmInitializerFacade(ISignRecognitionAlgorithmFactory signRecognitionAlgorithmFactory, IRepositorioFactory repositorioFactory)
        {
            this.signRecognitionAlgorithmFactory = signRecognitionAlgorithmFactory;
            this.repositorioFactory = repositorioFactory;
        }

        public void TrainStaticSignRecognitionAlgorithm()
        {
            var algorithm = signRecognitionAlgorithmFactory.CreateStaticSignRecognizer();
            var repository = repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos();
            var algorithmData = new SignRecognitionAlgorithmData(repository);

            algorithm.Train(algorithmData);
        }
    }
}