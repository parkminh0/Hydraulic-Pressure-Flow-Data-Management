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

namespace WooSungEngineering
{
    public partial class FormPopSetY : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flow1"></param>
        /// <param name="pressure1"></param>
        /// <param name="flow2"></param>
        /// <param name="pressure2"></param>
        public FormPopSetY(double flow1, double pressure1, double flow2, double pressure2)
        {
            InitializeComponent();

            txtFlow1.Text = flow1 == 0 ? " " : flow1.ToString();
            txtPressure1.Text = pressure1 == 0 ? " " : pressure1.ToString();
            txtFlow2.Text = flow2 == 0 ? " " : flow2.ToString();
            txtPressure2.Text = pressure2 == 0 ? " " : pressure2.ToString();
            txtFlow1.ToolTip = txtFlow2.ToolTip = txtPressure1.ToolTip = txtPressure2.ToolTip = LangResx.Main.tooltip;
        }

        /// <summary>
        /// Y축 Max 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(LangResx.Main.ApplyMsg1, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Program.Option.Flow1 = double.Parse((string.IsNullOrEmpty(txtFlow1.Text.Trim())) ? "0" : txtFlow1.Text.Trim());
                    Program.Option.Pressure1 = double.Parse((string.IsNullOrEmpty(txtPressure1.Text.Trim())) ? "0" : txtPressure1.Text.Trim());
                    Program.Option.Flow2 = double.Parse((string.IsNullOrEmpty(txtFlow2.Text.Trim())) ? "0" : txtFlow2.Text.Trim());
                    Program.Option.Pressure2 = double.Parse((string.IsNullOrEmpty(txtPressure2.Text.Trim())) ? "0" : txtPressure2.Text.Trim());
                    XtraMessageBox.Show(LangResx.Main.ApplyMsg, "", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
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