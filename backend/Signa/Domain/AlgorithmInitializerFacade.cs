using Signa.Dados.Repositorio;
using Signa.Domain.Algoritmos;
using Signa.Domain.Algoritmos.Estatico;

namespace Signa.Domain
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