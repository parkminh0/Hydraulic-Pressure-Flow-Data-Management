using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraEditors.TextEditController.IME;
using DevExpress.XtraRichEdit.Import.OpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WooSungEngineering.MDILogic
{
    internal class ConstRecordIO
    {
        // class ConstRecord

        private int _RecordKey;

        public int RecordKey
        {
            get { return _RecordKey; }
            set { _RecordKey = value; }
        }
        private string _Project;

        public string Project
        {
            get { return _Project; }
            set { _Project = value; }
        }
        private string _Client;

        public string Client
        {
            get { return _Client; }
            set { _Client = value; }
        }
        private string _SubContractor;

        public string SubContractor
        {
            get { return _SubContractor; }
            set { _SubContractor = value; }
        }
        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private string _Recipe;

        public string Recipe
        {
            get { return _Recipe; }
            set { _Recipe = value; }
        }
        private string _HoleNO;

        public string HoleNO
        {
            get { return _HoleNO; }
            set { _HoleNO = value; }
        }
        private string _Contractor;

        public string Contractor
        {
            get { return _Contractor; }
            set { _Contractor = value; }
        }
        private string _Operator;

        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }
        private bool _isDeleted;

        public bool isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        private bool _isSaved;

        public bool isSaved
        {
            get { return _isSaved; }
            set { _isSaved = value; }
        }


        /// <summary>
        /// 데이터 생성자
        /// </summary>
        /// <param name="pRecordKey"></param>
        public ConstRecordIO(int pRecordKey)
        {
            GetData(pRecordKey);
        }

        /// <summary>
        /// 데이터 가져오기
        /// </summary>
        /// <param name="pRecordKey"></param>
        private void GetData(int pRecordKey)
        {
            string sql = string.Empty;
            sql += "SELECT RecordKey, ";
            sql += "       Project, ";
            sql += "       Client, ";
            sql += "       SubContractor, ";
            sql += "       Date, ";
            sql += "       Recipe, ";
            sql += "       HoleNO, ";
            sql += "       Contractor, ";
            sql += "       Operator, ";
            sql += "       isSaved ";
            sql += "  FROM ConstRecord ";
            sql += " WHERE 1 = 1 ";
            sql += $"   AND RecordKey = {pRecordKey} ";
            DataTable dt = DBManager.Instance.GetDataTable(sql);
            if (dt == null || dt.Rows.Count == 0)
                return;

            int.TryParse(dt.Rows[0]["RecordKey"].ToString(), out _RecordKey);
            Project = dt.Rows[0]["Project"].ToString();
            Client = dt.Rows[0]["Client"].ToString();
            SubContractor = dt.Rows[0]["SubContractor"].ToString();
            DateTime.TryParse(dt.Rows[0]["Date"].ToString(), out _Date);
            Recipe = dt.Rows[0]["Recipe"].ToString();
            HoleNO = dt.Rows[0]["HoleNO"].ToString();
            Contractor = dt.Rows[0]["Contractor"].ToString();
            Operator = dt.Rows[0]["Operator"].ToString();
            bool.TryParse(dt.Rows[0]["isSaved"].ToString(), out _isSaved);
        }

        /// <summary>
        /// ProcessedData 가져오기
        /// </summary>
        /// <returns></returns>
        public DataTable GetProcessedData()
        {
            string sql = string.Empty;
            sql += "SELECT [RecordKey], ";
            sql += "       [ExcelRowNum], ";
            sql += "       [Date], ";
            sql += "       strftime('%H:%M:%S', [Time]) AS [Time], ";
            sql += "       [Flow1], ";
            sql += "       [Pressure1], ";
            sql += "       [Flow2], ";
            sql += "       [Pressure2], ";
            sql += "       [TimeGap] ";
            sql += "  FROM [ProcessedData] ";
            sql += $"WHERE [RecordKey] = {RecordKey} ";
            sql += " ORDER BY [Time] ";
            return DBManager.Instance.GetDataTable(sql);
        }

        /// <summary>
        /// 임시 RawData 가져오기
        /// </summary>
        /// <returns></returns>
        public DataTable GetRawData()
        {
            string sql = string.Empty;
            sql += $"SELECT {RecordKey} AS RecordKey, ";
            sql += "        ROW_NUMBER() OVER (ORDER BY [Time]) AS ExcelRowNum, ";
            sql += "        [Date], ";
            sql += "        strftime('%H:%M:%S', [Time]) AS [Time], ";
            sql += "        [Flow1], ";
            sql += "        [Pressure1], ";
            sql += "        [Flow2], ";
            sql += "        [Pressure2], ";
            sql += "        (STRFTIME('%s', STRFTIME('%H:%M:%S', [Time])) - (SELECT MIN(STRFTIME('%s', STRFTIME('%H:%M:%S', [Time]))) FROM RawData GROUP BY[Date])) / 60.0 AS [TimeGap] ";
            sql += "   FROM RawData ";
            sql += "  ORDER BY ExcelRowNum ";
            return DBManager.Instance.GetDataTable(sql);
        }

        /// <summary>
        /// 데이터 조회
        /// </summary>
        public static DataTable Select(string pProject, string pClient, string pSubContractor, DateTime dte1, DateTime dte2, bool allDte)
        {
            string sql = string.Empty;
            sql += "SELECT RecordKey, ";
            sql += "       Project, ";
            sql += "       Client, ";
            sql += "       SubContractor, ";
            sql += "       [Date], ";
            sql += "       Recipe, ";
            sql += "       HoleNO, ";
            sql += "       Contractor, ";
            sql += "       Operator ";
            sql += "  FROM ConstRecord ";
            sql += " WHERE 1 = 1 ";
            sql += "   AND isSaved = 1 ";
            sql += "   AND isDeleted = 0 ";
            if (pProject != "전체" && pProject != "All" && !string.IsNullOrEmpty(pProject))
            {
                sql += $" AND Project LIKE '%{pProject}%' ";
            }
            if (pClient != "전체" && pClient != "All" && !string.IsNullOrEmpty(pClient))
            {
                sql += $" AND Client LIKE '%{pClient}%' ";
            }
            if (pSubContractor != "전체" && pSubContractor != "All" && !string.IsNullOrEmpty(pSubContractor))
            {
                sql += $" AND SubContractor LIKE '%{pSubContractor}%' ";
            }
            if (!allDte)
            {
                sql += $" AND ([DATE] BETWEEN '{dte1.ToString("yyyy-MM-dd")}' AND '{dte2.ToString("yyyy-MM-dd")}') ";
            }
            sql += " ORDER BY CreateDtm Desc ";
            return DBManager.Instance.GetDataTable(sql);
        }

        /// <summary>
        /// ConstRecord Update
        /// </summary>
        public int UpdateConst()
        {
            string sql = string.Empty; // 기록 INSERT
            sql += "UPDATE ConstRecord ";
            sql += "SET ";
            sql += $"       RecordKey = {RecordKey}, ";
            sql += $"       Project = '{Project}', ";
            sql += $"       Client = '{Client}', ";
            sql += $"       SubContractor = '{SubContractor}', ";
            if (Date == DateTime.MinValue)
                sql += $"    Date = NULL, ";
            else
                sql += $"   Date = '{Date.ToString("yyyy-MM-dd")}', ";
            sql += $"       Recipe = '{Recipe}', ";
            sql += $"       HoleNO = '{HoleNO}', ";
            sql += $"       Contractor = '{Contractor}', ";
            sql += $"       Operator = '{Operator}', ";
            sql += $"       LastUpdateDtm = CURRENT_TIMESTAMP, ";
            sql += "        isSaved = 1 ";
            sql += $" WHERE RecordKey = {RecordKey} ";
            return DBManager.Instance.ExcuteDataUpdate(sql);
        }

        /// <summary>
        /// 기록 삭제
        /// </summary>
        /// <returns></returns>
        public static int DeleteData(int key)
        {
            string sql = string.Empty;
            sql += "UPDATE ConstRecord ";
            sql += "   SET isDeleted = 1 ";
            sql += $"WHERE RecordKey = {key} ";
            return DBManager.Instance.ExcuteDataUpdate(sql);
        }
    }
}
