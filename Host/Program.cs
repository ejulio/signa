using Microsoft.Owin.Hosting;
using Signa;
using Signa.Data;
using Signa.Recognizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverAddress = "http://localhost:9000";
            WebApp.Start<Signa.Startup>(serverAddress);

            Console.WriteLine("Aplicação iniciada");
            
            Console.WriteLine("Treinando algoritmo");
            var signSamplesController = SignSamplesController.Instance;
            signSamplesController.SamplesFilePath = "./data/sign-samples.json";
            signSamplesController.Load();
            var svmRecognizerTrainningData = new SvmTrainningData(signSamplesController.SignSamples);
            Svm.Instance.Train(svmRecognizerTrainningData);
            Console.WriteLine("Algoritmo treinado");
            Console.ReadLine();
        }
    }
}
