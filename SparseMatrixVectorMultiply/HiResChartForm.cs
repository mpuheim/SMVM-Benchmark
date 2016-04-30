using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SparseVectorMatrixMultiply.Benchmarks;

namespace SparseVectorMatrixMultiply
{
    public partial class HiResChartForm : Form
    {
        public Chart HiResolutionChart;

        public HiResChartForm()
        {
            InitializeComponent();
            HiResolutionChart = HiResChart;
        }

        public void DrawChart(Benchmark benchmark, string type)
        {
            //clear series
            HiResChart.Series.Clear();
            //add series from benchmark
            foreach (var stats in benchmark.methodStats)
            {
                var series = new Series(stats.methodName);
                series.ChartType = SeriesChartType.Spline;
                series.BorderWidth = 4;
                //add points
                switch (type)
                {
                    case "size":
                        foreach (var step in stats.step)
                        {
                            double x = step.matrixDimension;
                            double y = step.flops;
                            series.Points.Add(new DataPoint(x, y));
                        }
                        break;
                    case "sparcity":
                        foreach (var step in stats.step)
                        {
                            double x = 100 * step.matrixSparcity;
                            double y = step.flops;
                            series.Points.Add(new DataPoint(x, y));
                        }
                        break;
                }
                //add series to the chart
                HiResChart.Series.Add(series);
            }
            //add legend
            var legend = new Legend();
            legend.Alignment = StringAlignment.Center;
            legend.Docking = Docking.Top;
            legend.Name = "Legend";
            HiResChart.Legends.Add(legend);
            //add axis labels and range
            HiResChart.ChartAreas[0].AxisY.Title = "Flops";
            int t = benchmark.methodStats[0].step.Count - 1;
            double min = 0;
            double max = 1;
            switch (type)
            {
                case "size":
                    HiResChart.ChartAreas[0].AxisX.Title = "Size";
                    min = benchmark.methodStats[0].step[0].matrixDimension;
                    max = benchmark.methodStats[0].step[t].matrixDimension;
                    break;
                case "sparcity":
                    HiResChart.ChartAreas[0].AxisX.Title = "Sparcity (%)";
                    min = 100 * benchmark.methodStats[0].step[0].matrixSparcity;
                    max = 100 * benchmark.methodStats[0].step[t].matrixSparcity;
                    break;
            }
            HiResChart.ChartAreas[0].AxisX.Minimum = min;
            HiResChart.ChartAreas[0].AxisX.Maximum = max;
            //change fonts
            Font font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            HiResChart.Legends[0].Font = font;
            HiResChart.ChartAreas[0].AxisX.TitleFont = font;
            HiResChart.ChartAreas[0].AxisY.TitleFont = font;
            HiResChart.ChartAreas[0].AxisX.LabelStyle.Font = font;
            HiResChart.ChartAreas[0].AxisY.LabelStyle.Font = font;
        }
    }
}
