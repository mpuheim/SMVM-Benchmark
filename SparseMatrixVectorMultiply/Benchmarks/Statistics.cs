using System.Collections.Generic;

namespace SparseVectorMatrixMultiply.Benchmarks
{
    public class Statistics
    {
        public string methodName;
        public int stepCount;
        public List<StepStats> step;
        public double runtime;

        public Statistics(string method)
        {
            methodName = method;
            stepCount = 0;
            step = new List<StepStats>();
            runtime = 0;
        }

        public void add(int stepNum, double stepTime, int matrixDimension, double matrixSparcity, int ops, double flops)
        {
            stepCount++;
            step.Add(new StepStats(stepNum, stepTime, matrixDimension, matrixSparcity, ops, flops));
            runtime += stepTime;
        }
    }

    public class StepStats
    {
        public int stepNum;
        public double stepTime;
        public int matrixDimension;
        public double matrixSparcity;
        public int operations;
        public double flops;

        public StepStats(int stepNum, double stepTime, int mDimension, double mSparcity, int ops, double flops)
        {
            this.stepNum = stepNum;
            this.stepTime = stepTime;
            this.matrixDimension = mDimension;
            this.matrixSparcity = mSparcity;
            this.operations = ops;
            this.flops = flops;
        }
    }
}

