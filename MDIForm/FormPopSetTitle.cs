using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WooSungEngineering
{
    public partial class FormPopSetTitle : DevExpress.XtraEditors.XtraForm
    {
        private int reportMode;
        /// <summary>
        /// 
        /// </summary>
        public FormPopSetTitle(int reportMode)
        {
            InitializeComponent();
            this.reportMode = reportMode;
        }

        /// <summary>
        /// 폼 로드 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPopSetTitle_Load(object sender, EventArgs e)
        {
            txtCompName.Text = Program.Option.CompName;
            pictureEdit1.Image = LogicManager.Common.ByteArrayToImage(Program.Option.ReportImage);
            switch (reportMode)
            {
                case 0:
                    btnPrint.Text = LangResx.Main.btnPrintPDF;
                    break;
                case 1:
                    btnPrint.Text = LangResx.Main.btnPrint;
                    break;
            }
        }

        /// <summary>
        /// 이미지 업로드, 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.ToString() == "Upload")
            {
                pictureEdit1.LoadImage();
            }
            else if (e.Button.Properties.Tag.ToString() == "Delete")
            {
                pictureEdit1.Image = null;
            }
        }

        /// <summary>
        /// 출력 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Program.Option.CompName = txtCompName.Text;
            Program.Option.ReportImage = LogicManager.Common.ImageToByteArray(pictureEdit1.Image);
            Program.SaveConfig();

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}