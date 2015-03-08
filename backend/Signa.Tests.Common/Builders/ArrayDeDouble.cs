using System;

namespace Signa.Tests.Common.Builders
{
    public class ArrayDeDouble
    {
        private int size;
        
        public ArrayDeDouble ComTamanho(int size)
        {
            this.size = size;
            return this;
        }

        public double[] Construir()
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
