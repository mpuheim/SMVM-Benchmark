using SparseVectorMatrixMultiply.Interfaces;

namespace SparseVectorMatrixMultiply.Representations
{
    /// <summary>
    /// Compressed Column Storage - https://en.wikipedia.org/wiki/Sparse_matrix#Compressed_sparse_column_.28CSC_or_CCS.29
    /// </summary>
    class CCS : IMatrixConvertible, IVectorMultipliable, ISerializable
    {
        //matrix dimensions
        public int width;
        public int height;
        public int nonzeros;

        //CCS arrays
        public double[] val;    //holds all the nonzero entries of matrix (top-to-bottom then left-to-right)
        public int[] row_ind;   //contains row indices corresponding to the values
        public int[] col_ptr;   //list of val indexes where each column starts

        public CCS(double[,] matrix)
        {
            init(matrix);
        }

        public CCS(IMatrixConvertible matrixRepresentation)
        {
            init(matrixRepresentation.toMatrix());
        }

        private void init(double[,] matrix)
        {
            //get matrix dimensions
            height = matrix.GetLength(0);
            width = matrix.GetLength(1);
            //get number of nonzero elements
            nonzeros = 0;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (matrix[i, j] != 0)
                        nonzeros++;
            //initialize CCS arrays
            val = new double[nonzeros];
            col_ptr = new int[width + 1];
            row_ind = new int[nonzeros];
            //convert matrix to CCS
            int k = 0;
            col_ptr[0] = 0;
            for (int j = 0; j < width; j++)
                {
                for (int i = 0; i < height; i++)
                {
                    if (matrix[i, j] != 0)
                    {
                        val[k] = matrix[i, j];
                        row_ind[k] = i;
                        k++;
                    }
                }
                col_ptr[j + 1] = k;
            }
        }

        public double[,] toMatrix()
        {
            double[,] matrix = new double[height, width];
            //zero matrix
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    matrix[i, j] = 0;
            //add nonzero values
            int ja = 0;
            for (int k = 0; k < nonzeros; k++)
            {
                if (k >= col_ptr[ja]) ja++;
                matrix[row_ind[k], ja - 1] = val[k];
            }
            //return matrix
            return matrix;
        }

        public double[] vectorMultiply(double[] vector)
        {
            //auxiliary indices
            int i_start, i_end;
            //resulting vector array
            double[] result = new double[height];
            //zero resulting vector
            for (int i = 0; i < height; i++)
                result[i] = 0;
            //multiplication
            for (int j = 0; j < width; j++)
            {
                i_start = col_ptr[j];
                i_end = col_ptr[j + 1];
                for (int k = i_start; k < i_end; k++)
                    result[row_ind[k]] += val[k] * vector[j];
            }
            return result;
        }

        public string serialize()
        {
            double[,] matrix = this.toMatrix();
            string serialized = "";
            serialized += matrix[0, 0].ToString("N3");
            for (int j = 1; j < width; j++)
                serialized += ' ' + matrix[0, j].ToString("N3");
            for (int i = 1; i < height; i++)
            {
                serialized += System.Environment.NewLine;
                serialized += matrix[i, 0].ToString("N3");
                for (int j = 1; j < width; j++)
                    serialized += ' ' + matrix[i, j].ToString("N3");
            }
            return serialized;
        }
    }
}
