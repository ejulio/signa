using System.Collections.Generic;

namespace Signa.Domain.Signs.Static
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