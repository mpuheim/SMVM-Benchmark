using System;

namespace SparseVectorMatrixMultiply.Generators
{
    public class RandomMatrixGenerator
    {
        private Random rnd;

        public RandomMatrixGenerator()
        {
            rnd = new Random();
        }

        /// <summary>
        /// Generate a random matrix with specified size and sparcity
        /// </summary>
        /// <param name="height">Height of the matrix</param>
        /// <param name="width">Width of the matrix</param>
        /// <param name="nonzeros">Number of nonzero elements</param>
        /// <param name="minvalue">Minimum value of nonzero matrix element</param>
        /// <param name="maxvalue">Maximum value of nonzero matrix element</param>
        /// <returns>random matrix with specified size and sparcity</returns>
        public double[,] nextMatrix(int height, int width, int nonzeros, double minvalue = -1, double maxvalue = 1)
        {
            //declare auxiliary variables
            int i, j, k, new_i, new_j, new_k;
            double tmp;
            int len = height * width;
            //initialize matrix
            double[,] matrix = new double[height, width];
            //fill in random values
            k = 0;
            for (i = 0; i < height; i++)
                for (j = 0; j < width; j++, k++)
                {
                    if (k < nonzeros)
                        matrix[i, j] = rnd.NextDouble() * (maxvalue - minvalue) + minvalue;
                    else
                        matrix[i, j] = 0;
                }
            //shuffle matrix elements
            for (k = 0; k < nonzeros; k++)
            {
                //get matrix indices
                i = k / height;
                j = k % width;
                new_k = rnd.Next(k, len);
                new_i = new_k / height;
                new_j = new_k % width;
                //swap elements
                tmp = matrix[i,j];
                matrix[i, j] = matrix[new_i, new_j];
                matrix[new_i, new_j] = tmp;
            }
            //return random matrix
            return matrix;
        }
    }
}
