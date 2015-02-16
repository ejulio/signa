using Signa.Model;
using System;
using System.Collections.Generic;

namespace Signa.Tests
{
    class SignSamples
    {
        internal IList<Sign> Data { get; private set; }

        public SignSamples()
        {
            Data = new List<Sign>();
        }

        public void Generate()
        {
            Sign sign;
            SignSample sample;
            IList<SignSample> samples;
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                samples = new List<SignSample>();
                for (int j = 0; j < 3; j++)
                {
                    sample = new SignSample
                    {
                        PalmNormal = new double[] { random.NextDouble(), random.NextDouble(), random.NextDouble() },
                        HandDirection = new double[] { random.NextDouble(), random.NextDouble(), random.NextDouble() },
                        AnglesBetweenFingers = new double[] { random.NextDouble(), random.NextDouble(), random.NextDouble(), random.NextDouble() }
                    };
                    samples.Add(sample);
                }

                sign = new Sign
                {
                    Description = "Sign " + i,
                    ExampleFilePath = ""
,
                    Samples = samples
                };

                Data.Add(sign);
            }
        }
    }
}
