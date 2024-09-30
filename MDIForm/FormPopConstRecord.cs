using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WooSungEngineering.MDILogic;

namespace WooSungEngineering
{
    public partial class FormPopConstRecord : DevExpress.XtraEditors.XtraForm
    {
        public int ChoicetempKey;
        private bool isSaved;
        private DataTable dtConstRecord;
        private int recordKey;

        #region 팝업 shown & GetData
        /// <summary>
        /// 시작 시 GetData
        /// </summary>
        public FormPopConstRecord(bool isSaved, int recordKey)
        {
            InitializeComponent();
            this.isSaved = isSaved;
            this.recordKey = recordKey;
        }

        /// <summary>
        /// 폼 쇼운
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPopConstRecord_Shown(object sender, EventArgs e)
        {
            dte1.DateTime = DateTime.Now.AddMonths(-1);
            dte2.DateTime = DateTime.Now;

            // 초기 화면 최근 1달 데이터
            GetData();
        }

        /// <summary>
        /// 데이터 조회
        /// </summary>
        private void GetData()
        {
            if (dte2.DateTime == DateTime.MinValue)
                dte2.DateTime = DateTime.Now;

            dtConstRecord = ConstRecordIO.Select(cboProject.Text.Trim(), cboClient.Text.Trim(), cboSubContractor.Text.Trim(), dte1.DateTime, dte2.DateTime, chkDate.Checked);
            grdConstRecord.DataSource = dtConstRecord;
            
        }
        #endregion

        #region btn조회/열기/삭제/닫기
        /// <summary>
        /// 조건 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }

        #region 선택한 데이터 저장 및 더블클릭 시 열기
        /// <summary>
        /// 선택한 데이터 저장 
        /// </summary>
        private void GetFocused()
        {
            DataRow row = grdViewConstRecord.GetFocusedDataRow();
            if (row == null)
                return;

            try
            {
                ChoicetempKey = int.Parse(row["RecordKey"].ToString());
            }
            catch (Exception)
            {
                ChoicetempKey = 0;
                return;
            }
        }

        /// <summary>
        /// btn열기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>y
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (ChoicetempKey == 0)
                return;

            showData();
        }
        private void grdViewConstRecord_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            GetFocused();
            if (e.Clicks == 2 && ChoicetempKey != 0)
                showData();
        }
        private void showData()
        {
            if (!isSaved)
            {
                if (XtraMessageBox.Show("데이터가 저장되지 않았습니다.\r\n해당 데이터를 조회하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        /// <summary>
        /// 삭제 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(LangResx.Main.Delete2Msg, LangResx.Main.DeleteMsgCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                DeleteSelectedDatas();
            else
                return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void DeleteSelectedDatas()
        {
            //grdViewConstRecord.DeleteSelectedRows();
            //DataTable dtDeleted = dtConstRecord.GetChanges(DataRowState.Deleted);
            //dtConstRecord.AcceptChanges();
            //dtConstRecord.RejectChanges();

            int[] handles = grdViewConstRecord.GetSelectedRows();

            List<string> sqls = new List<string>();
            foreach (int handle in handles)
            {
                int key = int.Parse(dtConstRecord.Rows[handle]["RecordKey"].ToString());
                if (key == recordKey)
                {
                    XtraMessageBox.Show(LangResx.Main.DeleteErrMsg, LangResx.Main.ErrMsg_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                sqls.Add($"UPDATE ConstRecord SET isDeleted = 1 WHERE RecordKey = '{key}' ");
            }
            string result = DBManager.Instance.ExcuteTransaction(sqls);

            if (string.IsNullOrEmpty(result))
            {
                XtraMessageBox.Show(LangResx.Main.DeleteMsg_Success, LangResx.Main.DeleteMsgCaption, MessageBoxButtons.OK);
                GetData();
            }
            else
            {
                XtraMessageBox.Show(LangResx.Main.DeleteMsg_Err, LangResx.Main.ErrMsg_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 팝업 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 이벤트 method
        /// <summary>
        /// 전체 날짜 조회 여부 확인
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            dte1.Enabled = dte2.Enabled = !chkDate.Checked;
        }

        /// <summary>
        /// 컬럼 수에 따라 조회 결과 - 건수 변화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdViewConstRecord_RowCountChanged(object sender, EventArgs e)
        {
            groupControl1.Text = "≡ " + LangResx.Main.SearchResult1 + string.Format("{0:n0}", grdViewConstRecord.RowCount) + LangResx.Main.SearchResult2;
        }
        
        //그리드뷰 = deletesele
        #endregion
    }
}