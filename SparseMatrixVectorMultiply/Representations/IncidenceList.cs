using SparseVectorMatrixMultiply.Interfaces;

namespace SparseVectorMatrixMultiply.Representations
{
    class IncidenceList : IMatrixConvertible, IVectorMultipliable, ISerializable
    {
        //matrix dimensions
        public int width;
        public int height;
        public int[] nonzeros;

        //storage using graph representation
        public double[][] edges;  //nonzero matrix values
        public int[][] verteces;  //indices of related verteces

        public IncidenceList(double[,] matrix)
        {
            init(matrix);
        }

        public IncidenceList(IMatrixConvertible matrixRepresentation)
        {
            init(matrixRepresentation.toMatrix());
        }

        public void init(double[,] matrix)
        {
            //get matrix dimensions
            height = matrix.GetLength(0);
            width = matrix.GetLength(1);
            //calculate nonzero value count for each row;
            nonzeros = new int[height];
            for (int i = 0; i < height; i++)
            {
                nonzeros[i] = 0;
                for (int j = 0; j < width; j++)
                    if (matrix[i, j] != 0) nonzeros[i]++;
            }
            //initialize the storage
            edges = new double[height][];
            verteces = new int[height][];
            for (int i = 0; i < height; i++)
            {
                edges[i] = new double[nonzeros[i]];
                verteces[i] = new int[nonzeros[i]];
                if (nonzeros[i] > 0)
                {
                    int k = 0;
                    for (int j = 0; j < width; j++)
                    {
                        if (matrix[i, j] != 0)
                        {
                            edges[i][k] = matrix[i, j];
                            verteces[i][k] = j;
                            k++;
                        }
                    }
                }
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
            for (int i = 0; i < height; i++)
                for (int k = 0; k < nonzeros[i]; k++)
                    matrix[i, verteces[i][k]] = edges[i][k];
            //return matrix
            return matrix;
        }

        public double[] vectorMultiply(double[] vector)
        {
            //auxiliary variables
            int edges_i_len = 0;
            double[] edges_i;
            int[] verteces_i;
            //resulting vector array
            double[] result = new double[height];
            //multiplication
            for (int i = 0; i < height; i++)
            {
                edges_i_len = nonzeros[i];
                edges_i = edges[i];
                verteces_i = verteces[i];
                result[i] = 0;
                for (int k = 0; k < edges_i_len; k++)
                    result[i] += edges_i[k] * vector[verteces_i[k]];
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
