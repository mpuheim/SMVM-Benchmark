using SparseVectorMatrixMultiply.Representations;
using SparseVectorMatrixMultiply.Interfaces;

namespace SparseVectorMatrixMultiply.Conversion
{
    public static class MatrixConvertor
    {
        public static IVectorMultipliable convert(IMatrixConvertible matrix, string representation)
        {
            switch (representation)
            {
                case "CCS":
                    return new CCS(matrix);
                case "CRS":
                    return new CRS(matrix);
                case "Dense2D":
                    return new Dense2D(matrix);
                case "DenseJagged":
                    return new DenseJagged(matrix);
                case "IncidenceList":
                    return new IncidenceList(matrix);
                default:
                    return null;
            }
        }

        public static IVectorMultipliable convert(double[,] matrix, string representation)
        {
            switch (representation)
            {
                case "CCS":
                    return new CCS(matrix);
                case "CRS":
                    return new CRS(matrix);
                case "Dense2D":
                    return new Dense2D(matrix);
                case "DenseJagged":
                    return new DenseJagged(matrix);
                case "IncidenceList":
                    return new IncidenceList(matrix);
                default:
                    return null;
            }
        }
    }
}
