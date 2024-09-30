namespace WooSungEngineering
{
    partial class FormPopSetY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopSetY));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.txtFlow2 = new DevExpress.XtraEditors.TextEdit();
            this.txtPressure1 = new DevExpress.XtraEditors.TextEdit();
            this.txtFlow1 = new DevExpress.XtraEditors.TextEdit();
            this.txtPressure2 = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlow2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPressure1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlow1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPressure2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.txtFlow2);
            this.layoutControl1.Controls.Add(this.txtPressure1);
            this.layoutControl1.Controls.Add(this.txtFlow1);
            this.layoutControl1.Controls.Add(this.txtPressure2);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnPrint);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.ImageOptions.ImageToTextIndent = 10;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnPrint.ImageOptions.ImageToTextIndent = 10;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtFlow2
            // 
            resources.ApplyResources(this.txtFlow2, "txtFlow2");
            this.txtFlow2.Name = "txtFlow2";
            this.txtFlow2.Properties.Appearance.Options.UseTextOptions = true;
            this.txtFlow2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFlow2.Properties.AutoHeight = ((bool)(resources.GetObject("txtFlow2.Properties.AutoHeight")));
            this.txtFlow2.StyleController = this.layoutControl1;
            // 
            // txtPressure1
            // 
            resources.ApplyResources(this.txtPressure1, "txtPressure1");
            this.txtPressure1.Name = "txtPressure1";
            this.txtPressure1.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPressure1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPressure1.Properties.AutoHeight = ((bool)(resources.GetObject("txtPressure1.Properties.AutoHeight")));
            this.txtPressure1.StyleController = this.layoutControl1;
            // 
            // txtFlow1
            // 
            resources.ApplyResources(this.txtFlow1, "txtFlow1");
            this.txtFlow1.Name = "txtFlow1";
            this.txtFlow1.Properties.Appearance.Options.UseTextOptions = true;
            this.txtFlow1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFlow1.Properties.AutoHeight = ((bool)(resources.GetObject("txtFlow1.Properties.AutoHeight")));
            this.txtFlow1.StyleController = this.layoutControl1;
            // 
            // txtPressure2
            // 
            resources.ApplyResources(this.txtPressure2, "txtPressure2");
            this.txtPressure2.Name = "txtPressure2";
            this.txtPressure2.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPressure2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPressure2.Properties.AutoHeight = ((bool)(resources.GetObject("txtPressure2.Properties.AutoHeight")));
            this.txtPressure2.StyleController = this.layoutControl1;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(237, 213);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem5.Control = this.txtPressure2;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 114);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(231, 38);
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.TextSize = new System.Drawing.Size(34, 12);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem1.Control = this.txtFlow1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(231, 38);
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.TextSize = new System.Drawing.Size(34, 12);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem2.Control = this.txtPressure1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(231, 38);
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.TextSize = new System.Drawing.Size(34, 12);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem3.Control = this.txtFlow2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(231, 38);
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.TextSize = new System.Drawing.Size(34, 12);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panelControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 152);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(231, 55);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // FormPopSetY
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("FormPopSetY.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPopSetY";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFlow2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPressure1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlow1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPressure2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtFlow2;
        private DevExpress.XtraEditors.TextEdit txtPressure1;
        private DevExpress.XtraEditors.TextEdit txtFlow1;
        private DevExpress.XtraEditors.TextEdit txtPressure2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
    }
}