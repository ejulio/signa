using System;

namespace Signa.Tests.Common.Builders
{
    public class DoubleArrayBuilder
    {
        private int size;
        
        public DoubleArrayBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public double[] Build()
        {
            double[] array = new double[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.NextDouble();
            }

            return array;
        }
    }
}
