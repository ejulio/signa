using Microsoft.Owin.Hosting;
using Signa.Data;
using Signa.Recognizer;
using System;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();

            var repository = new SignRepository(SignController.SignSamplesFilePath);
            repository.Load();
            if (repository.Count == 0)
            {
                Console.WriteLine("Sem dados para treinar o algoritmo");
            }
            else
            {
                Console.WriteLine("Treinando o algoritmo com os exemplos");
                var trainningData = new SvmTrainningData(repository);
                Svm.Instance.Train(trainningData);
                Console.WriteLine("Algoritmo treinado");
            }

            Console.ReadKey();
        }

        private static void StartServer()
        {
            var serverAddress = "http://localhost:9000";
            WebApp.Start<Signa.Startup>(serverAddress);
            Console.WriteLine("Aplicação iniciada em {0}", serverAddress);
        }
    }
}
