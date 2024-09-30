using DevExpress.Export;
using DevExpress.PivotGrid.SliceQueryEngine;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WooSungEngineering
{
    public partial class FormPopEdit : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dtMod;
        private int key;
        public DataTable dt;
        public FormPopEdit(DataTable rawData, int key)
        {
            InitializeComponent();
            dtMod = rawData;
            this.key = key;
        }

        /// <summary>
        /// 팝업 쇼운
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPopEdit_Shown(object sender, EventArgs e)
        {
            grdProcessedData.DataSource = dtMod;
        }

        /// <summary>
        /// 'X' 클릭 시 행 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rpbteDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grdViewProcessedData.DeleteSelectedRows();
        }

        /// <summary>
        /// 수정한 데이터 TI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdViewProcessedData.IsEditing)
            {
                grdViewProcessedData.CloseEditor();
                grdViewProcessedData.UpdateCurrentRow();
            }

            if (XtraMessageBox.Show(LangResx.Main.EditDataMsg, LangResx.Main.EditData_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataView dv = dtMod.DefaultView;
                dv.Sort = "Time";
                dt = dv.ToTable();
                DateTime minTime = DateTime.Parse(dt.Rows[0]["Time"].ToString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DateTime nowTime = DateTime.Parse(dr["Time"].ToString());
                    TimeSpan span = nowTime - minTime;
                    double time = (double)span.TotalSeconds / 60.0;
                    
                    dr["RecordKey"] = key;
                    dr["ExcelRowNum"] = i+1;
                    dr["TimeGap"] = time;
                }
                XtraMessageBox.Show(LangResx.Main.EditData_Success, "", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                return;
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
    }
}