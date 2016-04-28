namespace SparseVectorMatrixMultiply
{
    partial class HiResChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.HiResChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.HiResChart)).BeginInit();
            this.SuspendLayout();
            // 
            // HiResChart
            // 
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.MajorGrid.LineWidth = 2;
            chartArea1.AxisX.MajorTickMark.LineWidth = 2;
            chartArea1.AxisX.MajorTickMark.Size = 2F;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.AxisY.MajorGrid.LineWidth = 2;
            chartArea1.AxisY.MajorTickMark.LineWidth = 2;
            chartArea1.AxisY.MajorTickMark.Size = 2F;
            chartArea1.Name = "ChartArea";
            this.HiResChart.ChartAreas.Add(chartArea1);
            this.HiResChart.Location = new System.Drawing.Point(20, 18);
            this.HiResChart.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.HiResChart.Name = "HiResChart";
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Name = "Series3";
            series4.ChartArea = "ChartArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Name = "Series4";
            this.HiResChart.Series.Add(series1);
            this.HiResChart.Series.Add(series2);
            this.HiResChart.Series.Add(series3);
            this.HiResChart.Series.Add(series4);
            this.HiResChart.Size = new System.Drawing.Size(900, 700);
            this.HiResChart.TabIndex = 0;
            this.HiResChart.Text = "Chart";
            // 
            // HiResChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 732);
            this.Controls.Add(this.HiResChart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "HiResChartForm";
            this.Text = "Chart";
            ((System.ComponentModel.ISupportInitialize)(this.HiResChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart HiResChart;
    }
}