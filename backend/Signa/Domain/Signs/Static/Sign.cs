using System.Collections.Generic;

namespace Signa.Domain.Signs.Static
{
    public class Sign
    {
        public string Description { get; set; }
        public string ExampleFilePath { get; set; }
        public IList<Sample> Samples { get; set; }

        public Sign()
        {
            Samples = new List<Sample>();
        }
    }
}