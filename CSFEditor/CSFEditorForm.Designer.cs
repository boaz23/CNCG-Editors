using System.Drawing;

namespace CNCGEditors {
    partial class CsfEditorForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (this.components != null) {
                    this.components.Dispose();
                }
                if (this.Fs_CsfFile != null) {
                    this.Fs_CsfFile.Close();
                }
                if (this.Sfd_CsfFile != null) {
                    this.Sfd_CsfFile.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.StsStrp = new System.Windows.Forms.StatusStrip();
            this.StsStrp_Label_CsfFile_LabelsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.StsStrp_Label_CsfFile_StringsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.StsStrp_Label_CsfLabel_StringsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.LstBox_CsfLabels = new System.Windows.Forms.ListBox();
            this.TxtBox_CsfLabel_Name = new System.Windows.Forms.TextBox();
            this.SpltCntnr_CsfLabelNameData = new System.Windows.Forms.SplitContainer();
            this.ChkBox_CsfLabel_OnlyString_IsRtl = new System.Windows.Forms.CheckBox();
            this.Lbl_CsfLabel_OnlyString_Value = new System.Windows.Forms.Label();
            this.TxtBox_CsfLabel_OnlyString_Value = new System.Windows.Forms.RichTextBox();
            this.Lbl_CsfLabel_Name = new System.Windows.Forms.Label();
            this.MnMnu = new System.Windows.Forms.MainMenu(this.components);
            this.MnMnu_File = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_New = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_Open = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_Close = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_Save = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_SaveAs = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_IO_History_Separator = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_ClearHistory = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_History_Exit_Separator = new System.Windows.Forms.MenuItem();
            this.MnMnu_File_Exit = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfLabelAdd = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfLabelInsertAt = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfLabelRemove = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfLabelRemoveAt = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfString_Separator = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfStringAdd = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfStringInsertAt = new System.Windows.Forms.MenuItem();
            this.MnMnu_Edit_CsfStringRemoveAt = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_AdvancedMode = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_DeleteCsfLabelConfirmation = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_DeleteCsfStringConfirmation = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_WordWarp = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_ShowHistory = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_Settings_Reset_Separator = new System.Windows.Forms.MenuItem();
            this.MnMnu_Settings_Reset = new System.Windows.Forms.MenuItem();
            this.MnMnu_Help = new System.Windows.Forms.MenuItem();
            this.MnMnu_Help_WhatIsAdvancedMode = new System.Windows.Forms.MenuItem();
            this.MnMnu_Help_CsfFileFormat = new System.Windows.Forms.MenuItem();
            this.Sfd_CsfFile = new System.Windows.Forms.SaveFileDialog();
            this.Ofd_CsfFile = new System.Windows.Forms.OpenFileDialog();
            this.ChkBox_AppendExtraBytes = new System.Windows.Forms.CheckBox();
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames = new System.Windows.Forms.CheckBox();
            this.CmbBox_CsfFileLanguage = new System.Windows.Forms.ComboBox();
            this.CmbBox_CsfFileVersion = new System.Windows.Forms.ComboBox();
            this.Lbl_CsfFileLanguage = new System.Windows.Forms.Label();
            this.Lbl_CsfFileUnused = new System.Windows.Forms.Label();
            this.Lbl_CsfFileVersion = new System.Windows.Forms.Label();
            this.NmrcUpDwn_CsfFileLanguage = new CNCGEditors.Windows.Forms.HexNumUpDown();
            this.NmrcUpDwn_CsfFileUnused = new CNCGEditors.Windows.Forms.HexNumUpDown();
            this.NmrcUpDwn_CsfFileVersion = new CNCGEditors.Windows.Forms.HexNumUpDown();
            this.StsStrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpltCntnr_CsfLabelNameData)).BeginInit();
            this.SpltCntnr_CsfLabelNameData.Panel1.SuspendLayout();
            this.SpltCntnr_CsfLabelNameData.Panel2.SuspendLayout();
            this.SpltCntnr_CsfLabelNameData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NmrcUpDwn_CsfFileLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NmrcUpDwn_CsfFileUnused)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NmrcUpDwn_CsfFileVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // StsStrp
            // 
            this.StsStrp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.StsStrp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StsStrp_Label_CsfFile_LabelsCount,
            this.StsStrp_Label_CsfFile_StringsCount,
            this.StsStrp_Label_CsfLabel_StringsCount});
            this.StsStrp.Location = new System.Drawing.Point(0, 563);
            this.StsStrp.Margin = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.StsStrp.Name = "StsStrp";
            this.StsStrp.Size = new System.Drawing.Size(784, 24);
            this.StsStrp.TabIndex = 1;
            // 
            // StsStrp_Label_CsfFile_LabelsCount
            // 
            this.StsStrp_Label_CsfFile_LabelsCount.AutoSize = false;
            this.StsStrp_Label_CsfFile_LabelsCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StsStrp_Label_CsfFile_LabelsCount.Name = "StsStrp_Label_CsfFile_LabelsCount";
            this.StsStrp_Label_CsfFile_LabelsCount.Size = new System.Drawing.Size(175, 19);
            this.StsStrp_Label_CsfFile_LabelsCount.Text = "File\'s Labels Count: ";
            this.StsStrp_Label_CsfFile_LabelsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StsStrp_Label_CsfFile_StringsCount
            // 
            this.StsStrp_Label_CsfFile_StringsCount.AutoSize = false;
            this.StsStrp_Label_CsfFile_StringsCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StsStrp_Label_CsfFile_StringsCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StsStrp_Label_CsfFile_StringsCount.Name = "StsStrp_Label_CsfFile_StringsCount";
            this.StsStrp_Label_CsfFile_StringsCount.Size = new System.Drawing.Size(175, 19);
            this.StsStrp_Label_CsfFile_StringsCount.Text = "File\'s Strings Count: ";
            this.StsStrp_Label_CsfFile_StringsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StsStrp_Label_CsfLabel_StringsCount
            // 
            this.StsStrp_Label_CsfLabel_StringsCount.AutoSize = false;
            this.StsStrp_Label_CsfLabel_StringsCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.StsStrp_Label_CsfLabel_StringsCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StsStrp_Label_CsfLabel_StringsCount.Name = "StsStrp_Label_CsfLabel_StringsCount";
            this.StsStrp_Label_CsfLabel_StringsCount.Size = new System.Drawing.Size(175, 19);
            this.StsStrp_Label_CsfLabel_StringsCount.Text = "Label\'s String Count: ";
            this.StsStrp_Label_CsfLabel_StringsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LstBox_CsfLabels
            // 
            this.LstBox_CsfLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstBox_CsfLabels.FormattingEnabled = true;
            this.LstBox_CsfLabels.IntegralHeight = false;
            this.LstBox_CsfLabels.Location = new System.Drawing.Point(0, 0);
            this.LstBox_CsfLabels.Margin = new System.Windows.Forms.Padding(0);
            this.LstBox_CsfLabels.MinimumSize = new System.Drawing.Size(150, 201);
            this.LstBox_CsfLabels.Name = "LstBox_CsfLabels";
            this.LstBox_CsfLabels.Size = new System.Drawing.Size(262, 408);
            this.LstBox_CsfLabels.TabIndex = 2;
            this.LstBox_CsfLabels.SelectedIndexChanged += new System.EventHandler(this.LstBox_CsfLabels_SelectedIndexChanged);
            this.LstBox_CsfLabels.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.LstBox_CsfLabels_Format);
            // 
            // TxtBox_CsfLabel_Name
            // 
            this.TxtBox_CsfLabel_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBox_CsfLabel_Name.BackColor = System.Drawing.SystemColors.Window;
            this.TxtBox_CsfLabel_Name.Enabled = false;
            this.TxtBox_CsfLabel_Name.Location = new System.Drawing.Point(12, 31);
            this.TxtBox_CsfLabel_Name.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.TxtBox_CsfLabel_Name.MinimumSize = new System.Drawing.Size(215, 20);
            this.TxtBox_CsfLabel_Name.Name = "TxtBox_CsfLabel_Name";
            this.TxtBox_CsfLabel_Name.Size = new System.Drawing.Size(462, 20);
            this.TxtBox_CsfLabel_Name.TabIndex = 3;
            // 
            // SpltCntnr_CsfLabelNameData
            // 
            this.SpltCntnr_CsfLabelNameData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpltCntnr_CsfLabelNameData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SpltCntnr_CsfLabelNameData.Location = new System.Drawing.Point(12, 139);
            this.SpltCntnr_CsfLabelNameData.Name = "SpltCntnr_CsfLabelNameData";
            // 
            // SpltCntnr_CsfLabelNameData.Panel1
            // 
            this.SpltCntnr_CsfLabelNameData.Panel1.AutoScroll = true;
            this.SpltCntnr_CsfLabelNameData.Panel1.Controls.Add(this.LstBox_CsfLabels);
            // 
            // SpltCntnr_CsfLabelNameData.Panel2
            // 
            this.SpltCntnr_CsfLabelNameData.Panel2.AutoScroll = true;
            this.SpltCntnr_CsfLabelNameData.Panel2.Controls.Add(this.ChkBox_CsfLabel_OnlyString_IsRtl);
            this.SpltCntnr_CsfLabelNameData.Panel2.Controls.Add(this.Lbl_CsfLabel_OnlyString_Value);
            this.SpltCntnr_CsfLabelNameData.Panel2.Controls.Add(this.TxtBox_CsfLabel_OnlyString_Value);
            this.SpltCntnr_CsfLabelNameData.Panel2.Controls.Add(this.Lbl_CsfLabel_Name);
            this.SpltCntnr_CsfLabelNameData.Panel2.Controls.Add(this.TxtBox_CsfLabel_Name);
            this.SpltCntnr_CsfLabelNameData.Size = new System.Drawing.Size(760, 412);
            this.SpltCntnr_CsfLabelNameData.SplitterDistance = 266;
            this.SpltCntnr_CsfLabelNameData.TabIndex = 4;
            // 
            // ChkBox_CsfLabel_OnlyString_IsRtl
            // 
            this.ChkBox_CsfLabel_OnlyString_IsRtl.AutoSize = true;
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Enabled = false;
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Location = new System.Drawing.Point(12, 172);
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Margin = new System.Windows.Forms.Padding(12, 3, 3, 12);
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Name = "ChkBox_CsfLabel_OnlyString_IsRtl";
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Size = new System.Drawing.Size(80, 17);
            this.ChkBox_CsfLabel_OnlyString_IsRtl.TabIndex = 18;
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Text = "Right to left";
            this.ChkBox_CsfLabel_OnlyString_IsRtl.UseVisualStyleBackColor = true;
            // 
            // Lbl_CsfLabel_OnlyString_Value
            // 
            this.Lbl_CsfLabel_OnlyString_Value.AutoSize = true;
            this.Lbl_CsfLabel_OnlyString_Value.Enabled = false;
            this.Lbl_CsfLabel_OnlyString_Value.Location = new System.Drawing.Point(9, 63);
            this.Lbl_CsfLabel_OnlyString_Value.Margin = new System.Windows.Forms.Padding(9, 9, 0, 3);
            this.Lbl_CsfLabel_OnlyString_Value.Name = "Lbl_CsfLabel_OnlyString_Value";
            this.Lbl_CsfLabel_OnlyString_Value.Size = new System.Drawing.Size(37, 13);
            this.Lbl_CsfLabel_OnlyString_Value.TabIndex = 6;
            this.Lbl_CsfLabel_OnlyString_Value.Text = "Value:";
            // 
            // TxtBox_CsfLabel_OnlyString_Value
            // 
            this.TxtBox_CsfLabel_OnlyString_Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBox_CsfLabel_OnlyString_Value.DataBindings.Add(new System.Windows.Forms.Binding("WordWrap", global::CNCGEditors.Properties.Settings.Default, "WordWarp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtBox_CsfLabel_OnlyString_Value.Enabled = false;
            this.TxtBox_CsfLabel_OnlyString_Value.Location = new System.Drawing.Point(12, 82);
            this.TxtBox_CsfLabel_OnlyString_Value.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.TxtBox_CsfLabel_OnlyString_Value.MinimumSize = new System.Drawing.Size(215, 84);
            this.TxtBox_CsfLabel_OnlyString_Value.Name = "TxtBox_CsfLabel_OnlyString_Value";
            this.TxtBox_CsfLabel_OnlyString_Value.Size = new System.Drawing.Size(462, 84);
            this.TxtBox_CsfLabel_OnlyString_Value.TabIndex = 5;
            this.TxtBox_CsfLabel_OnlyString_Value.Text = "";
            this.TxtBox_CsfLabel_OnlyString_Value.WordWrap = global::CNCGEditors.Properties.Settings.Default.WordWarp;
            // 
            // Lbl_CsfLabel_Name
            // 
            this.Lbl_CsfLabel_Name.AutoSize = true;
            this.Lbl_CsfLabel_Name.Enabled = false;
            this.Lbl_CsfLabel_Name.Location = new System.Drawing.Point(9, 12);
            this.Lbl_CsfLabel_Name.Margin = new System.Windows.Forms.Padding(9, 12, 0, 3);
            this.Lbl_CsfLabel_Name.Name = "Lbl_CsfLabel_Name";
            this.Lbl_CsfLabel_Name.Size = new System.Drawing.Size(38, 13);
            this.Lbl_CsfLabel_Name.TabIndex = 4;
            this.Lbl_CsfLabel_Name.Text = "Name:";
            // 
            // MnMnu
            // 
            this.MnMnu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnMnu_File,
            this.MnMnu_Edit,
            this.MnMnu_Settings,
            this.MnMnu_Help});
            this.MnMnu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // MnMnu_File
            // 
            this.MnMnu_File.Index = 0;
            this.MnMnu_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnMnu_File_New,
            this.MnMnu_File_Open,
            this.MnMnu_File_Close,
            this.MnMnu_File_Save,
            this.MnMnu_File_SaveAs,
            this.MnMnu_File_IO_History_Separator,
            this.MnMnu_File_ClearHistory,
            this.MnMnu_File_History_Exit_Separator,
            this.MnMnu_File_Exit});
            this.MnMnu_File.Text = "&File";
            // 
            // MnMnu_File_New
            // 
            this.MnMnu_File_New.Index = 0;
            this.MnMnu_File_New.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.MnMnu_File_New.Text = "New";
            this.MnMnu_File_New.Click += new System.EventHandler(this.MnMnu_File_New_Click);
            // 
            // MnMnu_File_Open
            // 
            this.MnMnu_File_Open.Index = 1;
            this.MnMnu_File_Open.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.MnMnu_File_Open.Text = "Open";
            this.MnMnu_File_Open.Click += new System.EventHandler(this.MnMnu_File_Open_Click);
            // 
            // MnMnu_File_Close
            // 
            this.MnMnu_File_Close.Enabled = false;
            this.MnMnu_File_Close.Index = 2;
            this.MnMnu_File_Close.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.MnMnu_File_Close.Text = "Close";
            this.MnMnu_File_Close.Click += new System.EventHandler(this.MnMnu_File_Close_Click);
            // 
            // MnMnu_File_Save
            // 
            this.MnMnu_File_Save.Enabled = false;
            this.MnMnu_File_Save.Index = 3;
            this.MnMnu_File_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.MnMnu_File_Save.Text = "Save";
            this.MnMnu_File_Save.Click += new System.EventHandler(this.MnMnu_File_Save_Click);
            // 
            // MnMnu_File_SaveAs
            // 
            this.MnMnu_File_SaveAs.Enabled = false;
            this.MnMnu_File_SaveAs.Index = 4;
            this.MnMnu_File_SaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.MnMnu_File_SaveAs.Text = "Save As";
            this.MnMnu_File_SaveAs.Click += new System.EventHandler(this.MnMnu_File_SaveAs_Click);
            // 
            // MnMnu_File_IO_History_Separator
            // 
            this.MnMnu_File_IO_History_Separator.Index = 5;
            this.MnMnu_File_IO_History_Separator.Text = "-";
            // 
            // MnMnu_File_ClearHistory
            // 
            this.MnMnu_File_ClearHistory.Enabled = false;
            this.MnMnu_File_ClearHistory.Index = 6;
            this.MnMnu_File_ClearHistory.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.MnMnu_File_ClearHistory.Text = "Clear History";
            this.MnMnu_File_ClearHistory.Click += new System.EventHandler(this.MnMnu_File_ClearHistory_Click);
            // 
            // MnMnu_File_History_Exit_Separator
            // 
            this.MnMnu_File_History_Exit_Separator.Index = 7;
            this.MnMnu_File_History_Exit_Separator.Text = "-";
            // 
            // MnMnu_File_Exit
            // 
            this.MnMnu_File_Exit.Index = 8;
            this.MnMnu_File_Exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.MnMnu_File_Exit.Text = "Exit";
            this.MnMnu_File_Exit.Click += new System.EventHandler(this.MnMnu_File_Exit_Click);
            // 
            // MnMnu_Edit
            // 
            this.MnMnu_Edit.Enabled = false;
            this.MnMnu_Edit.Index = 1;
            this.MnMnu_Edit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnMnu_Edit_CsfLabelAdd,
            this.MnMnu_Edit_CsfLabelInsertAt,
            this.MnMnu_Edit_CsfLabelRemove,
            this.MnMnu_Edit_CsfLabelRemoveAt,
            this.MnMnu_Edit_CsfString_Separator,
            this.MnMnu_Edit_CsfStringAdd,
            this.MnMnu_Edit_CsfStringInsertAt,
            this.MnMnu_Edit_CsfStringRemoveAt});
            this.MnMnu_Edit.Text = "&Edit";
            // 
            // MnMnu_Edit_CsfLabelAdd
            // 
            this.MnMnu_Edit_CsfLabelAdd.Enabled = false;
            this.MnMnu_Edit_CsfLabelAdd.Index = 0;
            this.MnMnu_Edit_CsfLabelAdd.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.MnMnu_Edit_CsfLabelAdd.Text = "Add Label";
            this.MnMnu_Edit_CsfLabelAdd.Click += new System.EventHandler(this.MnMnu_Edit_CsfLabelAdd_Click);
            // 
            // MnMnu_Edit_CsfLabelInsertAt
            // 
            this.MnMnu_Edit_CsfLabelInsertAt.Enabled = false;
            this.MnMnu_Edit_CsfLabelInsertAt.Index = 1;
            this.MnMnu_Edit_CsfLabelInsertAt.Shortcut = System.Windows.Forms.Shortcut.ShiftF1;
            this.MnMnu_Edit_CsfLabelInsertAt.Text = "Insert Label At";
            this.MnMnu_Edit_CsfLabelInsertAt.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // MnMnu_Edit_CsfLabelRemove
            // 
            this.MnMnu_Edit_CsfLabelRemove.Enabled = false;
            this.MnMnu_Edit_CsfLabelRemove.Index = 2;
            this.MnMnu_Edit_CsfLabelRemove.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.MnMnu_Edit_CsfLabelRemove.Text = "Remove Label";
            // 
            // MnMnu_Edit_CsfLabelRemoveAt
            // 
            this.MnMnu_Edit_CsfLabelRemoveAt.Enabled = false;
            this.MnMnu_Edit_CsfLabelRemoveAt.Index = 3;
            this.MnMnu_Edit_CsfLabelRemoveAt.Shortcut = System.Windows.Forms.Shortcut.ShiftF3;
            this.MnMnu_Edit_CsfLabelRemoveAt.Text = "Remove Label At";
            this.MnMnu_Edit_CsfLabelRemoveAt.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // MnMnu_Edit_CsfString_Separator
            // 
            this.MnMnu_Edit_CsfString_Separator.Enabled = false;
            this.MnMnu_Edit_CsfString_Separator.Index = 4;
            this.MnMnu_Edit_CsfString_Separator.Text = "-";
            this.MnMnu_Edit_CsfString_Separator.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // MnMnu_Edit_CsfStringAdd
            // 
            this.MnMnu_Edit_CsfStringAdd.Enabled = false;
            this.MnMnu_Edit_CsfStringAdd.Index = 5;
            this.MnMnu_Edit_CsfStringAdd.Shortcut = System.Windows.Forms.Shortcut.CtrlF1;
            this.MnMnu_Edit_CsfStringAdd.Text = "Add String";
            this.MnMnu_Edit_CsfStringAdd.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // MnMnu_Edit_CsfStringInsertAt
            // 
            this.MnMnu_Edit_CsfStringInsertAt.Enabled = false;
            this.MnMnu_Edit_CsfStringInsertAt.Index = 6;
            this.MnMnu_Edit_CsfStringInsertAt.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF1;
            this.MnMnu_Edit_CsfStringInsertAt.Text = "Insert String At";
            this.MnMnu_Edit_CsfStringInsertAt.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // MnMnu_Edit_CsfStringRemoveAt
            // 
            this.MnMnu_Edit_CsfStringRemoveAt.Enabled = false;
            this.MnMnu_Edit_CsfStringRemoveAt.Index = 7;
            this.MnMnu_Edit_CsfStringRemoveAt.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF3;
            this.MnMnu_Edit_CsfStringRemoveAt.Text = "Remove String At";
            this.MnMnu_Edit_CsfStringRemoveAt.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // MnMnu_Settings
            // 
            this.MnMnu_Settings.Index = 2;
            this.MnMnu_Settings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnMnu_Settings_AdvancedMode,
            this.MnMnu_Settings_DeleteCsfLabelConfirmation,
            this.MnMnu_Settings_DeleteCsfStringConfirmation,
            this.MnMnu_Settings_WordWarp,
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy,
            this.MnMnu_Settings_ShowHistory,
            this.MnMnu_Settings_Settings_Reset_Separator,
            this.MnMnu_Settings_Reset});
            this.MnMnu_Settings.Text = "&Settings";
            // 
            // MnMnu_Settings_AdvancedMode
            // 
            this.MnMnu_Settings_AdvancedMode.Checked = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.MnMnu_Settings_AdvancedMode.Index = 0;
            this.MnMnu_Settings_AdvancedMode.Text = "Advanced Mode";
            this.MnMnu_Settings_AdvancedMode.Click += new System.EventHandler(this.MnMnu_Settings_AdvancedMode_Click);
            // 
            // MnMnu_Settings_DeleteCsfLabelConfirmation
            // 
            this.MnMnu_Settings_DeleteCsfLabelConfirmation.Checked = global::CNCGEditors.Properties.Settings.Default.DeleteCsfLabelsConfirmation;
            this.MnMnu_Settings_DeleteCsfLabelConfirmation.Index = 1;
            this.MnMnu_Settings_DeleteCsfLabelConfirmation.Text = "Delete Label Confirmation";
            this.MnMnu_Settings_DeleteCsfLabelConfirmation.Click += new System.EventHandler(this.MnMnu_Settings_DeleteLabelConfirmation_Click);
            // 
            // MnMnu_Settings_DeleteCsfStringConfirmation
            // 
            this.MnMnu_Settings_DeleteCsfStringConfirmation.Checked = global::CNCGEditors.Properties.Settings.Default.DeleteCsfStringsConfirmation;
            this.MnMnu_Settings_DeleteCsfStringConfirmation.Index = 2;
            this.MnMnu_Settings_DeleteCsfStringConfirmation.Text = "Delete String Confirmation";
            this.MnMnu_Settings_DeleteCsfStringConfirmation.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.MnMnu_Settings_DeleteCsfStringConfirmation.Click += new System.EventHandler(this.MnMnu_Settings_DeleteCsfStringConfirmation_Click);
            // 
            // MnMnu_Settings_WordWarp
            // 
            this.MnMnu_Settings_WordWarp.Checked = global::CNCGEditors.Properties.Settings.Default.WordWarp;
            this.MnMnu_Settings_WordWarp.Index = 3;
            this.MnMnu_Settings_WordWarp.Text = "Word Warp";
            this.MnMnu_Settings_WordWarp.Click += new System.EventHandler(this.MnMnu_Settings_WordWarp_Click);
            // 
            // MnMnu_Settings_SortListBoxCsfLabelsNamesBy
            // 
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy.Index = 4;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name,
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index});
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy.Text = "Sort Labels Names List Box By";
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name
            // 
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name.Checked = global::CNCGEditors.Properties.Settings.Default.CsfLabels_SortBy_Name;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name.Index = 0;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name.RadioCheck = true;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name.Text = "Label\'s Name";
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name.Click += new System.EventHandler(this.MnMnu_Settings_SortListBoxCsfFileLabelsNamesBy_CsfFileLabel_Name_Click);
            // 
            // MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index
            // 
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index.Checked = global::CNCGEditors.Properties.Settings.Default.CsfLabels_SortBy_Index;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index.Index = 1;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index.RadioCheck = true;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index.Text = "Label\'s Appearance Order";
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index.Click += new System.EventHandler(this.MnMnu_Settings_SortListBoxCsfFileLabelsNamesBy_CsfFileLabel_Index_Click);
            // 
            // MnMnu_Settings_ShowHistory
            // 
            this.MnMnu_Settings_ShowHistory.Checked = global::CNCGEditors.Properties.Settings.Default.ShowHistory;
            this.MnMnu_Settings_ShowHistory.Index = 5;
            this.MnMnu_Settings_ShowHistory.Text = "Show History";
            this.MnMnu_Settings_ShowHistory.Click += new System.EventHandler(this.MnMnu_Settings_ShowHistory_Click);
            // 
            // MnMnu_Settings_Settings_Reset_Separator
            // 
            this.MnMnu_Settings_Settings_Reset_Separator.Index = 6;
            this.MnMnu_Settings_Settings_Reset_Separator.Text = "-";
            // 
            // MnMnu_Settings_Reset
            // 
            this.MnMnu_Settings_Reset.Enabled = false;
            this.MnMnu_Settings_Reset.Index = 7;
            this.MnMnu_Settings_Reset.Text = "Reset Settings";
            this.MnMnu_Settings_Reset.Click += new System.EventHandler(this.MnMnu_Settings_Reset_Click);
            // 
            // MnMnu_Help
            // 
            this.MnMnu_Help.Index = 3;
            this.MnMnu_Help.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnMnu_Help_WhatIsAdvancedMode,
            this.MnMnu_Help_CsfFileFormat});
            this.MnMnu_Help.Text = "Help";
            // 
            // MnMnu_Help_WhatIsAdvancedMode
            // 
            this.MnMnu_Help_WhatIsAdvancedMode.Index = 0;
            this.MnMnu_Help_WhatIsAdvancedMode.Text = "What\'s Advanced Mode";
            this.MnMnu_Help_WhatIsAdvancedMode.Click += new System.EventHandler(this.MnMnu_Help_WhatIsAdvancedMode_Click);
            // 
            // MnMnu_Help_CsfFileFormat
            // 
            this.MnMnu_Help_CsfFileFormat.Index = 1;
            this.MnMnu_Help_CsfFileFormat.Text = "CSF File Format";
            this.MnMnu_Help_CsfFileFormat.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.MnMnu_Help_CsfFileFormat.Click += new System.EventHandler(this.MnMnu_Help_CsfFileFormat_Click);
            // 
            // Sfd_CsfFile
            // 
            this.Sfd_CsfFile.DefaultExt = "csf";
            this.Sfd_CsfFile.Filter = "CSF files|*.csf|All files|*.*";
            this.Sfd_CsfFile.RestoreDirectory = true;
            this.Sfd_CsfFile.SupportMultiDottedExtensions = true;
            // 
            // Ofd_CsfFile
            // 
            this.Ofd_CsfFile.DefaultExt = "csf";
            this.Ofd_CsfFile.Filter = "CSF files|*.csf|All files|*.*";
            this.Ofd_CsfFile.RestoreDirectory = true;
            this.Ofd_CsfFile.ShowReadOnly = true;
            this.Ofd_CsfFile.SupportMultiDottedExtensions = true;
            // 
            // ChkBox_AppendExtraBytes
            // 
            this.ChkBox_AppendExtraBytes.AutoSize = true;
            this.ChkBox_AppendExtraBytes.Checked = true;
            this.ChkBox_AppendExtraBytes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkBox_AppendExtraBytes.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ChkBox_AppendExtraBytes.Enabled = false;
            this.ChkBox_AppendExtraBytes.Location = new System.Drawing.Point(15, 110);
            this.ChkBox_AppendExtraBytes.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.ChkBox_AppendExtraBytes.Name = "ChkBox_AppendExtraBytes";
            this.ChkBox_AppendExtraBytes.Size = new System.Drawing.Size(269, 17);
            this.ChkBox_AppendExtraBytes.TabIndex = 17;
            this.ChkBox_AppendExtraBytes.Text = "Keep the extra bytes at the end of the file after write";
            this.ChkBox_AppendExtraBytes.UseVisualStyleBackColor = true;
            this.ChkBox_AppendExtraBytes.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.ChkBox_AppendExtraBytes.CheckedChanged += new System.EventHandler(this.ChkBox_AppendExtraBytes_CheckedChanged);
            // 
            // ChkBox_CmbBox_CsfFileLanguage_SortByNames
            // 
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.AutoSize = true;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Checked = true;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Enabled = false;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Location = new System.Drawing.Point(460, 79);
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Name = "ChkBox_CmbBox_CsfFileLanguage_SortByNames";
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Size = new System.Drawing.Size(96, 17);
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.TabIndex = 3;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Text = "Sort By Names";
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.UseVisualStyleBackColor = true;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.CheckedChanged += new System.EventHandler(this.ChkBox_CmbBox_CsfFileLanguage_SortByNames_CheckedChanged);
            // 
            // CmbBox_CsfFileLanguage
            // 
            this.CmbBox_CsfFileLanguage.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CmbBox_CsfFileLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBox_CsfFileLanguage.Enabled = false;
            this.CmbBox_CsfFileLanguage.FormattingEnabled = true;
            this.CmbBox_CsfFileLanguage.IntegralHeight = false;
            this.CmbBox_CsfFileLanguage.Location = new System.Drawing.Point(76, 77);
            this.CmbBox_CsfFileLanguage.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.CmbBox_CsfFileLanguage.Name = "CmbBox_CsfFileLanguage";
            this.CmbBox_CsfFileLanguage.Size = new System.Drawing.Size(180, 21);
            this.CmbBox_CsfFileLanguage.TabIndex = 16;
            this.CmbBox_CsfFileLanguage.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.CmbBox_CsfFileLanguage.SelectedIndexChanged += new System.EventHandler(this.CmbBox_CsfFileLanguage_SelectedIndexChanged);
            this.CmbBox_CsfFileLanguage.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.CmbBox_CsfFileLanguage_Format);
            // 
            // CmbBox_CsfFileVersion
            // 
            this.CmbBox_CsfFileVersion.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CmbBox_CsfFileVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBox_CsfFileVersion.Enabled = false;
            this.CmbBox_CsfFileVersion.FormattingEnabled = true;
            this.CmbBox_CsfFileVersion.IntegralHeight = false;
            this.CmbBox_CsfFileVersion.Location = new System.Drawing.Point(76, 12);
            this.CmbBox_CsfFileVersion.Name = "CmbBox_CsfFileVersion";
            this.CmbBox_CsfFileVersion.Size = new System.Drawing.Size(180, 21);
            this.CmbBox_CsfFileVersion.TabIndex = 13;
            this.CmbBox_CsfFileVersion.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.CmbBox_CsfFileVersion.SelectedIndexChanged += new System.EventHandler(this.CmbBox_CsfFileVersion_SelectedIndexChanged);
            this.CmbBox_CsfFileVersion.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.CmbBox_CsfFileVersion_Format);
            // 
            // Lbl_CsfFileLanguage
            // 
            this.Lbl_CsfFileLanguage.AutoSize = true;
            this.Lbl_CsfFileLanguage.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Lbl_CsfFileLanguage.Enabled = false;
            this.Lbl_CsfFileLanguage.Location = new System.Drawing.Point(12, 80);
            this.Lbl_CsfFileLanguage.Name = "Lbl_CsfFileLanguage";
            this.Lbl_CsfFileLanguage.Size = new System.Drawing.Size(58, 13);
            this.Lbl_CsfFileLanguage.TabIndex = 9;
            this.Lbl_CsfFileLanguage.Text = "Language:";
            this.Lbl_CsfFileLanguage.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // Lbl_CsfFileUnused
            // 
            this.Lbl_CsfFileUnused.AutoSize = true;
            this.Lbl_CsfFileUnused.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Lbl_CsfFileUnused.Enabled = false;
            this.Lbl_CsfFileUnused.Location = new System.Drawing.Point(12, 47);
            this.Lbl_CsfFileUnused.Name = "Lbl_CsfFileUnused";
            this.Lbl_CsfFileUnused.Size = new System.Drawing.Size(47, 13);
            this.Lbl_CsfFileUnused.TabIndex = 8;
            this.Lbl_CsfFileUnused.Text = "Unused:";
            this.Lbl_CsfFileUnused.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // Lbl_CsfFileVersion
            // 
            this.Lbl_CsfFileVersion.AutoSize = true;
            this.Lbl_CsfFileVersion.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Lbl_CsfFileVersion.Enabled = false;
            this.Lbl_CsfFileVersion.Location = new System.Drawing.Point(12, 15);
            this.Lbl_CsfFileVersion.Name = "Lbl_CsfFileVersion";
            this.Lbl_CsfFileVersion.Size = new System.Drawing.Size(45, 13);
            this.Lbl_CsfFileVersion.TabIndex = 6;
            this.Lbl_CsfFileVersion.Text = "Version:";
            this.Lbl_CsfFileVersion.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            // 
            // NmrcUpDwn_CsfFileLanguage
            // 
            this.NmrcUpDwn_CsfFileLanguage.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NmrcUpDwn_CsfFileLanguage.Enabled = false;
            this.NmrcUpDwn_CsfFileLanguage.Hexadecimal = true;
            this.NmrcUpDwn_CsfFileLanguage.Location = new System.Drawing.Point(268, 78);
            this.NmrcUpDwn_CsfFileLanguage.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
            this.NmrcUpDwn_CsfFileLanguage.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.NmrcUpDwn_CsfFileLanguage.Name = "NmrcUpDwn_CsfFileLanguage";
            this.NmrcUpDwn_CsfFileLanguage.Size = new System.Drawing.Size(180, 20);
            this.NmrcUpDwn_CsfFileLanguage.TabIndex = 15;
            this.NmrcUpDwn_CsfFileLanguage.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.NmrcUpDwn_CsfFileLanguage.ValueChanged += new System.EventHandler(this.NmrcUpDwn_CsfFileLanguage_ValueChanged);
            // 
            // NmrcUpDwn_CsfFileUnused
            // 
            this.NmrcUpDwn_CsfFileUnused.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NmrcUpDwn_CsfFileUnused.Enabled = false;
            this.NmrcUpDwn_CsfFileUnused.Hexadecimal = true;
            this.NmrcUpDwn_CsfFileUnused.Location = new System.Drawing.Point(76, 45);
            this.NmrcUpDwn_CsfFileUnused.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.NmrcUpDwn_CsfFileUnused.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.NmrcUpDwn_CsfFileUnused.Name = "NmrcUpDwn_CsfFileUnused";
            this.NmrcUpDwn_CsfFileUnused.Size = new System.Drawing.Size(180, 20);
            this.NmrcUpDwn_CsfFileUnused.TabIndex = 14;
            this.NmrcUpDwn_CsfFileUnused.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.NmrcUpDwn_CsfFileUnused.ValueChanged += new System.EventHandler(this.NmrcUpDwn_CsfFileUnused_ValueChanged);
            // 
            // NmrcUpDwn_CsfFileVersion
            // 
            this.NmrcUpDwn_CsfFileVersion.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::CNCGEditors.Properties.Settings.Default, "AdvancedMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NmrcUpDwn_CsfFileVersion.Enabled = false;
            this.NmrcUpDwn_CsfFileVersion.Hexadecimal = true;
            this.NmrcUpDwn_CsfFileVersion.Location = new System.Drawing.Point(268, 13);
            this.NmrcUpDwn_CsfFileVersion.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
            this.NmrcUpDwn_CsfFileVersion.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.NmrcUpDwn_CsfFileVersion.Name = "NmrcUpDwn_CsfFileVersion";
            this.NmrcUpDwn_CsfFileVersion.Size = new System.Drawing.Size(180, 20);
            this.NmrcUpDwn_CsfFileVersion.TabIndex = 5;
            this.NmrcUpDwn_CsfFileVersion.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.NmrcUpDwn_CsfFileVersion.Visible = global::CNCGEditors.Properties.Settings.Default.AdvancedMode;
            this.NmrcUpDwn_CsfFileVersion.ValueChanged += new System.EventHandler(this.NmrcUpDwn_CsfFileVersion_ValueChanged);
            // 
            // CsfEditorForm
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 587);
            this.Controls.Add(this.ChkBox_AppendExtraBytes);
            this.Controls.Add(this.ChkBox_CmbBox_CsfFileLanguage_SortByNames);
            this.Controls.Add(this.CmbBox_CsfFileLanguage);
            this.Controls.Add(this.NmrcUpDwn_CsfFileLanguage);
            this.Controls.Add(this.NmrcUpDwn_CsfFileUnused);
            this.Controls.Add(this.CmbBox_CsfFileVersion);
            this.Controls.Add(this.Lbl_CsfFileLanguage);
            this.Controls.Add(this.Lbl_CsfFileUnused);
            this.Controls.Add(this.Lbl_CsfFileVersion);
            this.Controls.Add(this.NmrcUpDwn_CsfFileVersion);
            this.Controls.Add(this.SpltCntnr_CsfLabelNameData);
            this.Controls.Add(this.StsStrp);
            this.DoubleBuffered = true;
            this.Menu = this.MnMnu;
            this.MinimumSize = new System.Drawing.Size(584, 439);
            this.Name = "CsfEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSF Editor";
            this.Load += new System.EventHandler(this.CsFEditorForm_Load);
            this.StsStrp.ResumeLayout(false);
            this.StsStrp.PerformLayout();
            this.SpltCntnr_CsfLabelNameData.Panel1.ResumeLayout(false);
            this.SpltCntnr_CsfLabelNameData.Panel2.ResumeLayout(false);
            this.SpltCntnr_CsfLabelNameData.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpltCntnr_CsfLabelNameData)).EndInit();
            this.SpltCntnr_CsfLabelNameData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NmrcUpDwn_CsfFileLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NmrcUpDwn_CsfFileUnused)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NmrcUpDwn_CsfFileVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip StsStrp;
        private System.Windows.Forms.ListBox LstBox_CsfLabels;
        private System.Windows.Forms.TextBox TxtBox_CsfLabel_Name;
        private System.Windows.Forms.SplitContainer SpltCntnr_CsfLabelNameData;
        private System.Windows.Forms.ToolStripStatusLabel StsStrp_Label_CsfFile_LabelsCount;
        private System.Windows.Forms.ToolStripStatusLabel StsStrp_Label_CsfFile_StringsCount;
        private System.Windows.Forms.RichTextBox TxtBox_CsfLabel_OnlyString_Value;
        private System.Windows.Forms.Label Lbl_CsfLabel_OnlyString_Value;
        private System.Windows.Forms.MainMenu MnMnu;
        private System.Windows.Forms.MenuItem MnMnu_File;
        private System.Windows.Forms.MenuItem MnMnu_File_New;
        private System.Windows.Forms.MenuItem MnMnu_File_Open;
        private System.Windows.Forms.MenuItem MnMnu_File_Close;
        private System.Windows.Forms.MenuItem MnMnu_File_Save;
        private System.Windows.Forms.MenuItem MnMnu_File_SaveAs;
        private System.Windows.Forms.MenuItem MnMnu_File_IO_History_Separator;
        private System.Windows.Forms.MenuItem MnMnu_File_ClearHistory;
        private System.Windows.Forms.MenuItem MnMnu_File_History_Exit_Separator;
        private System.Windows.Forms.MenuItem MnMnu_File_Exit;
        private System.Windows.Forms.MenuItem MnMnu_Edit;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfLabelAdd;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfLabelInsertAt;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfLabelRemove;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfLabelRemoveAt;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfString_Separator;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfStringAdd;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfStringInsertAt;
        private System.Windows.Forms.MenuItem MnMnu_Edit_CsfStringRemoveAt;
        private System.Windows.Forms.MenuItem MnMnu_Settings;
        private System.Windows.Forms.MenuItem MnMnu_Settings_AdvancedMode;
        private System.Windows.Forms.MenuItem MnMnu_Settings_SortListBoxCsfLabelsNamesBy;
        private System.Windows.Forms.MenuItem MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name;
        private System.Windows.Forms.MenuItem MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index;
        private System.Windows.Forms.SaveFileDialog Sfd_CsfFile;
        private System.Windows.Forms.MenuItem MnMnu_Help;
        private System.Windows.Forms.MenuItem MnMnu_Help_WhatIsAdvancedMode;
        private System.Windows.Forms.MenuItem MnMnu_Help_CsfFileFormat;
        private System.Windows.Forms.MenuItem MnMnu_Settings_Settings_Reset_Separator;
        private System.Windows.Forms.MenuItem MnMnu_Settings_Reset;
        private System.Windows.Forms.OpenFileDialog Ofd_CsfFile;
        private System.Windows.Forms.MenuItem MnMnu_Settings_ShowHistory;
        private CNCGEditors.Windows.Forms.HexNumUpDown NmrcUpDwn_CsfFileVersion;
        private System.Windows.Forms.Label Lbl_CsfFileVersion;
        private System.Windows.Forms.Label Lbl_CsfFileUnused;
        private System.Windows.Forms.Label Lbl_CsfFileLanguage;
        private System.Windows.Forms.ComboBox CmbBox_CsfFileVersion;
        private CNCGEditors.Windows.Forms.HexNumUpDown NmrcUpDwn_CsfFileUnused;
        private System.Windows.Forms.ComboBox CmbBox_CsfFileLanguage;
        private CNCGEditors.Windows.Forms.HexNumUpDown NmrcUpDwn_CsfFileLanguage;
        private System.Windows.Forms.CheckBox ChkBox_CmbBox_CsfFileLanguage_SortByNames;
        private System.Windows.Forms.CheckBox ChkBox_AppendExtraBytes;
        private System.Windows.Forms.CheckBox ChkBox_CsfLabel_OnlyString_IsRtl;
        private System.Windows.Forms.Label Lbl_CsfLabel_Name;
        private System.Windows.Forms.ToolStripStatusLabel StsStrp_Label_CsfLabel_StringsCount;
        private System.Windows.Forms.MenuItem MnMnu_Settings_DeleteCsfLabelConfirmation;
        private System.Windows.Forms.MenuItem MnMnu_Settings_DeleteCsfStringConfirmation;
        private System.Windows.Forms.MenuItem MnMnu_Settings_WordWarp;
    }
}