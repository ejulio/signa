using System.Collections.Generic;

namespace Signa.Domain.Signs.Dynamic
{
    public class Sign
    {
        public string Description { get; set; }
        public string ExampleFilePath { get; set; }
        public IList<SignSample> Samples { get; set; }

        public Sign()
        {
            Samples = new List<SignSample>();
        }
    }
}