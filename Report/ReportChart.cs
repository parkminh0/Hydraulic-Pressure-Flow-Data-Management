using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.Drawing;
using DevExpress.XtraPrinting;

namespace WooSungEngineering
{
    public partial class ReportChart : DevExpress.XtraReports.UI.XtraReport
    {
        private XYDiagram diagram;
        private DataTable dt;
        private double flowAvg1;
        private double flowAvg2;
        private double pressureAvg1;
        private double pressureAvg2;
        private bool flow1chk;
        private bool pressure1chk;
        private bool flow2chk;
        private bool pressure2chk;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">차트 데이터</param>
        /// <param name="radioIndex">데이터 시간 간격</param>
        public ReportChart(DataTable dt, int radioIndex, double FlowAvg1, double PressureAvg1, double FlowAvg2, double PressureAvg2, bool flow1chk, bool pressure1chk, bool flow2chk, bool pressure2chk)
        {
            InitializeComponent();

            xrPictureBox1.Image = LogicManager.Common.ByteArrayToImage(Program.Option.ReportImage);
            diagram = xrChart1.Diagram as XYDiagram;
            this.dt = dt;
            this.flowAvg1 = FlowAvg1;
            this.flowAvg2 = FlowAvg2;
            this.pressureAvg1 = PressureAvg1;
            this.pressureAvg2 = PressureAvg2;
            this.flow1chk = flow1chk;
            this.pressure1chk = pressure1chk;
            this.flow2chk = flow2chk;
            this.pressure2chk = pressure2chk;

            SetAxisX(radioIndex);
            SetChart();
        }

        /// <summary>
        /// X축 제목 및 간격 설정
        /// </summary>
        /// <param name="chartData"></param>
        /// <returns></returns>
        private void SetAxisX(int radioIndex)
        {
            diagram.AxisX.Title.Text = LangResx.Main.ChartAxisX;

            switch (radioIndex)
            {
                case 0:
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 0.5;
                    diagram.AxisX.Title.Text += "(0.5 min)";
                    break;
                case 1:
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 1;
                    diagram.AxisX.Title.Text += "(1 min)";
                    break;
                case 2:
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 2;
                    diagram.AxisX.Title.Text += "(2 min)";
                    break;
                case 3:
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 5;
                    diagram.AxisX.Title.Text += "(5 min)";
                    break;
            }
        }

        /// <summary>
        /// 차트 세팅
        /// </summary>
        private void SetChart()
        {
            // 유량1
            if (flow1chk)
            {
                Series flow1 = xrChart1.Series[0];
                flow1.ArgumentDataMember = "TimeGap";
                flow1.ValueDataMembers.AddRange("Flow1");
                flow1.DataSource = dt;
                flow1.Name = LangResx.Main.Series0;
                diagram.AxisY.Title.Text = LangResx.Main.AxisY0;
            }
            else
            {
                xrChart1.Series[0].Visible = false;
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            // 압력1
            if (pressure1chk)
            {
                Series pressure1 = xrChart1.Series[1];
                pressure1.ArgumentDataMember = "TimeGap";
                pressure1.ValueDataMembers.AddRange("Pressure1");
                pressure1.DataSource = dt;
                pressure1.Name = LangResx.Main.Series1;
                diagram.SecondaryAxesY["Pressure1"].Title.Text = LangResx.Main.AxisY1;
            }
            else
            {
                xrChart1.Series[1].Visible = false;
                diagram.SecondaryAxesY["Pressure1"].Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            // 유량2
            if (flow2chk)
            {
                Series flow2 = xrChart1.Series[2];
                flow2.ArgumentDataMember = "TimeGap";
                flow2.ValueDataMembers.AddRange("Flow2");
                flow2.DataSource = dt;
                flow2.Name = LangResx.Main.Series2;
                diagram.SecondaryAxesY["Flow2"].Title.Text = LangResx.Main.AxisY2;
            }
            else
            {
                xrChart1.Series[2].Visible = false;
                diagram.SecondaryAxesY["Flow2"].Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            // 압력2
            if (pressure2chk)
            {
                Series pressure2 = xrChart1.Series[3];
                pressure2.ArgumentDataMember = "TimeGap";
                pressure2.ValueDataMembers.AddRange("Pressure2");
                pressure2.DataSource = dt;
                pressure2.Name = LangResx.Main.Series3;
                diagram.SecondaryAxesY["Pressure2"].Title.Text = LangResx.Main.AxisY3;
            }
            else
            {
                xrChart1.Series[3].Visible = false;
                diagram.SecondaryAxesY["Pressure2"].Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            diagram.AxisY.WholeRange.SetMinMaxValues(0, (Program.Option.Flow1 == 0) ? flowAvg1 * 3.5 : Program.Option.Flow1);
            diagram.SecondaryAxesY["Pressure1"].WholeRange.SetMinMaxValues(0, (Program.Option.Pressure1 == 0) ? pressureAvg1 * 3.0 : Program.Option.Pressure1);
            diagram.SecondaryAxesY["Flow2"].WholeRange.SetMinMaxValues(0, (Program.Option.Flow2 == 0) ? flowAvg2 * 2.5 : Program.Option.Flow2);
            diagram.SecondaryAxesY["Pressure2"].WholeRange.SetMinMaxValues(0, (Program.Option.Pressure2 == 0) ? pressureAvg2 * 2.0 : Program.Option.Pressure2);
        }

        #region 텍스트 사이즈 조절
        /// <summary>
        /// 레포트 모든 텍스트 항목 사이즈 조절
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInfo_BeforePrint(object sender, CancelEventArgs e)
        {
            AutoscaleControlText(sender as XRLabel);
        }

        /// <summary>
        /// 자동 폰트사이즈 조절 (셀에 맞춤)
        /// </summary>
        /// <param name="control"></param>
        public void AutoscaleControlText(XRControl control)
        {
            control.Font = new DXFont(control.Font.Name, control.Font.Size, control.Font.Style);
            while (MeasureTextWidthPixels(control).Width / 2 > control.WidthF)
            {
                control.Font = new DXFont(control.Font.Name, control.Font.Size - 0.01f, control.Font.Style);
            }
        }

        /// <summary>
        /// 컨트롤의 텍스트 사이즈 계산
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private SizeF MeasureTextWidthPixels(XRControl control)
        {
            string text = control.Text;

            ((XtraReport)control.Report.Report).PrintingSystem.Graph.Font = control.Font;

            float cellWidth = control.WidthF;

            switch (((XtraReport)control.Report.Report).ReportUnit)
            {
                case DevExpress.XtraReports.UI.ReportUnit.HundredthsOfAnInch:
                    cellWidth = GraphicsUnitConverter.Convert(cellWidth, GraphicsUnit.Inch, GraphicsUnit.Document) / 100;
                    break;
                case DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter:
                    cellWidth = GraphicsUnitConverter.Convert(cellWidth, GraphicsUnit.Millimeter, GraphicsUnit.Document) / 10;
                    break;
            }

            SizeF size = ((XtraReport)control.Report.Report).PrintingSystem.Graph.MeasureString(text, (int)cellWidth);
            float textHeight = 0;

            switch (((XtraReport)control.Report.Report).ReportUnit)
            {
                case DevExpress.XtraReports.UI.ReportUnit.HundredthsOfAnInch:
                    textHeight = GraphicsUnitConverter.Convert(size.Height, GraphicsUnit.Document, GraphicsUnit.Inch) * 100;
                    break;
                case DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter:
                    textHeight = GraphicsUnitConverter.Convert(size.Height, GraphicsUnit.Document, GraphicsUnit.Millimeter) * 10;
                    break;
            }
            return new SizeF(size.Width, textHeight);
        }
        #endregion
    }
}
