namespace VisualSniffer
{
    partial class statistics
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.avgMin = new AJBauer.AGauge();
            this.avgSec = new AJBauer.AGauge();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.avgMin);
            this.splitContainer1.Panel1.Controls.Add(this.avgSec);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Size = new System.Drawing.Size(690, 581);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(480, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Avg(min)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Avg(sec)";
            // 
            // avgMin
            // 
            this.avgMin.BaseArcColor = System.Drawing.Color.Gray;
            this.avgMin.BaseArcRadius = 80;
            this.avgMin.BaseArcStart = 135;
            this.avgMin.BaseArcSweep = 270;
            this.avgMin.BaseArcWidth = 2;
            this.avgMin.Cap_Idx = ((byte)(1));
            this.avgMin.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.avgMin.CapPosition = new System.Drawing.Point(10, 10);
            this.avgMin.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.avgMin.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.avgMin.CapText = "";
            this.avgMin.Center = new System.Drawing.Point(100, 100);
            this.avgMin.Location = new System.Drawing.Point(402, 12);
            this.avgMin.MaxValue = 400F;
            this.avgMin.MinValue = 0F;
            this.avgMin.Name = "avgMin";
            this.avgMin.NeedleColor1 = AJBauer.AGauge.NeedleColorEnum.Gray;
            this.avgMin.NeedleColor2 = System.Drawing.Color.DimGray;
            this.avgMin.NeedleRadius = 80;
            this.avgMin.NeedleType = 0;
            this.avgMin.NeedleWidth = 2;
            this.avgMin.Range_Idx = ((byte)(0));
            this.avgMin.RangeColor = System.Drawing.Color.LightGreen;
            this.avgMin.RangeEnabled = true;
            this.avgMin.RangeEndValue = 300F;
            this.avgMin.RangeInnerRadius = 70;
            this.avgMin.RangeOuterRadius = 80;
            this.avgMin.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Red,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.avgMin.RangesEnabled = new bool[] {
        true,
        true,
        false,
        false,
        false};
            this.avgMin.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.avgMin.RangesInnerRadius = new int[] {
        70,
        70,
        70,
        70,
        70};
            this.avgMin.RangesOuterRadius = new int[] {
        80,
        80,
        80,
        80,
        80};
            this.avgMin.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.avgMin.RangeStartValue = -100F;
            this.avgMin.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.avgMin.ScaleLinesInterInnerRadius = 73;
            this.avgMin.ScaleLinesInterOuterRadius = 80;
            this.avgMin.ScaleLinesInterWidth = 1;
            this.avgMin.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.avgMin.ScaleLinesMajorInnerRadius = 70;
            this.avgMin.ScaleLinesMajorOuterRadius = 80;
            this.avgMin.ScaleLinesMajorStepValue = 50F;
            this.avgMin.ScaleLinesMajorWidth = 2;
            this.avgMin.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.avgMin.ScaleLinesMinorInnerRadius = 75;
            this.avgMin.ScaleLinesMinorNumOf = 9;
            this.avgMin.ScaleLinesMinorOuterRadius = 80;
            this.avgMin.ScaleLinesMinorWidth = 1;
            this.avgMin.ScaleNumbersColor = System.Drawing.Color.Black;
            this.avgMin.ScaleNumbersFormat = null;
            this.avgMin.ScaleNumbersRadius = 95;
            this.avgMin.ScaleNumbersRotation = 0;
            this.avgMin.ScaleNumbersStartScaleLine = 0;
            this.avgMin.ScaleNumbersStepScaleLines = 1;
            this.avgMin.Size = new System.Drawing.Size(207, 191);
            this.avgMin.TabIndex = 1;
            this.avgMin.Text = "aGauge2";
            this.avgMin.Value = 0F;
            // 
            // avgSec
            // 
            this.avgSec.BaseArcColor = System.Drawing.Color.Gray;
            this.avgSec.BaseArcRadius = 80;
            this.avgSec.BaseArcStart = 135;
            this.avgSec.BaseArcSweep = 270;
            this.avgSec.BaseArcWidth = 2;
            this.avgSec.Cap_Idx = ((byte)(1));
            this.avgSec.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.avgSec.CapPosition = new System.Drawing.Point(10, 10);
            this.avgSec.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.avgSec.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.avgSec.CapText = "";
            this.avgSec.Center = new System.Drawing.Point(100, 100);
            this.avgSec.Location = new System.Drawing.Point(53, 12);
            this.avgSec.MaxValue = 400F;
            this.avgSec.MinValue = 0F;
            this.avgSec.Name = "avgSec";
            this.avgSec.NeedleColor1 = AJBauer.AGauge.NeedleColorEnum.Gray;
            this.avgSec.NeedleColor2 = System.Drawing.Color.DimGray;
            this.avgSec.NeedleRadius = 80;
            this.avgSec.NeedleType = 0;
            this.avgSec.NeedleWidth = 2;
            this.avgSec.Range_Idx = ((byte)(0));
            this.avgSec.RangeColor = System.Drawing.Color.LightGreen;
            this.avgSec.RangeEnabled = true;
            this.avgSec.RangeEndValue = 300F;
            this.avgSec.RangeInnerRadius = 70;
            this.avgSec.RangeOuterRadius = 80;
            this.avgSec.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Red,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.avgSec.RangesEnabled = new bool[] {
        true,
        true,
        false,
        false,
        false};
            this.avgSec.RangesEndValue = new float[] {
        300F,
        400F,
        0F,
        0F,
        0F};
            this.avgSec.RangesInnerRadius = new int[] {
        70,
        70,
        70,
        70,
        70};
            this.avgSec.RangesOuterRadius = new int[] {
        80,
        80,
        80,
        80,
        80};
            this.avgSec.RangesStartValue = new float[] {
        -100F,
        300F,
        0F,
        0F,
        0F};
            this.avgSec.RangeStartValue = -100F;
            this.avgSec.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.avgSec.ScaleLinesInterInnerRadius = 73;
            this.avgSec.ScaleLinesInterOuterRadius = 80;
            this.avgSec.ScaleLinesInterWidth = 1;
            this.avgSec.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.avgSec.ScaleLinesMajorInnerRadius = 70;
            this.avgSec.ScaleLinesMajorOuterRadius = 80;
            this.avgSec.ScaleLinesMajorStepValue = 50F;
            this.avgSec.ScaleLinesMajorWidth = 2;
            this.avgSec.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.avgSec.ScaleLinesMinorInnerRadius = 75;
            this.avgSec.ScaleLinesMinorNumOf = 9;
            this.avgSec.ScaleLinesMinorOuterRadius = 80;
            this.avgSec.ScaleLinesMinorWidth = 1;
            this.avgSec.ScaleNumbersColor = System.Drawing.Color.Black;
            this.avgSec.ScaleNumbersFormat = null;
            this.avgSec.ScaleNumbersRadius = 95;
            this.avgSec.ScaleNumbersRotation = 0;
            this.avgSec.ScaleNumbersStartScaleLine = 0;
            this.avgSec.ScaleNumbersStepScaleLines = 1;
            this.avgSec.Size = new System.Drawing.Size(294, 191);
            this.avgSec.TabIndex = 0;
            this.avgSec.Text = "aGauge1";
            this.avgSec.Value = 0F;
            // 
            // chart1
            // 
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 45;
            chartArea1.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea1.Area3DStyle.Rotation = 40;
            chartArea1.Name = "packetTypeCnt";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "packetTypeCnt";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.SmartLabelStyle.Enabled = false;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(690, 347);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 581);
            this.Controls.Add(this.splitContainer1);
            this.Name = "statistics";
            this.Text = "statistics";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AJBauer.AGauge avgMin;
        private AJBauer.AGauge avgSec;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;


    }
}