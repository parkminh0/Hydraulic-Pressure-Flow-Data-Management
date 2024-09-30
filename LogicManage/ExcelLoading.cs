using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using DevExpress.XtraCharts;

namespace WooSungEngineering
{
    class ExcelLoading
    {
        private string filePath;
        public bool isOK;
        public string resultMessage;
        public DataTable rawTable;
        public int key;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="aPath"></param>
        /// <param name="EditMode">0: 신규, 1: 엑셀수정</param>
        /// <param name="Recordkey"></param>
        public ExcelLoading(string aPath, int EditMode, int Recordkey)
        {
            try
            {
                filePath = aPath;
                isOK = true;

                DataTable excelTable = GetExcelLoading(); // 엑셀 로딩
                if (excelTable == null)
                    return;

                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    if (excelTable.Columns.Count >= 4 && dr[0].ToString() == "Date" && dr[1].ToString() == "Time" && dr[2].ToString() == "L/min" && dr[3].ToString() == "kg/cm2")
                    {
                        excelTable.Columns[0].ColumnName = "Date";
                        excelTable.Columns[1].ColumnName = "Time";
                        excelTable.Columns[2].ColumnName = "Flow1";
                        excelTable.Columns[3].ColumnName = "Pressure1";
                        if (excelTable.Columns.Count == 6 && dr[4].ToString() == "L/min" && dr[5].ToString() == "kg/cm2")
                        {
                            excelTable.Columns[4].ColumnName = "Flow2";
                            excelTable.Columns[5].ColumnName = "Pressure2";
                        }
                        excelTable.Rows.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        excelTable.Rows.RemoveAt(i);
                        i--;
                    }
                }

                // RawData
                rawTable = DBManager.Instance.GetDataTable("SELECT * FROM RawData WHERE 1 = 2 ");
                DataView dv = excelTable.DefaultView;
                dv.RowFilter = "[Time] IS NOT NULL ";
                rawTable.Merge(dv.ToTable(), true, MissingSchemaAction.Ignore);
                DBManager.Instance.DoBulkCopyTI(rawTable, "RawData");

                // 사용할 키
                if (EditMode == 0)
                {
                    DataTable dt = DBManager.Instance.GetDataTable("SELECT IFNULL(MAX(RecordKey), 0) + 1 AS KEY FROM ConstRecord WHERE isSaved = 1 ");
                    key = int.Parse(dt.Rows[0]["KEY"].ToString()); // 키 값

                    List<string> querys = new List<string>();

                    // 저장되지 않은 ConstRecord 삭제 (수정일 경우 삭제 안함)
                    string sql = string.Empty;
                    sql += "DELETE FROM ProcessedData ";
                    sql += $" WHERE RecordKey IN ";
                    sql += "       ( SELECT RecordKey ";
                    sql += "           FROM ConstRecord ";
                    sql += "          WHERE isSaved = 0) ";
                    querys.Add(sql);

                    sql = string.Empty;
                    sql += "DELETE FROM ConstRecord ";
                    sql += " WHERE isSaved = 0 ";
                    querys.Add(sql);

                    // 빈 ConstRecord 삽입
                    sql = string.Empty;
                    sql += "INSERT INTO ConstRecord ";
                    sql += "( ";
                    sql += "       RecordKey, ";
                    sql += "       CreateDtm, ";
                    sql += "       LastUpdateDtm, ";
                    sql += "       isDeleted, ";
                    sql += "       isSaved ";
                    sql += ") ";
                    sql += "VALUES ( ";
                    sql += $"      {key}, ";
                    sql += "       CURRENT_TIMESTAMP, ";
                    sql += "       CURRENT_TIMESTAMP, ";
                    sql += "       0, ";
                    sql += "       0 ";
                    sql += ") ";
                    querys.Add(sql);

                    // ProcessedData 에 RowData 삽입
                    sql = string.Empty;
                    sql += "INSERT INTO ProcessedData ";
                    sql += $"  SELECT {key} AS RecordKey, ";
                    sql += "         ROW_NUMBER() OVER (ORDER BY [Time]) AS ExcelRowNum, ";
                    sql += "         [Date], ";
                    sql += "         STRFTIME('%H:%M:%S', [Time]) AS [Time], ";
                    sql += "         [Flow1], ";
                    sql += "         [Pressure1], ";
                    sql += "         [Flow2], ";
                    sql += "         [Pressure2], ";
                    sql += "         (STRFTIME('%s', STRFTIME('%H:%M:%S', [Time])) - (SELECT MIN(STRFTIME('%s', STRFTIME('%H:%M:%S', [Time]))) FROM RawData GROUP BY [Date])) / 60.0 AS [TimeGap]  ";
                    sql += "    FROM RawData ";
                    querys.Add(sql);
                    DBManager.Instance.ExcuteTransaction(querys);  // 마스터 DB 저장
                }
                else
                {
                    key = Recordkey;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 엑셀로딩(코드플렉스 참조용)
        /// </summary>
        private DataTable GetExcelLoading()
        {
            DataSet ds;

            var file = new FileInfo(filePath);
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    IExcelDataReader reader = null;
                    if (file.Extension == ".xls")
                    {
                        SpreadsheetControl ssc = new SpreadsheetControl();
                        ssc.LoadDocument(file.FullName);
                        MemoryStream ms = new MemoryStream();
                        ssc.SaveDocument(ms, DocumentFormat.Xlsx);
                        reader = ExcelReaderFactory.CreateOpenXmlReader(ms);

                    }
                    else if (file.Extension == ".xlsx")
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    if (reader == null)
                        return null;
                    reader.IsFirstRowAsColumnNames = false;
                    ds = reader.AsDataSet();

                    if (ds.Tables.Count < 1)
                        return null;

                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                isOK = false;
                resultMessage = LangResx.Main.LoadExcel_ExistErr + "\r\n" + ex.Message;
                return null;
            }
        }
    }
}
