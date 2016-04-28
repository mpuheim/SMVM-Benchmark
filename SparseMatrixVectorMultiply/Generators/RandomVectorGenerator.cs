using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparseVectorMatrixMultiply.Generators
{
    public class RandomVectorGenerator
    {
        private Random rnd;

        public RandomVectorGenerator()
        {
            rnd = new Random();
        }

        /// <summary>
        /// Generate a random vector with specified size
        /// </summary>
        /// <param name="lenght">Lenght of the vector</param>
        /// <param name="nonzeros">Number of nonzero elements</param>
        /// <param name="minvalue">Minimum value of nonzero matrix element</param>
        /// <param name="maxvalue">Maximum value of nonzero matrix element</param>
        /// <returns>random vector with specified size</returns>
        public double[] nextVector(int lenght, int nonzeros, double minvalue = -1, double maxvalue = 1)
        {
            //declare auxiliary variables
            int i, new_i;
            double tmp;
            //initialize vector
            double[] vector = new double[lenght];
            //fill in random values
            for (i = 0; i < nonzeros; i++)
                vector[i] = rnd.NextDouble() * (maxvalue - minvalue) + minvalue;
            //fill in zeros
            for (i = nonzeros; i < lenght; i++)
                vector[i] = 0;
            //shuffle elements
            for (i = 0; i < nonzeros; i++)
            {
                //get new index
                new_i = rnd.Next(i, lenght);
                //swap elements
                tmp = vector[i];
                vector[i] = vector[new_i];
                vector[new_i] = tmp;
            }
            //return random matrix
            return vector;
        }
    }
}
