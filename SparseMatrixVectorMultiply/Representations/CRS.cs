using SparseVectorMatrixMultiply.Interfaces;

namespace SparseVectorMatrixMultiply.Representations
{
    /// <summary>
    /// Compressed Row Storage - https://en.wikipedia.org/wiki/Sparse_matrix#Compressed_sparse_row_.28CSR.2C_CRS_or_Yale_format.29
    /// </summary>
    class CRS : IMatrixConvertible, IVectorMultipliable, ISerializable
    {
        //matrix dimensions
        public int width;
        public int height;
        public int nonzeros;

        //CRS arrays
        public double[] A;  //holds all the nonzero entries of matrix (left-to-right, top-to-bottom)
        public int[] IA;    //IA[i] contains number of all nonzero elements before i-th row
        public int[] JA;    //contains the column index in matrix for each element of A

        public CRS(double [,] matrix)
        {
            init(matrix);
        }

        public CRS(IMatrixConvertible matrixRepresentation)
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
            //initialize CRS arrays
            A = new double[nonzeros];
            IA = new int[height + 1];
            JA = new int[nonzeros];
            //convert matrix to CRS
            int k = 0;
            IA[0] = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        A[k] = matrix[i, j];
                        JA[k] = j;
                        k++;
                    }
                }
                IA[i + 1] = k;
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
            int ia = 0;
            for (int k = 0; k < nonzeros; k++)
            {
                if (k >= IA[ia]) ia++;
                matrix[ia-1, JA[k]] = A[k];
            }
            //return matrix
            return matrix;
        }

        public double[] vectorMultiply(double[] vector)
        {
            //auxiliary indices
            int j_start, j_end;
            //resulting vector array
            double[] result = new double[height];
            //multiplication
            for (int i = 0; i < height; i++)
            {
                result[i] = 0;
                j_start = IA[i];
                j_end = IA[i+1];
                for (int k = j_start; k < j_end; k++)
                    result[i] += A[k] * vector[JA[k]];
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
