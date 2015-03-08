using System.Collections.Generic;

namespace Signa.Dominio.Sinais.Estatico
{
    public class SinalEstatico
    {
        public string Description { get; set; }
        public string ExampleFilePath { get; set; }
        public IList<Sample> Amostras { get; set; }

        public SinalEstatico()
        {
            Amostras = new List<Sample>();
        }
    }
}