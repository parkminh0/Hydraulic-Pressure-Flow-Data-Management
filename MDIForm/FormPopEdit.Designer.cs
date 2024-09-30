namespace WooSungEngineering
{
    partial class FormPopEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopEdit));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdProcessedData = new DevExpress.XtraGrid.GridControl();
            this.grdViewProcessedData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpbteDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProcessedData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewProcessedData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpbteDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Font = ((System.Drawing.Font)(resources.GetObject("groupControl2.AppearanceCaption.Font")));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.grdProcessedData);
            resources.ApplyResources(buttonImageOptions1, "buttonImageOptions1");
            this.groupControl2.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton(resources.GetString("groupControl2.CustomHeaderButtons"), ((bool)(resources.GetObject("groupControl2.CustomHeaderButtons1"))), buttonImageOptions1, ((DevExpress.XtraBars.Docking2010.ButtonStyle)(resources.GetObject("groupControl2.CustomHeaderButtons2"))), resources.GetString("groupControl2.CustomHeaderButtons3"), ((int)(resources.GetObject("groupControl2.CustomHeaderButtons4"))), ((bool)(resources.GetObject("groupControl2.CustomHeaderButtons5"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("groupControl2.CustomHeaderButtons6"))), ((bool)(resources.GetObject("groupControl2.CustomHeaderButtons7"))), ((bool)(resources.GetObject("groupControl2.CustomHeaderButtons8"))), ((bool)(resources.GetObject("groupControl2.CustomHeaderButtons9"))), resources.GetString("groupControl2.CustomHeaderButtons10"), ((int)(resources.GetObject("groupControl2.CustomHeaderButtons11"))))});
            this.groupControl2.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Name = "groupControl2";
            // 
            // grdProcessedData
            // 
            resources.ApplyResources(this.grdProcessedData, "grdProcessedData");
            this.grdProcessedData.LookAndFeel.SkinName = "WXI";
            this.grdProcessedData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grdProcessedData.MainView = this.grdViewProcessedData;
            this.grdProcessedData.Name = "grdProcessedData";
            this.grdProcessedData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpbteDelete});
            this.grdProcessedData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewProcessedData});
            // 
            // grdViewProcessedData
            // 
            this.grdViewProcessedData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn6});
            this.grdViewProcessedData.DetailHeight = 257;
            this.grdViewProcessedData.GridControl = this.grdProcessedData;
            this.grdViewProcessedData.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("grdViewProcessedData.GroupSummary"))), resources.GetString("grdViewProcessedData.GroupSummary1"), ((DevExpress.XtraGrid.Columns.GridColumn)(resources.GetObject("grdViewProcessedData.GroupSummary2"))), resources.GetString("grdViewProcessedData.GroupSummary3"))});
            this.grdViewProcessedData.Name = "grdViewProcessedData";
            resources.ApplyResources(this.grdViewProcessedData, "grdViewProcessedData");
            this.grdViewProcessedData.OptionsCustomization.AllowGroup = false;
            this.grdViewProcessedData.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.grdViewProcessedData.OptionsView.EnableAppearanceOddRow = true;
            this.grdViewProcessedData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grdViewProcessedData.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "Time";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "Flow1";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "Pressure1";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumn7, "gridColumn7");
            this.gridColumn7.FieldName = "Flow2";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumn8, "gridColumn8");
            this.gridColumn8.FieldName = "Pressure2";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.ColumnEdit = this.rpbteDelete;
            this.gridColumn6.FieldName = "Delete";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            // 
            // rpbteDelete
            // 
            resources.ApplyResources(this.rpbteDelete, "rpbteDelete");
            resources.ApplyResources(editorButtonImageOptions1, "editorButtonImageOptions1");
            this.rpbteDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rpbteDelete.Buttons"))), resources.GetString("rpbteDelete.Buttons1"), ((int)(resources.GetObject("rpbteDelete.Buttons2"))), ((bool)(resources.GetObject("rpbteDelete.Buttons3"))), ((bool)(resources.GetObject("rpbteDelete.Buttons4"))), ((bool)(resources.GetObject("rpbteDelete.Buttons5"))), editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, resources.GetString("rpbteDelete.Buttons6"), ((object)(resources.GetObject("rpbteDelete.Buttons7"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rpbteDelete.Buttons8"))), ((DevExpress.Utils.ToolTipAnchor)(resources.GetObject("rpbteDelete.Buttons9"))))});
            this.rpbteDelete.Name = "rpbteDelete";
            this.rpbteDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.rpbteDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rpbteDelete_ButtonClick);
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
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.ImageOptions.ImageToTextIndent = 10;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl1);
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.groupControl2);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnClose);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(494, 665);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl2;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(494, 616);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panelControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 616);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem4.Size = new System.Drawing.Size(494, 49);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // FormPopEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("FormPopEdit.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPopEdit";
            this.Shown += new System.EventHandler(this.FormPopEdit_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProcessedData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewProcessedData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpbteDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grdProcessedData;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewProcessedData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rpbteDelete;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}