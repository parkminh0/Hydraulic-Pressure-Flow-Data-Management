using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using WooSungEngineering.MDILogic;
using System.Linq;
using DevExpress.XtraReports.UI;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using DevExpress.CodeParser;
using DevExpress.Diagram.Core.Shapes;
using static DevExpress.Utils.Frames.FrameHelper;

namespace WooSungEngineering
{
    public partial class MainForm : RibbonForm
    {
        private XYDiagram diagram;
        private ConstRecordIO constRecordIO;
        private DataTable rawData;
        private DataTable dtChart;
        private int EditMode = 1;  // 0: 엑셀 신규, 1: 엑셀 교체
        private bool isExcelSaved = true;
        private bool isTextSaved = true;
        private double Total1 = 0;
        private double Total2 = 0;
        private double FlowAvg1 = 0;
        private double FlowAvg2 = 0;
        private double PressureAvg1 = 0;
        private double PressureAvg2 = 0;
        private int radioIndex;

        #region 폼 생성 & 로드
        /// <summary>
        /// 기본 생성자
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            rdoTime.SelectedIndex = -1;
            diagram = chartControl1.Diagram as XYDiagram;
        }

        /// <summary>
        /// 폼 로드 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UserSkin != "")
                defaultLookAndFeel1.LookAndFeel.SkinName = Properties.Settings.Default.UserSkin;

            // Layout Caption 지정
            layoutControlItem12.Text = LangResx.Main.FlowAvg1Caption;
            layoutControlItem14.Text = LangResx.Main.PressAvg1Caption;
            layoutControlItem9.Text = LangResx.Main.FlowAvg2Caption;
            layoutControlItem13.Text = LangResx.Main.PressAvg2Caption;

            GetData(0);
        }

        /// <summary>
        /// 폼 쇼운
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            SplashScreenManager.CloseForm(false);
        }

        /// <summary>
        /// 시작준비
        ///  1. DB Connection
        ///  2. Ready Screen
        /// </summary>
        public string Splash()
        {
            Text = LangResx.Main.title;
            string InitLoadComplete = string.Empty;
            SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetProgress, 10);

            if (Program.constance.DBConnectInSplash) // 스플래쉬에서 DB 커넥션
            {
                string networkMsg = string.Empty;
                if (string.IsNullOrEmpty(networkMsg))
                {
                    SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetStatus, "Database Connection...");
                    DataTable dt = DBManager.Instance.GetDataTable(Program.constance.DBTestQuery);  //DB Connection
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetProgress, 30);
                        SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetStatus, "Ready to database and user screen...");
                        SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetProgress, 40);
                    }
                    else
                    {
                        InitLoadComplete = LangResx.Main.Splash_DB_Err + "\r\n" + DBManager.DBMErrString;
                        return InitLoadComplete;
                    }
                }
                else
                {
                    InitLoadComplete = networkMsg;
                    return InitLoadComplete;
                }

                SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetStatus, "Loading Common data...");
                SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetProgress, 100);
            }

            return InitLoadComplete;
        }
        #endregion

        #region GetData, SetControl/SetCalc/Chart
        /// <summary>
        /// 현재 메인 폼의 데이터 세팅
        /// </summary>
        /// <param name="recordKey">초기화면 0(마지막 등록된 데이터)</param>
        private void GetData(int recordKey = 0)
        {
            if (recordKey == 0) // 초기화면
            {
                DataTable dt = DBManager.Instance.GetDataTable("SELECT IFNULL(MAX(RecordKey), 0) AS RecordKey FROM ConstRecord WHERE isSaved = 1 AND isDeleted = 0 ");
                recordKey = int.Parse(dt.Rows[0]["RecordKey"].ToString());
            }

            constRecordIO = new ConstRecordIO(recordKey);
            SetControl(); // 컨트롤 텍스트 채우기
            rawData = constRecordIO.GetProcessedData();
            if (rawData.Rows.Count == 0)
            {
                EditMode = 0;
                return;
            }

            SetCalc(); // 차트 생성
        }

        /// <summary>
        /// 데이터 text 세팅
        /// </summary>
        private void SetControl()
        {
            if (EditMode == 0 || constRecordIO.RecordKey == 0)
                항목.Text = "▶ " + LangResx.Main.regist_text;
            else
            {
                항목.Text = " ";
                txtProject.Text = constRecordIO.Project;
                txtRecipe.Text = constRecordIO.Recipe;
                txtHoleNO.Text = constRecordIO.HoleNO;
                if (constRecordIO.Date == DateTime.MinValue || constRecordIO.Date == null)
                    dteDate.EditValue = null;
                else
                    dteDate.DateTime = constRecordIO.Date;
                txtClient.Text = constRecordIO.Client;
                txtContractor.Text = constRecordIO.Contractor;
                txtSubContractor.Text = constRecordIO.SubContractor;
                txtOperator.Text = constRecordIO.Operator;
            }
        }

        /// <summary>
        /// 토탈, 평균 계산
        /// </summary>
        /// <param name="rawData"></param>
        private void SetNum()
        {
            string sql = string.Empty;
            sql += "SELECT (STRFTIME('%s', p2.[Time]) - STRFTIME('%s', p1.[Time])) / 60.0 AS totalTime ";
            sql += "  FROM ProcessedData p1 ";
            sql += "  LEFT JOIN ";
            sql += "            (SELECT  RecordKey, ";
            sql += "                     [Time] ";
            sql += "               FROM  ProcessedData ";
            sql += "              ORDER BY [Time] DESC) AS p2 ";
            sql += "         ON p1.RecordKey = p2.recordKey ";
            sql += $"WHERE p1.RecordKey = {constRecordIO.RecordKey} ";
            sql += " LIMIT 1";
            DataTable dt = DBManager.Instance.GetDataTable(sql);
            double totalTime = double.Parse(dt.Rows[0]["totalTime"].ToString()) + 1; // 데이터의 총 시간(분)

            // 차트, 레포트 변수
            double.TryParse(rawData.Compute("SUM(Flow1)", "").ToString(), out Total1);
            double.TryParse(rawData.Compute("SUM(Flow2)", "").ToString(), out Total2);
            double.TryParse(rawData.Compute("Avg(Flow1)", "").ToString(), out FlowAvg1);
            double.TryParse(rawData.Compute("Avg(Flow2)", "").ToString(), out FlowAvg2);
            double.TryParse(rawData.Compute("Avg(Pressure1)", "").ToString(), out PressureAvg1);
            double.TryParse(rawData.Compute("Avg(Pressure2)", "").ToString(), out PressureAvg2);

            // 유량평균 텍스트용
            double pFlowAvg1 = 0;
            double pFlowAvg2 = 0;
            double.TryParse((Total1 / totalTime).ToString(), out pFlowAvg1);
            double.TryParse((Total2 / totalTime).ToString().ToString(), out pFlowAvg2);

            if (checkEdit1.Checked)
                spnTotal1.EditValue = Total1 * 2;
            else
                spnTotal1.EditValue = Total1;

            if(checkEdit2.Checked)
                spnTotal2.EditValue = Total2 * 2;
            else
                spnTotal2.EditValue = Total2;

            spnFlowAvg1.EditValue = pFlowAvg1;
            spnFlowAvg2.EditValue = pFlowAvg2;
            spnPressureAvg1.EditValue = PressureAvg1;
            spnPressureAvg2.EditValue = PressureAvg2;
        }

        /// <summary>
        /// Total, Average, 시간차이 계산
        /// </summary>
        private void SetCalc()
        {
            SetNum();

            try
            {
                double startTime = double.Parse(rawData.Rows[0]["TimeGap"].ToString());
                double endTime = double.Parse(rawData.Rows[1]["TimeGap"].ToString());
                double timeDiff = endTime - startTime;
                if (timeDiff == 0.5)
                {
                    rdoTime.SelectedIndex = 0;
                }
                else
                {
                    switch (timeDiff)
                    {
                        case 1:
                            rdoTime.SelectedIndex = 1;
                            break;
                        case 2:
                            rdoTime.SelectedIndex = 2;
                            break;
                        case 5:
                            rdoTime.SelectedIndex = 3;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
            
            SetChart();
        }

        /// <summary>
        /// Chart Data 시간 설정에 맞게 필터링
        /// </summary>
        /// <param name="chartData"></param>
        /// <returns></returns>
        private DataTable FilterChartData()
        {
            DataTable modChart = rawData.Copy();
            DataView dv = modChart.DefaultView;

            switch (radioIndex)
            {
                case 0: // 0.5분
                    dv.RowFilter = "";
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 0.5;
                    break;
                case 1:
                    dv.RowFilter = "CONVERT(TimeGap, 'System.Int32') % 1 + TimeGap - CONVERT(TimeGap, 'System.Int32') = 0";
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 1;
                    break;
                case 2:
                    dv.RowFilter = "CONVERT(TimeGap, 'System.Int32') % 2 + TimeGap - CONVERT(TimeGap, 'System.Int32') = 0";
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 2;
                    break;
                case 3: // 5분
                    dv.RowFilter = "CONVERT(TimeGap, 'System.Int32') % 5 + TimeGap - CONVERT(TimeGap, 'System.Int32') = 0";
                    diagram.AxisX.NumericScaleOptions.GridSpacing = 5;
                    break;
            }

            return modChart;
        }

        /// <summary>
        /// 차트 설정
        /// </summary>
        private void SetChart()
        {
            dtChart = FilterChartData();

            Series flow1 = chartControl1.Series[0];
            flow1.ArgumentDataMember = "TimeGap";
            flow1.ValueDataMembers.AddRange("Flow1");
            flow1.DataSource = dtChart;

            Series pressure1 = chartControl1.Series[1];
            pressure1.ArgumentDataMember = "TimeGap";
            pressure1.ValueDataMembers.AddRange("Pressure1");
            pressure1.DataSource = dtChart;

            Series flow2 = chartControl1.Series[2];
            flow2.ArgumentDataMember = "TimeGap";
            flow2.ValueDataMembers.AddRange("Flow2");
            flow2.DataSource = dtChart;

            Series pressure2 = chartControl1.Series[3];
            pressure2.ArgumentDataMember = "TimeGap";
            pressure2.ValueDataMembers.AddRange("Pressure2");
            pressure2.DataSource = dtChart;

            // 유량1, 압력1 범위 설정
            diagram.AxisY.WholeRange.SetMinMaxValues(0, (Program.Option.Flow1 == 0) ? FlowAvg1 * 3.5 : Program.Option.Flow1);
            diagram.SecondaryAxesY["Pressure1"].WholeRange.SetMinMaxValues(0, (Program.Option.Pressure1 == 0) ? PressureAvg1 * 3.0 : Program.Option.Pressure1);
            // 유량2, 압력2 범위 설정
            diagram.SecondaryAxesY["Flow2"].WholeRange.SetMinMaxValues(0, (Program.Option.Flow2 == 0) ? FlowAvg2 * 2.5 : Program.Option.Flow2);
            diagram.SecondaryAxesY["Pressure2"].WholeRange.SetMinMaxValues(0, (Program.Option.Pressure2 == 0) ? PressureAvg2 * 2.0 : Program.Option.Pressure2);
            
            if (Total2 == 0)
            {
                // 데이터 2가 없을 때 해당 시리즈, AxisY(Y축) 숨김
                chartControl1.Series[2].Visible = false;
                chartControl1.Series[3].Visible = false;
                diagram.SecondaryAxesY["Flow2"].Visibility = DevExpress.Utils.DefaultBoolean.False;
                diagram.SecondaryAxesY["Pressure2"].Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                // 데이터 2가 존재할 때 해당 시리즈, AxisY(Y축) 표시
                chartControl1.Series[2].Visible = true;
                chartControl1.Series[3].Visible = true;
                diagram.SecondaryAxesY["Flow2"].Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.SecondaryAxesY["Pressure2"].Visibility = DevExpress.Utils.DefaultBoolean.True;
            }
        }
        #endregion

        #region 엑셀 로드
        /// <summary>
        /// 신규용 엑셀 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnNewLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!isExcelSaved || !isTextSaved)
            {
                if (XtraMessageBox.Show(LangResx.Main.NewLoadMsg1 + "\r\n" + LangResx.Main.NewLoadMsg2, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            EditMode = 0;
            int recordKey = GetExcel();
            if (recordKey == 0)
                return;

            GetData(recordKey);
        }

        /// <summary>
        /// 수정용 엑셀 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnEditLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Total1 == 0 && rawData.Rows.Count == 0)
            {
                XtraMessageBox.Show(LangResx.Main.EditLoadMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            EditMode = 1;
            int recordKey = GetExcel();
            if (recordKey == 0)
                return;

            rawData = constRecordIO.GetRawData();
            SetCalc();
        }

        /// <summary>
        /// 엑셀 로드
        /// </summary>
        private int GetExcel()
        {
            string filePath;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = LangResx.Main.Load_type + "(*.xlsx;*.xls)|*.xlsx;*.xls";
                int tempKey = 0;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    SplashScreenManager.Default.SetWaitFormDescription(LangResx.Main.LoadingDataMsg);
                    // Raw Data 가져오기
                    ExcelLoading el = new ExcelLoading(filePath, EditMode, constRecordIO.RecordKey);
                    tempKey = el.key;
                    if (!string.IsNullOrWhiteSpace(el.resultMessage))
                    {
                        XtraMessageBox.Show(el.resultMessage, LangResx.Main.LoadExcel_Err, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tempKey = 0;
                    }

                    SplashScreenManager.CloseForm(false);
                    isExcelSaved = false;
                    return tempKey;
                }
                else
                {
                    return tempKey;
                }
            }
        }
        #endregion

        #region 기록 목록 팝업
        /// <summary>
        /// 저장된 데이터 목록 팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnConstRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormPopConstRecord frm = new FormPopConstRecord(isExcelSaved && isTextSaved, constRecordIO.RecordKey);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                EditMode = 1;
                GetData(frm.ChoicetempKey);
                isExcelSaved = true;
                isTextSaved = true;
            }
        }
        #endregion

        #region 유압 수정 팝업
        /// <summary>
        /// 데이터 수정 팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (rawData.Rows.Count == 0)
                return;
            
            FormPopEdit frm = new FormPopEdit(rawData.Copy(), constRecordIO.RecordKey);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                rawData = frm.dt;
                SetCalc();
                isExcelSaved = false;
            }
        }
        #endregion

        #region 삭제
        /// <summary>
        /// 현재 기록 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (constRecordIO.RecordKey == 0 || constRecordIO.isSaved == false)
            {
                XtraMessageBox.Show(LangResx.Main.DeleteMsg_NoData, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show(LangResx.Main.DeleteMsg, LangResx.Main.DeleteMsgCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int result = ConstRecordIO.DeleteData(constRecordIO.RecordKey);
                if (result == 1)
                {
                    XtraMessageBox.Show(LangResx.Main.DeleteMsg_Success + "\r\n" + LangResx.Main.DeleteMsg_Load, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isTextSaved = true;
                    isExcelSaved = true;
                    EditMode = 1;
                    GetData(0);
                }
                else
                {
                    XtraMessageBox.Show(LangResx.Main.DeleteMsg_Err, LangResx.Main.ErrMsg_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region 저장
        /// <summary>
        /// 저장할 항목내용 클래스에 세팅
        /// </summary>
        private void SetInfo()
        {
            constRecordIO.Project = txtProject.Text.Trim();
            constRecordIO.Client = txtClient.Text.Trim();
            constRecordIO.SubContractor = txtSubContractor.Text.Trim();
            constRecordIO.Date = DateTime.Parse(dteDate.DateTime.ToString("yyyy-MM-dd"));
            constRecordIO.Recipe = txtRecipe.Text.Trim();
            constRecordIO.HoleNO = txtHoleNO.Text.Trim();
            constRecordIO.Contractor = txtContractor.Text.Trim();
            constRecordIO.Operator = txtOperator.Text.Trim();
        }

        /// <summary>
        /// 현재 기록 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (constRecordIO.RecordKey == 0)
                return;

            if (XtraMessageBox.Show(LangResx.Main.SaveMsg, LangResx.Main.SaveMsg_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription(LangResx.Main.SavingMsg);

                int result1 = -1;
                bool result2 = true;

                SetInfo();
                result1 = constRecordIO.UpdateConst();
                result2 = DBManager.Instance.DoBulkCopyTI(rawData, "ProcessedData", constRecordIO.RecordKey);

                SplashScreenManager.CloseForm(false);

                if (result1 == 1 && result2 == true)
                {
                    XtraMessageBox.Show(LangResx.Main.SaveMsg_Success, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EditMode = 1;
                    GetData(constRecordIO.RecordKey);
                    isExcelSaved = true;
                    isTextSaved = true;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region PDF & Report
        /// <summary>
        /// PDF 내보내기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportPDF_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraReport rpt = CreateReportChart(0);
            if (rpt == null)
                return;

            try
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "PDF File (*.pdf)|*.pdf";
                    dialog.FileName = LangResx.Main.PDF_Caption + string.Format("_{0}", DateTime.Now.ToString("yyyyMMdd"));
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                        SplashScreenManager.Default.SetWaitFormDescription(LangResx.Main.Msg_SavingPdf);
                        // 파일 저장작업
                        rpt.ExportToPdf(dialog.FileName);
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show(LangResx.Main.Msg_Saved, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(LangResx.Main.Err_Saving + "\r\n" + ex.Message, LangResx.Main.ErrMsg_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraReport rpt = CreateReportChart(1);
            if (rpt == null)
                return;

            rpt.ShowPreview();
        }

        /// <summary>
        /// XtraReport 생성
        /// </summary>
        private XtraReport CreateReportChart(int reportMode)
        {
            FormPopSetTitle frm = new FormPopSetTitle(reportMode);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bool flow1chk = false;
                bool pressure1chk = false;
                bool flow2chk = false;
                bool pressure2chk = false;
                bool.TryParse(chartControl1.Series[0].CheckedInLegend.ToString(), out flow1chk);
                bool.TryParse(chartControl1.Series[1].CheckedInLegend.ToString(), out pressure1chk);
                if (Total2 != 0)
                {
                    bool.TryParse(chartControl1.Series[2].CheckedInLegend.ToString(), out flow2chk);
                    bool.TryParse(chartControl1.Series[3].CheckedInLegend.ToString(), out pressure2chk);
                }

                ReportChart rptChart = new ReportChart(dtChart, radioIndex, FlowAvg1, PressureAvg1, FlowAvg2, PressureAvg2, flow1chk, pressure1chk, flow2chk, pressure2chk);
                // 제목 + 8개 항목
                rptChart.Parameters["Title"].Value = Program.Option.CompName;
                rptChart.Parameters["Project"].Value = txtProject.Text.Trim();
                rptChart.Parameters["Recipe"].Value = txtRecipe.Text.Trim();
                rptChart.Parameters["HoleNO"].Value = txtHoleNO.Text.Trim();
                rptChart.Parameters["Date"].Value = dteDate.Text.Trim();
                rptChart.Parameters["Client"].Value = txtClient.Text.Trim();
                rptChart.Parameters["Contractor"].Value = txtContractor.Text.Trim();
                rptChart.Parameters["SubContractor"].Value = txtSubContractor.Text.Trim();
                rptChart.Parameters["Operator"].Value = txtOperator.Text.Trim();
                rptChart.Parameters["Project"].Value = txtProject.Text.Trim();

                // Total, Avg
                rptChart.Parameters["Total1"].Value = (spnTotal1.Text.Trim() == "0" || !flow1chk) ? " " : spnTotal1.Text.Trim();
                rptChart.Parameters["FlowAvg1"].Value = (spnFlowAvg1.Text.Trim() == "0" || !flow1chk) ? " " : spnFlowAvg1.Text.Trim();
                rptChart.Parameters["PressureAvg1"].Value = (spnPressureAvg1.Text.Trim() == "0" || !pressure1chk) ? " " : spnPressureAvg1.Text.Trim();
                rptChart.Parameters["Total2"].Value = (spnTotal2.Text.Trim() == "0" || !flow2chk) ? " " : spnTotal2.Text.Trim();
                rptChart.Parameters["FlowAvg2"].Value = (spnFlowAvg2.Text.Trim() == "0" || !flow2chk) ? " " : spnFlowAvg2.Text.Trim();
                rptChart.Parameters["PressureAvg2"].Value = (spnPressureAvg2.Text.Trim() == "0" || !pressure2chk) ? " " : spnPressureAvg2.Text.Trim();

                LogicManager.Report.makeReportCommon(LangResx.Main.PDF_Caption, 0, 0, 0, 0, true);
                LogicManager.Report.CreateCompositeReport(rptChart, true);
                //LogicManager.Report.MainReport.ShowPreview();
                LogicManager.Report.MainReport.BackColor = System.Drawing.Color.Red;
                return LogicManager.Report.MainReport;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 언어 / 종료
        /// <summary>
        /// 프로그램 종료시
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (XtraMessageBox.Show(LangResx.Main.QuitMsg, LangResx.Main.QuitMsg_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                Program.SaveConfig();
            }
        }

        /// <summary>
        /// 프로그램 종료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
        #endregion

        #region 이벤트 method
        /// <summary>
        /// 0.5, 1, 2, 5 분 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioIndex = rdoTime.SelectedIndex;
            SetChart();
        }

        /// <summary>
        /// 텍스트 변경사항 확인
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordData_EditValueChanged(object sender, EventArgs e)
        {
            isTextSaved = false;
        }

        /// <summary>
        /// 토글에 따라 평균값 정수/실수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn)
            {
                lbIsInt.Text = LangResx.Main.toggleInt;
                spnTotal1.Properties.EditMask = "n0";
                spnTotal2.Properties.EditMask = "n0";
                spnFlowAvg1.Properties.EditMask = "n0";
                spnFlowAvg2.Properties.EditMask = "n0";
                spnPressureAvg1.Properties.EditMask = "n0";
                spnPressureAvg2.Properties.EditMask = "n0";
            }
            else
            {
                lbIsInt.Text = LangResx.Main.toggleFloat;
                if (Total1 != 0)
                {
                    spnTotal1.Properties.EditMask = "n2";
                    spnFlowAvg1.Properties.EditMask = "n2";
                    spnPressureAvg1.Properties.EditMask = "n2";
                }
                if (Total2 != 0)
                {
                    spnTotal2.Properties.EditMask = "n2";
                    spnFlowAvg2.Properties.EditMask = "n2";
                    spnPressureAvg2.Properties.EditMask = "n2";
                }
            }
        }

        /// <summary>
        /// 차트 X축 공백값 출력 => 공백
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartControl1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            if (diagram != null && e.Item.Axis == diagram.AxisX)
            {
                if (dtChart != null && dtChart.Rows.Count > 0)
                {
                    DataRow dr = dtChart.Rows[dtChart.Rows.Count - 1];
                    double maxTimeGap = double.Parse(dr["TimeGap"].ToString());

                    double axisValue = 0;
                    double.TryParse(e.Item.AxisValue.ToString(), out axisValue);
                    if (axisValue > maxTimeGap)
                    {
                        e.Item.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// 토탈 2배
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
                spnTotal1.EditValue = Total1 * 2;
            else
                spnTotal1.EditValue = Total1;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked)
                spnTotal2.EditValue = Total2 * 2;
            else
                spnTotal2.EditValue = Total2;
        }
        #endregion

        #region 언어설정
        private void bbtnKorean_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Program.Option.cultureName == "" || Program.Option.cultureName == "ko")
                return;

            if (XtraMessageBox.Show("When changing the language, the program needs to be restarted.\r\nDo you want to restart?", "Change Language", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.ChangeLanguage("");
                Application.Restart();
            }
        }

        private void bbtnEnglish_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Program.Option.cultureName == "en")
                return;

            if (XtraMessageBox.Show("언어 변경시 프로그램 재시작이 필요합니다.\r\n재시작하시겠습니까?", "언어변경", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.ChangeLanguage("en");
                Application.Restart();
            }
        }
        #endregion

        private void bbtnSetY_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormPopSetY frm = new FormPopSetY(Program.Option.Flow1, Program.Option.Pressure1, Program.Option.Flow2, Program.Option.Pressure2);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                SetChart();
            }
        }
    }
}
