using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Signa.Data.Repository;
using Signa.Domain.Algorithms;
using Signa.Domain.Algorithms.Static;

namespace Signa.Domain
{
    public class AlgorithmInitializerFacade
    {
        private readonly ISignRecognitionAlgorithmFactory signRecognitionAlgorithmFactory;
        private readonly IRepositoryFactory repositoryFactory;

        public AlgorithmInitializerFacade(ISignRecognitionAlgorithmFactory signRecognitionAlgorithmFactory, IRepositoryFactory repositoryFactory)
        {
            this.signRecognitionAlgorithmFactory = signRecognitionAlgorithmFactory;
            this.repositoryFactory = repositoryFactory;
        }

        public void TrainStaticSignRecognitionAlgorithm()
        {
            var algorithm = signRecognitionAlgorithmFactory.CreateStaticSignRecognizer();
            var repository = repositoryFactory.CreateAndLoadStaticSignRepository();
            var algorithmData = new SignRecognitionAlgorithmData(repository);

            algorithm.Train(algorithmData);
        }
    }
}