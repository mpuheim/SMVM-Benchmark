using System.Collections.Generic;
using System.Diagnostics;
using SparseVectorMatrixMultiply.Interfaces;
using SparseVectorMatrixMultiply.Generators;
using SparseVectorMatrixMultiply.Conversion;

namespace SparseVectorMatrixMultiply.Benchmarks
{
    public class Benchmark
    {
        //benchmark variables
        public int stepNum;
        public double stepTime;
        public double elapsedTime;
        public string type;
        //method statistics
        public Statistics[] methodStats;
        //matrix properties
        public double sparcityInitial;
        public double sparcityCurrent;
        public double sparcityStep;
        public double sparcityFinal;
        public int dimensionInitial;
        public int dimensionCurrent;
        public int dimensionStep;
        public int dimensionFinal;
        //generators
        public RandomMatrixGenerator matGen;
        public RandomVectorGenerator vecGen;

        public Benchmark()
        {
            methodStats = new Statistics[5];
            matGen = new RandomMatrixGenerator();
            vecGen = new RandomVectorGenerator();
        }

        public void init(double sparcityInitial, double sparcityStep, double sparcityFinal, int dimension, double time)
        {
            this.init();
            this.sparcityInitial = sparcityInitial;
            this.sparcityCurrent = sparcityInitial;
            this.sparcityStep = sparcityStep;
            this.sparcityFinal = sparcityFinal;
            this.dimensionInitial = dimension;
            this.dimensionCurrent = dimension;
            this.dimensionStep = 1;
            this.dimensionFinal = dimension;
            this.stepTime = time / ((sparcityFinal - sparcityInitial) / sparcityStep);
            this.type = "SparcityBenchmark";
        }

        public void init(int dimensionInitial, int dimensionStep, int dimensionFinal, double sparcity, double time)
        {
            this.init();
            this.sparcityInitial = sparcity;
            this.sparcityCurrent = sparcity;
            this.sparcityStep = 1;
            this.sparcityFinal = sparcity;
            this.dimensionInitial = dimensionInitial;
            this.dimensionCurrent = dimensionInitial;
            this.dimensionStep = dimensionStep;
            this.dimensionFinal = dimensionFinal;
            this.stepTime = time / ((double)(dimensionFinal - dimensionInitial) / (double)dimensionStep);
            this.type = "DimensionBenchmark";
        }

        private void init()
        {
            methodStats[0] = new Statistics("CCS");
            methodStats[1] = new Statistics("CRS");
            methodStats[2] = new Statistics("Dense2D");
            methodStats[3] = new Statistics("DenseJagged");
            methodStats[4] = new Statistics("IncidenceList");
            stepNum = 0;
            elapsedTime = 0;
        }

        public string getStepInfo()
        {
            string s_stpNum = "STEP: " + stepNum.ToString() + " | ";
            string s_matDim = "SIZE " + dimensionCurrent.ToString() + " | ";
            string s_matSpr = "SPARCITY " + ((int)(100*sparcityCurrent)).ToString() + " % | ";
            string s_elTime = "TIME " + (elapsedTime/1000).ToString() + " seconds";
            return s_stpNum + s_matDim + s_matSpr + s_elTime;
        }

        public int runNextStep()
        {
            //set global time counter
            Stopwatch gStopWatch = Stopwatch.StartNew();
            //get counters for each method
            Stopwatch[] mStopWatch = new Stopwatch[5];
            double[] mTime = new double[5];
            int[] mOperations = new int[5];
            for (int m = 0; m < 5; m++)
            {
                mStopWatch[m] = new Stopwatch();
                mTime[m] = 0;
                mOperations[m] = 0;
            }
            //run benchmark
            double currentTime;
            do
            {
                //get test matrix & vector
                int nonzeros = (int)(dimensionCurrent * dimensionCurrent * sparcityCurrent);
                double[,] randomMatrix = matGen.nextMatrix(dimensionCurrent, dimensionCurrent, nonzeros);
                double[] vector = vecGen.nextVector(dimensionCurrent, dimensionCurrent);
                //for each method
                for (int m = 0; m < 5; m++)
                {
                    //convert matrix for current representation/method
                    IVectorMultipliable matrix = MatrixConvertor.convert(randomMatrix, methodStats[m].methodName);
                    //get current method stopwatch
                    Stopwatch stopWatch = mStopWatch[m];
                    //perform multiplication
                    stopWatch.Start();
                    matrix.vectorMultiply(vector);
                    stopWatch.Stop();
                    //increase operations counter
                    mOperations[m] += 1;
                    //update time counter
                    mTime[m] = stopWatch.ElapsedMilliseconds;
                }
            }
            while (gStopWatch.ElapsedMilliseconds < stepTime);
            //save stats
            elapsedTime += gStopWatch.ElapsedMilliseconds;
            for (int m = 0; m < 5; m++)
            {
                //calculate operations per second
                double mFlops = 1000 * mOperations[m] / mTime[m];
                //save statistics
                methodStats[m].add(stepNum, mTime[m], dimensionCurrent, sparcityCurrent, mOperations[m], mFlops);
            }
            //increase step counter
            stepNum++;
            //change dimension or sparcity of matrix
            if (sparcityCurrent < sparcityFinal)
            {
                sparcityCurrent += sparcityStep;
            }
            else if (dimensionCurrent < dimensionFinal)
            {
                dimensionCurrent += dimensionStep;
                sparcityCurrent = sparcityInitial;
            }
            else
                //end benchmark
                return 0;
            //finish step
            return 1;
        }

        public void saveStats(string path = "SMVM-Stats.txt")
        {
            List<string> lines = new List<string>();
            foreach (var method in methodStats)
            {
                lines.Add(method.methodName);
                lines.Add(" - runtime: " + method.runtime);
                lines.Add(" - steps:   " + method.stepCount);
                lines.Add(" - stats:");
                lines.Add("STEP | TIME | DIMENSION | SPARCITY | OPERATIONS | FLOPS");
                foreach (var stat in method.step)
                {
                    lines.Add(stat.stepNum
                    + " | " + ((int)stat.stepTime).ToString()
                    + " | " + stat.matrixDimension.ToString()
                    + " | " + ((int)(100*stat.matrixSparcity)).ToString() + " %"
                    + " | " + stat.operations.ToString()
                    + " | " + stat.flops.ToString("N3"));
                }
                lines.Add(" ");
            }
            System.IO.File.WriteAllLines(path, lines);
        }
    }
}
