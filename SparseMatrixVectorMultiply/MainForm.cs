using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SparseVectorMatrixMultiply.Benchmarks;
using SparseVectorMatrixMultiply.Representations;


namespace SparseVectorMatrixMultiply
{
    public partial class MainForm : Form
    {
        double[,] matrix;
        double[] vector;

        Benchmark sparcityBenchmark;
        Benchmark sizeBenchmark;

        HiResChartForm chartForm;

        public MainForm()
        {
            InitializeComponent();
            loadStructures();

            M2DButton.Click += new EventHandler(this.M2D_Click);
            JaggedButton.Click += new EventHandler(this.Jagged_Click);
            CRSButton.Click += new EventHandler(this.CRS_Click);
            CCSButton.Click += new EventHandler(this.CCS_Click);
            IncidenceButton.Click += new EventHandler(this.Incidence_Click);

            M2DxButton.Click += new EventHandler(this.M2Dx_Click);
            JaggedxButton.Click += new EventHandler(this.Jaggedx_Click);
            CRSxButton.Click += new EventHandler(this.CRSx_Click);
            CCSxButton.Click += new EventHandler(this.CCSx_Click);
            IncidencexButton.Click += new EventHandler(this.Incidencex_Click);

            RunBench1button.Click += new EventHandler(this.RunBench1_Click);
            SaveBench1button.Click += new EventHandler(this.SaveBench1_Click);
            ExportBench1button.Click += new EventHandler(this.ExportBench1_Click);
            SparcityChart.DoubleClick += new EventHandler(this.SparcityChart_Click);

            RunBench2button.Click += new EventHandler(this.RunBench2_Click);
            SaveBench2button.Click += new EventHandler(this.SaveBench2_Click);
            ExportBench2button.Click += new EventHandler(this.ExportBench2_Click);
            SizeChart.DoubleClick += new EventHandler(this.SizeChart_Click);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //???
        }

        private int loadStructures()
        {
            //load matrix from textboxes
            try
            {
                //parse the matrix string
                string[] separators = new string[] { "},", "} ,", "}{", "} {" };
                string[] matrixLines = matrixTextBox.Text.Split(separators, StringSplitOptions.None);
                string[][] matrixElements = new string[matrixLines.Length][];
                for (int i = 0; i < matrixLines.Length; i++)
                {
                    matrixLines[i] = String.Join("", matrixLines[i].Split('{', '}', ' '));
                    matrixElements[i] = matrixLines[i].Split(',');
                }
                //initialize the matrix
                int height = matrixLines.Length;
                int width = matrixElements[0].Length;
                matrix = new double[height, width];
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                        matrix[i, j] = Convert.ToDouble(matrixElements[i][j]);
            }
            //return if string is in wrong format
            catch
            {
                MessageBox.Show("Error, wrong matrix format.");
                return -1;
            }
            //load vector from textboxes
            try
            {
                //parse the vector string
                string[] separators = new string[] { "," };
                string[] vectorElements = vectorTextBox.Text.Split(separators, StringSplitOptions.None);
                for (int i = 0; i < vectorElements.Length; i++)
                    vectorElements[i] = String.Join("", vectorElements[i].Split('{', '}', ' '));
                //initialize the vector
                int length = vectorElements.Length;
                vector = new double[length];
                for (int i = 0; i < length; i++)
                    vector[i] = Convert.ToDouble(vectorElements[i]);
            }
            //return if string is in wrong format
            catch
            {
                MessageBox.Show("Error, wrong vector format.");
                return -1;
            }
            //return if vector length and matrix width differ
            if (vector.Length != matrix.GetLength(1))
            {
                MessageBox.Show("Error, matrix width and vector length differ.");
                return -1;
            }
            return 0;
        }

        private void M2D_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            string mat = new Dense2D(matrix).serialize();
            resultsTextBox.Text = "Dense Multidimensional format:" + Environment.NewLine + mat;
        }

        private void Jagged_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            string mat = new DenseJagged(matrix).serialize();
            resultsTextBox.Text = "Dense Jagged format:" + Environment.NewLine + mat;
        }

        private void CRS_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            string mat = new CRS(matrix).serialize();
            resultsTextBox.Text = "CRS format:" + Environment.NewLine + mat;
        }

        private void CCS_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            string mat = new CCS(matrix).serialize();
            resultsTextBox.Text = "CCS format:" + Environment.NewLine + mat;
        }

        private void Incidence_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            string mat = new IncidenceList(matrix).serialize();
            resultsTextBox.Text = "Incidence List format:" + Environment.NewLine + mat;
        }

        private void M2Dx_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            resultsTextBox.Text = "Multidimensional Matrix Multiplication:" + Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M =" + Environment.NewLine;
            Dense2D mat = new Dense2D(matrix);
            resultsTextBox.Text += mat.serialize();
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "V =" + Environment.NewLine;
            resultsTextBox.Text += String.Join(" ", vector.Select(x => x.ToString()).ToArray());
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M*V =" + Environment.NewLine;
            double[] res = mat.vectorMultiply(vector);
            resultsTextBox.Text += String.Join(" ", res.Select(x => x.ToString()).ToArray());
        }

        private void Jaggedx_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            resultsTextBox.Text = "Jagged Matrix Multiplication:" + Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M =" + Environment.NewLine;
            DenseJagged mat = new DenseJagged(matrix);
            resultsTextBox.Text += mat.serialize();
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "V =" + Environment.NewLine;
            resultsTextBox.Text += String.Join(" ", vector.Select(x => x.ToString()).ToArray());
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M*V =" + Environment.NewLine;
            double[] res = mat.vectorMultiply(vector);
            resultsTextBox.Text += String.Join(" ", res.Select(x => x.ToString()).ToArray());
        }

        private void CRSx_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            resultsTextBox.Text = "CRS Matrix Multiplication:" + Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M =" + Environment.NewLine;
            CRS mat = new CRS(matrix);
            resultsTextBox.Text += mat.serialize();
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "V =" + Environment.NewLine;
            resultsTextBox.Text += String.Join(" ", vector.Select(x => x.ToString()).ToArray());
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M*V =" + Environment.NewLine;
            double[] res = mat.vectorMultiply(vector);
            resultsTextBox.Text += String.Join(" ", res.Select(x => x.ToString()).ToArray());
        }

        private void CCSx_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            resultsTextBox.Text = "CCS Matrix Multiplication:" + Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M =" + Environment.NewLine;
            CCS mat = new CCS(matrix);
            resultsTextBox.Text += mat.serialize();
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "V =" + Environment.NewLine;
            resultsTextBox.Text += String.Join(" ", vector.Select(x => x.ToString()).ToArray());
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M*V =" + Environment.NewLine;
            double[] res = mat.vectorMultiply(vector);
            resultsTextBox.Text += String.Join(" ", res.Select(x => x.ToString()).ToArray());
        }

        private void Incidencex_Click(object sender, EventArgs e)
        {
            if (loadStructures() != 0) return;
            resultsTextBox.Text = "Incidence List Matrix Multiplication:" + Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M =" + Environment.NewLine;
            IncidenceList mat = new IncidenceList(matrix);
            resultsTextBox.Text += mat.serialize();
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "V =" + Environment.NewLine;
            resultsTextBox.Text += String.Join(" ", vector.Select(x => x.ToString()).ToArray());
            resultsTextBox.Text += Environment.NewLine + Environment.NewLine;
            resultsTextBox.Text += "M*V =" + Environment.NewLine;
            double[] res = mat.vectorMultiply(vector);
            resultsTextBox.Text += String.Join(" ", res.Select(x => x.ToString()).ToArray());
        }

        private void RunBench1_Click(object sender, EventArgs e)
        {
            //read user controls
            double sparcityInitial = (double)numericUpDown1.Value / 100;
            double sparcityFinal = (double)numericUpDown2.Value / 100;
            double sparcityStep = (double)numericUpDown3.Value / 100;
            int dimension = (int)numericUpDown4.Value;
            double time = 1000*(double)numericUpDown5.Value;
            SparcityChart.ChartAreas[0].AxisX.Minimum = (double)numericUpDown1.Value;
            SparcityChart.ChartAreas[0].AxisX.Maximum = Double.NaN;
            //initialize benchmark
            sparcityBenchmark = new Benchmark();
            sparcityBenchmark.init(sparcityInitial, sparcityStep, sparcityFinal, dimension, time);
            //run benchmark
            Bench1Status.Text = "STARTED";
            clearSparcityChart();
            Application.DoEvents();
            int run = 1;
            while (run == 1)
            {
                run = sparcityBenchmark.runNextStep();
                Bench1Status.Text = "STATUS:  " + sparcityBenchmark.getStepInfo();
                updateSparcityChart(sparcityBenchmark);
                Application.DoEvents();
            }
            SparcityChart.ChartAreas[0].AxisX.Maximum = (double)numericUpDown2.Value;
            //save stats
            Bench1Status.Text = "Saving stats to output-sparcity.txt";
            sparcityBenchmark.saveStats("output-sparcity.txt");
            //end
            SaveBench1button.Enabled = true;
            ExportBench1button.Enabled = true;
            SparcityChart.Enabled = true;
            Bench1Status.Text = "FINISHED";
        }

        private void RunBench2_Click(object sender, EventArgs e)
        {
            //read user controls
            int sizeInitial = (int)numericUpDown9.Value;
            int sizeFinal = (int)numericUpDown8.Value;
            int sizeStep = (int)numericUpDown7.Value;
            double sparcity = (double)numericUpDown10.Value / 100;
            double time = 1000 * (double)numericUpDown6.Value;
            SizeChart.ChartAreas[0].AxisX.Minimum = sizeInitial;
            SizeChart.ChartAreas[0].AxisX.Maximum = Double.NaN;
            //initialize benchmark
            sizeBenchmark = new Benchmark();
            sizeBenchmark.init(sizeInitial, sizeStep, sizeFinal, sparcity, time);
            //run benchmark
            Bench2Status.Text = "STARTED";
            clearSizeChart();
            Application.DoEvents();
            int run = 1;
            while (run == 1)
            {
                run = sizeBenchmark.runNextStep();
                Bench2Status.Text = "STATUS:  " + sizeBenchmark.getStepInfo();
                updateSizeChart(sizeBenchmark);
                Application.DoEvents();
            }
            SizeChart.ChartAreas[0].AxisX.Maximum = sizeFinal;
            //save stats
            Bench2Status.Text = "Saving stats to output-size.txt";
            sizeBenchmark.saveStats("output-size.txt");
            //end
            SaveBench2button.Enabled = true;
            ExportBench2button.Enabled = true;
            SizeChart.Enabled = true;
            Bench2Status.Text = "FINISHED";
        }

        private void clearSparcityChart()
        {
            for (int i = 0; i < 5; i++)
                SparcityChart.Series[i].Points.Clear();
        }

        private void updateSparcityChart(Benchmark benchmark)
        {
            double x, y;
            int step = benchmark.stepNum - 1;
            //update Dense 2D series
            x = 100 * benchmark.methodStats[2].step[step].matrixSparcity;
            y = benchmark.methodStats[2].step[step].flops;
            SparcityChart.Series[0].Points.Add(new DataPoint(x,y));
            //update Dense Jagged series
            x = 100 * benchmark.methodStats[3].step[step].matrixSparcity;
            y = benchmark.methodStats[3].step[step].flops;
            SparcityChart.Series[1].Points.Add(new DataPoint(x, y));
            //update CRS series
            x = 100 * benchmark.methodStats[1].step[step].matrixSparcity;
            y = benchmark.methodStats[1].step[step].flops;
            SparcityChart.Series[2].Points.Add(new DataPoint(x, y));
            //update CCS series
            x = 100 * benchmark.methodStats[0].step[step].matrixSparcity;
            y = benchmark.methodStats[0].step[step].flops;
            SparcityChart.Series[3].Points.Add(new DataPoint(x, y));
            //update CCS series
            x = 100 * benchmark.methodStats[4].step[step].matrixSparcity;
            y = benchmark.methodStats[4].step[step].flops;
            SparcityChart.Series[4].Points.Add(new DataPoint(x, y));
        }

        private void clearSizeChart()
        {
            for (int i = 0; i < 5; i++)
                SizeChart.Series[i].Points.Clear();
        }

        private void updateSizeChart(Benchmark benchmark)
        {
            double x, y;
            int step = benchmark.stepNum - 1;
            //update Dense 2D series
            x = benchmark.methodStats[2].step[step].matrixDimension;
            y = benchmark.methodStats[2].step[step].flops;
            SizeChart.Series[0].Points.Add(new DataPoint(x, y));
            //update Dense Jagged series
            x = benchmark.methodStats[3].step[step].matrixDimension;
            y = benchmark.methodStats[3].step[step].flops;
            SizeChart.Series[1].Points.Add(new DataPoint(x, y));
            //update CRS series
            x = benchmark.methodStats[1].step[step].matrixDimension;
            y = benchmark.methodStats[1].step[step].flops;
            SizeChart.Series[2].Points.Add(new DataPoint(x, y));
            //update CCS series
            x = benchmark.methodStats[0].step[step].matrixDimension;
            y = benchmark.methodStats[0].step[step].flops;
            SizeChart.Series[3].Points.Add(new DataPoint(x, y));
            //update CCS series
            x = benchmark.methodStats[4].step[step].matrixDimension;
            y = benchmark.methodStats[4].step[step].flops;
            SizeChart.Series[4].Points.Add(new DataPoint(x, y));
        }

        private void ExportBench2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chartForm = new HiResChartForm();
                chartForm.DrawChart(sizeBenchmark, "size");
                chartForm.HiResolutionChart.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
            }
        }

        private void ExportBench1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chartForm = new HiResChartForm();
                chartForm.DrawChart(sparcityBenchmark, "sparcity");
                chartForm.HiResolutionChart.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
            }
        }

        private void SaveBench2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                sizeBenchmark.saveStats(saveFileDialog1.FileName);
        }

        private void SaveBench1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                sparcityBenchmark.saveStats(saveFileDialog1.FileName);
        }

        private void SizeChart_Click(object sender, EventArgs e)
        {
            chartForm = new HiResChartForm();
            chartForm.DrawChart(sizeBenchmark, "size");
            chartForm.Show();
        }

        private void SparcityChart_Click(object sender, EventArgs e)
        {
            chartForm = new HiResChartForm();
            chartForm.DrawChart(sparcityBenchmark, "sparcity");
            chartForm.Show();
        }
    }
}
