using SparseVectorMatrixMultiply.Interfaces;

namespace SparseVectorMatrixMultiply.Representations
{
    class Dense2D : IMatrixConvertible, IVectorMultipliable, ISerializable
    {
        //matrix dimensions
        public int width;
        public int height;

        //Dense 2D matrix storage
        public double[,] matrix;

        public Dense2D(double[,] matrix)
        {
            init(matrix);
        }

        public Dense2D(IMatrixConvertible matrixRepresentation)
        {
            init(matrixRepresentation.toMatrix());
        }

        private void init(double[,] matrix)
        {
            //get matrix dimensions
            height = matrix.GetLength(0);
            width = matrix.GetLength(1);
            //initialize the storage
            this.matrix = new double[height, width];
            //copy matrix to storage
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    this.matrix[i, j] = matrix[i, j];
        }

        public double[,] toMatrix()
        {
            double[,] new_matrix = new double[height, width];
            //copy storage to new matrix
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    new_matrix[i, j] = this.matrix[i, j];
            //return new matrix
            return new_matrix;
        }

        public double[] vectorMultiply(double[] vector)
        {
            double[] result = new double[height];
            for (int i = 0; i < height; i++)
            {
                result[i] = 0;
                for (int j = 0; j < width; j++)
                    result[i] += matrix[i, j] * vector[j];
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
