namespace WooSungEngineering
{
    partial class FormPopSetTitle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopSetTitle));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.txtCompName = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnPrint.ImageOptions.ImageToTextIndent = 10;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnPrint);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.pictureEdit1);
            this.layoutControl1.Controls.Add(this.txtCompName);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(347, 0, 650, 400);
            this.layoutControl1.Root = this.Root;
            // 
            // pictureEdit1
            // 
            resources.ApplyResources(this.pictureEdit1, "pictureEdit1");
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.StyleController = this.layoutControl1;
            // 
            // txtCompName
            // 
            resources.ApplyResources(this.txtCompName, "txtCompName");
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Properties.AutoHeight = ((bool)(resources.GetObject("txtCompName.Properties.AutoHeight")));
            this.txtCompName.StyleController = this.layoutControl1;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(370, 254);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(buttonImageOptions1, "buttonImageOptions1");
            resources.ApplyResources(buttonImageOptions2, "buttonImageOptions2");
            this.layoutControlGroup1.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton(resources.GetString("layoutControlGroup1.CustomHeaderButtons"), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons1"))), buttonImageOptions1, ((DevExpress.XtraBars.Docking2010.ButtonStyle)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons2"))), resources.GetString("layoutControlGroup1.CustomHeaderButtons3"), ((int)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons4"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons5"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons6"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons7"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons8"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons9"))), resources.GetString("layoutControlGroup1.CustomHeaderButtons10"), ((int)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons11")))),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton(resources.GetString("layoutControlGroup1.CustomHeaderButtons12"), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons13"))), buttonImageOptions2, ((DevExpress.XtraBars.Docking2010.ButtonStyle)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons14"))), resources.GetString("layoutControlGroup1.CustomHeaderButtons15"), ((int)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons16"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons17"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons18"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons19"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons20"))), ((bool)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons21"))), resources.GetString("layoutControlGroup1.CustomHeaderButtons22"), ((int)(resources.GetObject("layoutControlGroup1.CustomHeaderButtons23"))))});
            this.layoutControlGroup1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 43);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(364, 205);
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.layoutControlGroup1_CustomButtonClick);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.pictureEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(356, 170);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem1.Control = this.txtCompName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(246, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(364, 43);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(24, 12);
            // 
            // FormPopSetTitle
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("FormPopSetTitle.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPopSetTitle";
            this.Load += new System.EventHandler(this.FormPopSetTitle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.TextEdit txtCompName;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}