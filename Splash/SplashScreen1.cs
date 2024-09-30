using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace WooSungEngineering
{
    public partial class SplashScreen1 : SplashScreen
    {
        public SplashScreen1()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            SplashScreenCommand command = (SplashScreenCommand)cmd;
            if (command == SplashScreenCommand.SetProgress)
            {
                int pos = (int)arg;
                progressBarControl1.Position = pos;
            }

            //Set Status
            if (command == SplashScreenCommand.SetStatus)
            {
                string stat = (string)arg;
                labelControl2.Text = stat;
            }
        }

        #endregion

        public enum SplashScreenCommand
        {
            SetProgress,
            DbConnect,
            SetStatus
        }
    }
}