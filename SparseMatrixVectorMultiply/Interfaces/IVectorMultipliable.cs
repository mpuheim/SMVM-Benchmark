using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparseVectorMatrixMultiply.Interfaces
{
    public interface IVectorMultipliable
    {
        double[] vectorMultiply(double[] vector);
    }
}
