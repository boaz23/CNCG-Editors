using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CNCGEditors.IO;
using CNCGEditors.Properties;
using CNCGEditors.Windows.Forms;
using Utility;

namespace CNCGEditors {
    public partial class CsfEditorForm : Form {
        private const char CsfFilesHistory_Files_Separator = '?';
        private bool csfFile_languageChanging,
                     csfFile_versionChanging;
        private int advancedModeStateHeightDiff;
        private CSFFile CsfFile;
        private FileStream Fs_CsfFile;

        public CsfEditorForm() {
            this.SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, true);
            this.InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            this.SortCmbBox_CsfFileLanguage_Items(true);
            this.CmbBox_CsfFileVersion.Items.Add(string.Empty);
            foreach (CSFFile.Versions version in EnumsHelper<CSFFile.Versions>.Values) {
                this.CmbBox_CsfFileVersion.Items.Add(version);
            }
        }

        private static string GetCsfFileLanguageText(CSFFile.Languages language) {
            string value;

            switch (language) {
                case CSFFile.Languages.EnglishUS:
                    value = "English US";
                    break;
                case CSFFile.Languages.EnglishUK:
                    value = "English UK";
                    break;
                default:
                    value = language.ToString();
                    break;
            }
            return value;
        }
        private void AddCsfFileToHistory(string csfFileName) {
            if (File.Exists(csfFileName)) {
                bool exists;

                exists = false;
                for (int i = this.MnMnu_File_IO_History_Separator.Index + 1; i < this.MnMnu_File_ClearHistory.Index; i++) {
                    if (this.MnMnu_File.MenuItems[i].Text == csfFileName) {
                        exists = true;
                        break;
                    }
                }
                if (!exists) {
                    int csfFilesHistoryCount;
                    string csfFilesHistory;
                    MenuItem csfFileMenuItem;

                    csfFilesHistoryCount = this.CsfFileHistoryCount;
                    csfFilesHistory = Settings.Default.CsfFilesHistory;
                    if (csfFilesHistory.IndexOf(csfFileName, StringComparison.Ordinal) == -1) {
                        if (csfFilesHistoryCount > 0) {
                            csfFilesHistory += CsfFilesHistory_Files_Separator;
                        }
                        csfFilesHistory += csfFileName;
                        Settings.Default.CsfFilesHistory = csfFilesHistory;
                        Settings.Default.Save();
                    }
                    csfFileMenuItem = new MenuItem(csfFileName);
                    csfFileMenuItem.MenuItems.AddRange(new[] {
                        new MenuItem("Open as read-write", (sender, args) => this.OpenCsfFile(FormUtilities.GetSenderMenuItem_ParentMenuItem_Text(sender), false)),
                        new MenuItem("Open as read-only", (sender, args) => this.OpenCsfFile(FormUtilities.GetSenderMenuItem_ParentMenuItem_Text(sender), true)),
                        new MenuItem("Remove from history",
                                     delegate(object sender, EventArgs args) {
                                         int csfFilesHistory_csfFileName_index;
                                         string _csfFileName,
                                                _csfFilesHistory;

                                         _csfFileName = FormUtilities.GetSenderMenuItem_ParentMenuItem_Text(sender);
                                         _csfFilesHistory = Settings.Default.CsfFilesHistory;
                                         csfFilesHistory_csfFileName_index = _csfFilesHistory.IndexOf(_csfFileName, StringComparison.Ordinal);
                                         if (csfFilesHistory_csfFileName_index == -1) {
                                             return;
                                         }
                                         for (int i = this.MnMnu_File_IO_History_Separator.Index + 1; i < this.MnMnu_File_ClearHistory.Index; i++) {
                                             if (this.MnMnu_File.MenuItems[i].Text != _csfFileName) {
                                                 continue;
                                             }
                                             this.MnMnu_File.MenuItems.RemoveAt(i);
                                             break;
                                         }
                                         this.MnMnu_File_ClearHistory.Enabled = this.CsfFileHistoryCount > 0;
                                         _csfFilesHistory = _csfFilesHistory.Remove(csfFilesHistory_csfFileName_index, _csfFileName.Length);
                                         if (_csfFilesHistory.Length > 0 && _csfFilesHistory[csfFilesHistory_csfFileName_index] == CsfFilesHistory_Files_Separator) {
                                             _csfFilesHistory = _csfFilesHistory.Remove(csfFilesHistory_csfFileName_index, 1);
                                         }
                                         Settings.Default.CsfFilesHistory = _csfFilesHistory;
                                         Settings.Default.Save();
                                     })
                    });
                    this.MnMnu_File.MenuItems.Add(this.CsfFileHistoryLastIndex, csfFileMenuItem);
                    csfFileMenuItem.Enabled = this.Fs_CsfFile == null || this.Fs_CsfFile.Name != csfFileMenuItem.Text;
                    csfFileMenuItem.Visible = this.MnMnu_Settings_ShowHistory.Checked;
                    this.MnMnu_File_ClearHistory.Enabled = (csfFilesHistoryCount + 1) > 0;
                }
            }
        }
        private string GetMessageBoxErrorMessage(string message, Exception exception) {
            if (message == null) {
                message = string.Empty;
            }
            if (this.AdvancedMode) {
                if (message == string.Empty) {
                    message += Exceptions.NewLines;
                }
                message += Exceptions.ExceptionToStringFull(exception);
            }
            return message;
        }
        private void OpenCsfFile(string fileName, bool readOnly) {
            if (this.Fs_CsfFile != null && this.Fs_CsfFile.Name == fileName) {
                this.ShowMessageBoxInformation("The file is already open.");
                return;
            }
            if (!this.OpenFileStream(fileName, FileMode.Open, readOnly ? FileAccess.Read : FileAccess.ReadWrite)) {
                return;
            }
            try {
                this.SetIoStateTrueAndInitiateCsfFile(readOnly ? new ReadonlyCSFFile(this.Fs_CsfFile) : new CSFFile(this.Fs_CsfFile));
            }
            catch (Exception exception) {
                this.Fs_CsfFile.Close();
                this.Fs_CsfFile = null;
                this.CsfFile = null;
                MessageBox.Show(this, this.GetMessageBoxErrorMessage("Bad file. Invalid CSF file format.", exception), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool OpenFileStream(string fileName, FileMode fileMode, FileAccess fileAccess) {
            return this.TryActionShowMessageBoxErrorIfFail(string.Format("Failed to open the file '{0}'.", fileName),
                                                           delegate {
                                                               this.Fs_CsfFile = File.Open(fileName, fileMode, fileAccess, FileShare.Read);
                                                               this.AddCsfFileToHistory(fileName);
                                                               this.SetCsfFilesHistoryEnabledState(fileName);
                                                           });
        }
        private bool OpenSaveFileStream() {
            return this.ShowFileDialog(this.Sfd_CsfFile) && this.OpenFileStream(this.Sfd_CsfFile.FileName, FileMode.Create, FileAccess.ReadWrite);
        }
        private void SaveToFileStream() {
            if (this.CsfFile == null) {
                return;
            }
            this.TryActionShowMessageBoxErrorIfFail(string.Format("Failed to write to the file stream of the file '{0}'.", this.Fs_CsfFile.Name),
                                                    delegate {
                                                        bool appendExtraBytes;
                                                        byte[] extraBytes;

                                                        appendExtraBytes = !this.AdvancedMode || this.AppendExtraBytes;
                                                        extraBytes = appendExtraBytes ? this.Fs_CsfFile.Read(checked((int)(this.Fs_CsfFile.Length - this.Fs_CsfFile.Position))) : null;
                                                        this.Fs_CsfFile.Position = 0;
                                                        this.CsfFile.WriteToStream(this.Fs_CsfFile);
                                                        if (appendExtraBytes) {
                                                            this.Fs_CsfFile.Write(extraBytes);
                                                        }
                                                    });
        }
        private void SetAdvancedModeState(bool advancedMode, bool resize) {
            this.SortLstBox_CsfLabels_Items(!advancedMode || this.LstBox_CsfLabels_SortBy_CsfLabelName);
            this.MnMnu_Edit_CsfStringAdd.Visible = advancedMode;
            this.MnMnu_Edit_CsfStringInsertAt.Visible = advancedMode;
            this.MnMnu_Edit_CsfStringRemoveAt.Visible = advancedMode;
            this.MnMnu_Edit_CsfString_Separator.Visible = advancedMode;
            this.MnMnu_Help_CsfFileFormat.Visible = advancedMode;
            this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy.Visible = advancedMode;
            this.ChkBox_AppendExtraBytes.Visible = advancedMode;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Visible = advancedMode;
            this.CmbBox_CsfFileLanguage.Visible = advancedMode;
            this.CmbBox_CsfFileVersion.Visible = advancedMode;
            this.Lbl_CsfFileLanguage.Visible = advancedMode;
            this.Lbl_CsfFileUnused.Visible = advancedMode;
            this.Lbl_CsfFileVersion.Visible = advancedMode;
            this.NmrcUpDwn_CsfFileLanguage.Visible = advancedMode;
            this.NmrcUpDwn_CsfFileUnused.Visible = advancedMode;
            this.NmrcUpDwn_CsfFileVersion.Visible = advancedMode;
            this.StsStrp_Label_CsfFile_StringsCount.Visible = advancedMode;
            this.StsStrp_Label_CsfLabel_StringsCount.Visible = advancedMode;
            if (advancedMode) {
                int height;

                height = this.Height;
                this.MinimumSize = new Size(584, 438);
                if (resize) {
                    int _advancedModeStateHeightDiff;

                    _advancedModeStateHeightDiff = this.advancedModeStateHeightDiff;
                    this.SpltCntnr_CsfLabelNameData.Top += _advancedModeStateHeightDiff;
                    if (height > this.MinimumSize.Height) {
                        this.Height += _advancedModeStateHeightDiff;
                    }
                    this.SpltCntnr_CsfLabelNameData.Height -= _advancedModeStateHeightDiff;
                }
            }
            else {
                this.MinimumSize = new Size(480, 311);
                if (resize) {
                    const int MnMnu_Margin_Bottom = 9;

                    int _advancedModeStateHeightDiff,
                        spltCntnr_CsfLabelNameData_top;

                    _advancedModeStateHeightDiff = -1;
                    spltCntnr_CsfLabelNameData_top = this.SpltCntnr_CsfLabelNameData.Top - this.SpltCntnr_CsfLabelNameData.Margin.Top;
                    foreach (Control control in this.Controls) {
                        int __advancedModeStateHeightDiff;

                        __advancedModeStateHeightDiff = control.Top + control.Height + control.Margin.Bottom - MnMnu_Margin_Bottom;
                        if (__advancedModeStateHeightDiff > _advancedModeStateHeightDiff && __advancedModeStateHeightDiff <= spltCntnr_CsfLabelNameData_top) {
                            _advancedModeStateHeightDiff = __advancedModeStateHeightDiff;
                        }
                    }
                    this.advancedModeStateHeightDiff = _advancedModeStateHeightDiff;
                    this.SpltCntnr_CsfLabelNameData.Top -= _advancedModeStateHeightDiff;
                    this.Height -= _advancedModeStateHeightDiff;
                    this.SpltCntnr_CsfLabelNameData.Height += _advancedModeStateHeightDiff;
                }
            }
        }
        private bool SetCmbBox_CsfFileLanguages_SelectedItem() {
            CSFFile.Languages language;

            language = this.CsfFileLanguage;
            if (language < CSFFile.Languages.EnglishUS || language > CSFFile.Languages.Chinese) {
                return false;
            }
            this.CmbBox_CsfFileLanguage.SelectedItem = language;
            return true;
        }
        private bool SetCmbBox_CsfFileVersions_SelectedItem() {
            CSFFile.Versions version;

            version = this.CsfFileVersion;
            if (version < CSFFile.Versions.Nox || version > CSFFile.Versions.RA2YRGeneralsZHBFME) {
                return false;
            }
            this.CmbBox_CsfFileVersion.SelectedItem = version;
            return true;
        }
        private void SetCsfLabelsSortOrder(bool sortByIndex) {
            this.SortLstBox_CsfLabels_Items(sortByIndex);
            this.SortByCsfLabeIndex = sortByIndex;
            this.SortByCsfLabelName = !sortByIndex;
            this.SetResetSettingsMenuItemEnabledState();
            Settings.Default.CsfLabels_SortBy_Index = sortByIndex;
            Settings.Default.CsfLabels_SortBy_Name = !sortByIndex;
            Settings.Default.Save();
        }
        private void SetCsfFilesHistoryEnabledState(string fileName) {
            for (int i = this.MnMnu_File_IO_History_Separator.Index + 1; i < this.MnMnu_File_ClearHistory.Index; i++) {
                this.MnMnu_File.MenuItems[i].Enabled = this.MnMnu_File.MenuItems[i].Text != fileName;
            }
        }
        private void SetIoState(bool ioState, bool readOnly) {
            this.ChkBox_AppendExtraBytes.Enabled = ioState && !readOnly;
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Enabled = ioState;
            this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Enabled = ioState && !readOnly;
            this.CmbBox_CsfFileLanguage.Enabled = ioState && !readOnly;
            this.CmbBox_CsfFileVersion.Enabled = ioState && !readOnly;
            this.Lbl_CsfFileLanguage.Enabled = ioState && !readOnly;
            this.Lbl_CsfFileUnused.Enabled = ioState && !readOnly;
            this.Lbl_CsfFileVersion.Enabled = ioState && !readOnly;
            this.Lbl_CsfLabel_Name.Enabled = ioState;
            this.Lbl_CsfLabel_OnlyString_Value.Enabled = ioState;
            this.MnMnu_File_Close.Enabled = ioState;
            this.MnMnu_Edit.Enabled = ioState;
            this.MnMnu_Edit_CsfLabelAdd.Enabled = ioState && !readOnly;
            this.MnMnu_Edit_CsfLabelInsertAt.Enabled = ioState && !readOnly;
            this.MnMnu_Edit_CsfLabelRemove.Enabled = ioState && !readOnly;
            this.MnMnu_Edit_CsfLabelRemoveAt.Enabled = ioState && !readOnly;
            this.MnMnu_Edit_CsfStringAdd.Enabled = ioState && !readOnly;
            this.MnMnu_Edit_CsfStringInsertAt.Enabled = ioState && !readOnly;
            this.MnMnu_Edit_CsfStringRemoveAt.Enabled = ioState && !readOnly;
            this.NmrcUpDwn_CsfFileLanguage.Enabled = ioState && !readOnly;
            this.NmrcUpDwn_CsfFileUnused.Enabled = ioState && !readOnly;
            this.NmrcUpDwn_CsfFileVersion.Enabled = ioState && !readOnly;
            this.TxtBox_CsfLabel_Name.Enabled = ioState;
            this.TxtBox_CsfLabel_OnlyString_Value.Enabled = ioState;
        }
        private void SetIoStateTrueAndInitiateCsfFile(CSFFile csfFile) {
            this.SetIoState(true, csfFile.IsReadOnly);
            this.CsfFile = csfFile;
            this.StsStrp_Label_CsfFile_StringsCount.Text = Resources.StringsCount + csfFile.Labels.Count;
            {
                int csfFile_stringsCount;
                List<CSFLabel> csfFile_labels;

                csfFile_stringsCount = 0;
                csfFile_labels = new List<CSFLabel>(csfFile.Labels.Count);
                this.LstBox_CsfLabels.BeginUpdate();
                foreach (CSFLabel csfLabel in csfFile.Labels) {
                    if (this.LstBox_CsfLabels_SortByCsfLabelName) {
                        this.LstBox_CsfLabels.Items.Add(csfLabel);
                    }
                    else {
                        csfFile_labels.Add(csfLabel);
                    }
                    csfFile_stringsCount += csfLabel.Strings.Count;
                }
                if (this.LstBox_CsfLabels_SortByCsfLabelName) {
                    csfFile_labels.Sort((csfLabel1, csfLabel2) => string.Compare(csfLabel1.Name, csfLabel2.Name, StringComparison.Ordinal));
                    foreach (CSFLabel csfLabel in csfFile_labels) {
                        this.LstBox_CsfLabels.Items.Add(csfLabel);
                    }
                }
                this.LstBox_CsfLabels.EndUpdate();
                this.StsStrp_Label_CsfFile_LabelsCount.Text = Resources.LabelsCount + csfFile.Labels.Count;
                this.StsStrp_Label_CsfFile_StringsCount.Text = Resources.StringsCount + csfFile_stringsCount;
            }
        }
        private void SetResetSettingsMenuItemEnabledState() {
            this.MnMnu_Settings_Reset.Enabled = !this.AdvancedMode || !this.DeleteCsfLabelConfirmation || !this.DeleteCsfStringConfirmation || this.WordWarp || !this.SortByCsfLabelName ||
                                                this.SortByCsfLabeIndex || !this.ShowHistory || this.CsfFileLanguageUInt != 0x0 || this.CsfFileUnusedUInt != 0x0 ||
                                                this.CsfFileVersionUInt != 0x3 || !this.AppendExtraBytes || !this.CmbBox_CsfFileLanguage_SortByNames;
        }
        private void SetShowHistoryState(bool showHistory) {
            for (int i = this.MnMnu_File_IO_History_Separator.Index; i <= this.MnMnu_File_ClearHistory.Index; i++) {
                this.MnMnu_File.MenuItems[i].Visible = showHistory;
            }
        }
        private bool ShowFileDialog(FileDialog fileDialog) {
            return fileDialog.ShowDialog(this) == DialogResult.OK;
        }
        private void SortLstBox_CsfLabels_Items(bool byName) {
            if (this.CsfFile == null) {
                return;
            }
            this.LstBox_CsfLabels.BeginUpdate();
            if (byName) {
                this.LstBox_CsfLabels.Sorted = true;
            }
            else {
                this.LstBox_CsfLabels.Sorted = false;
                this.LstBox_CsfLabels.Items.Clear();
                foreach (CSFLabel csfLabel in this.CsfFile.Labels) {
                    this.LstBox_CsfLabels.Items.Add(csfLabel);
                }
            }
            this.LstBox_CsfLabels.EndUpdate();
        }
        private void SortCmbBox_CsfFileLanguage_Items(bool byText) {
            KeyValuePair<CSFFile.Languages, string>[] languagesText;

            languagesText = new KeyValuePair<CSFFile.Languages, string>[EnumsHelper<CSFFile.Languages>.Values.Length];
            for (int i = 0; i < languagesText.Length; i++) {
                languagesText[i] = new KeyValuePair<CSFFile.Languages, string>(EnumsHelper<CSFFile.Languages>.Values[i], GetCsfFileLanguageText(EnumsHelper<CSFFile.Languages>.Values[i]));
            }
            this.CmbBox_CsfFileLanguage.Items.Clear();
            this.CmbBox_CsfFileLanguage.Items.Add(string.Empty);
            if (byText) {
                Array.Sort(languagesText, (languageText1, languageText2) => string.CompareOrdinal(languageText1.Value, languageText2.Value));
            }
            else {
                Array.Sort(languagesText, (languageText1, languageText2) => languageText1.Key.CompareTo(languageText2.Key));
            }
            foreach (KeyValuePair<CSFFile.Languages, string> languageText in languagesText) {
                this.CmbBox_CsfFileLanguage.Items.Add(languageText.Key);
            }
            this.SetCmbBox_CsfFileLanguages_SelectedItem();
        }
        private bool TryActionShowMessageBoxErrorIfFail(string message, Action action) {
            bool retry;

            do {
                try {
                    action();
                    return true;
                }
                catch (Exception exception) {
                    retry = this.ShowMessageBoxError(this.GetMessageBoxErrorMessage(message, exception)) == DialogResult.Retry;
                }
            } while (retry);
            return false;
        }

        private void ChkBox_AppendExtraBytes_CheckedChanged(object sender, EventArgs e) {
            this.SetResetSettingsMenuItemEnabledState();
            Settings.Default.CsfFileAppendExtraBytes = this.AppendExtraBytes;
            Settings.Default.Save();
        }
        private void ChkBox_CmbBox_CsfFileLanguage_SortByNames_CheckedChanged(object sender, EventArgs e) {
            bool cmbBox_CsfFileLanguage_SortByNames;

            cmbBox_CsfFileLanguage_SortByNames = this.CmbBox_CsfFileLanguage_SortByNames;
            this.SortCmbBox_CsfFileLanguage_Items(cmbBox_CsfFileLanguage_SortByNames);
            this.SetResetSettingsMenuItemEnabledState();
            Settings.Default.CsfFileLanguagesSortByName = cmbBox_CsfFileLanguage_SortByNames;
            Settings.Default.Save();
        }
        private void CmbBox_CsfFileLanguage_Format(object sender, ListControlConvertEventArgs e) {
            FormUtilities.SetListControlConvertEventArgsValue<CSFFile.Languages>(e, GetCsfFileLanguageText);
        }
        private void CmbBox_CsfFileLanguage_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.csfFile_languageChanging) {
                return;
            }
            this.csfFile_languageChanging = true;
            if (this.CmbBox_CsfFileLanguage.SelectedItem is CSFFile.Languages) {
                this.CsfFileLanguage = (CSFFile.Languages)this.CmbBox_CsfFileLanguage.SelectedItem;
            }
            else {
                this.SetCmbBox_CsfFileLanguages_SelectedItem();
            }
            this.csfFile_languageChanging = false;
        }
        private void CmbBox_CsfFileVersion_Format(object sender, ListControlConvertEventArgs e) {
            FormUtilities.SetListControlConvertEventArgsValue<CSFFile.Versions>(e,
                                                                                version =>
                                                                                version == CSFFile.Versions.RA2YRGeneralsZHBFME ? "RA2 | YR | Generals | ZH | BFME" : version.ToString());
        }
        private void CmbBox_CsfFileVersion_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.csfFile_versionChanging) {
                return;
            }
            this.csfFile_versionChanging = true;
            if (this.CmbBox_CsfFileVersion.SelectedItem is CSFFile.Versions) {
                this.CsfFileVersion = (CSFFile.Versions)this.CmbBox_CsfFileVersion.SelectedItem;
            }
            else {
                this.SetCmbBox_CsfFileVersions_SelectedItem();
            }
            this.csfFile_versionChanging = false;
        }
        private void CsFEditorForm_Load(object sender, EventArgs e) {
            this.SetAdvancedModeState(this.AdvancedMode, !this.AdvancedMode);
            this.LstBox_CsfLabels.Sorted = this.LstBox_CsfLabels_SortByCsfLabelName;
            {
                string[] csfFileNames;

                csfFileNames = Settings.Default.CsfFilesHistory.Split(CsfFilesHistory_Files_Separator);
                foreach (string csfFileName in csfFileNames) {
                    this.AddCsfFileToHistory(csfFileName);
                }
            }
            this.CmbBox_CsfFileVersion.SelectedIndex = 2;
            this.SetShowHistoryState(this.ShowHistory);
            this.CsfFileLanguageUInt = Settings.Default.CsfFileLanguage;
            this.CsfFileUnusedUInt = Settings.Default.CsfFileUnused;
            this.CsfFileVersionUInt = Settings.Default.CsfFileVersion;
            this.AppendExtraBytes = Settings.Default.CsfFileAppendExtraBytes;
            this.CmbBox_CsfFileLanguage_SortByNames = Settings.Default.CsfFileLanguagesSortByName;
            this.SetResetSettingsMenuItemEnabledState();
        }
        private void LstBox_CsfLabels_SelectedIndexChanged(object sender, EventArgs e) {
            bool isCsfLabelNull;
            CSFLabel csfLabel;

            csfLabel = this.LstBox_CsfLabels_SelectedCsfLabel;
            isCsfLabelNull = csfLabel == null;
            this.ChkBox_CsfLabel_OnlyString_IsRtl.Enabled = isCsfLabelNull;
            this.Lbl_CsfLabel_Name.Enabled = isCsfLabelNull;
            this.Lbl_CsfLabel_OnlyString_Value.Enabled = isCsfLabelNull;
            this.TxtBox_CsfLabel_Name.Enabled = isCsfLabelNull;
            this.TxtBox_CsfLabel_OnlyString_Value.Enabled = isCsfLabelNull;
            if (csfLabel == null) {
                return;
            }
            this.TxtBox_CsfLabel_Name.ReadOnly = csfLabel.IsReadOnly;
            this.TxtBox_CsfLabel_OnlyString_Value.ReadOnly = csfLabel.IsReadOnly;
            this.TxtBox_CsfLabel_Name.Text = csfLabel.Name;
            if (this.AdvancedMode) {
            }
            else {
                this.TxtBox_CsfLabel_OnlyString_Value.Text = csfLabel.Strings.Count > 0 ? csfLabel.Strings[0].Value : string.Empty;
            }
            throw new NotImplementedException();
        }
        private void LstBox_CsfLabels_Format(object sender, ListControlConvertEventArgs e) {
            e.Value = ((CSFLabel)e.ListItem).Name;
        }
        private void MnMnu_Edit_CsfLabelAdd_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }
        private void MnMnu_File_ClearHistory_Click(object sender, EventArgs e) {
            for (int i = this.MnMnu_File_IO_History_Separator.Index + 1; i < this.MnMnu_File_ClearHistory.Index; i++) {
                this.MnMnu_File.MenuItems.RemoveAt(i);
            }
            this.MnMnu_File_ClearHistory.Enabled = false;
            Settings.Default.CsfFilesHistory = string.Empty;
            Settings.Default.Save();
        }
        private void MnMnu_File_Close_Click(object sender, EventArgs e) {
            if (!this.TryActionShowMessageBoxErrorIfFail(this.Fs_CsfFile == null ? null : string.Format("Failed to close the file stream of the file '{0}'.", this.Fs_CsfFile.Name),
                                                         delegate {
                                                             if (this.Fs_CsfFile != null) {
                                                                 this.Fs_CsfFile.Close();
                                                                 this.Fs_CsfFile = null;
                                                             }
                                                             this.CsfFile = null;
                                                         })) {
                return;
            }
            this.SetCsfFilesHistoryEnabledState(null);
            this.SetIoState(false, true);
            this.StsStrp_Label_CsfFile_StringsCount.Text = Resources.StringsCount;
            this.StsStrp_Label_CsfFile_LabelsCount.Text = Resources.LabelsCount;
            this.MnMnu_File_Save.Enabled = false;
            this.MnMnu_File_SaveAs.Enabled = false;
            this.LstBox_CsfLabels.Items.Clear();
            this.CsfFile = null;
        }
        private void MnMnu_File_Exit_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        private void MnMnu_File_New_Click(object sender, EventArgs e) {
            this.MinimumSize = Size.Empty;
            FormUtilities.GetControlMinimumClientSize(this);
            this.SetIoStateTrueAndInitiateCsfFile(new CSFFile(CSFFile.Types.CSF, this.CsfFileVersion, this.CsfFileUnusedUInt, this.CsfFileLanguage, new List<CSFLabel>()));
        }
        private void MnMnu_File_Open_Click(object sender, EventArgs e) {
            if (!this.ShowFileDialog(this.Ofd_CsfFile)) {
                return;
            }
            this.OpenCsfFile(this.Ofd_CsfFile.FileName, this.Ofd_CsfFile.ReadOnlyChecked);
        }
        private void MnMnu_File_Save_Click(object sender, EventArgs e) {
            if (this.CsfFile == null || (this.Fs_CsfFile == null && !this.OpenSaveFileStream())) {
                return;
            }
            this.SaveToFileStream();
        }
        private void MnMnu_File_SaveAs_Click(object sender, EventArgs e) {
            if (this.CsfFile == null || !this.OpenSaveFileStream()) {
                return;
            }
            this.SaveToFileStream();
        }
        private void MnMnu_Help_CsfFileFormat_Click(object sender, EventArgs e) {
            this.TryActionShowMessageBoxErrorIfFail("Failed to open a new browser tab or window.", () => Process.Start("http://modenc.renegadeprojects.com/CSF_File_Format"));
        }
        private void MnMnu_Help_WhatIsAdvancedMode_Click(object sender, EventArgs e) {
            this.ShowMessageBoxInformation(
                                           "Advanced mode allows you to see the CSF file format from the 'Help' menu, specify the CSF file's version, language and the unused value, specify how you want the labels to be sorted in the list box, change the appearance order of a label in a CSF file, see how much strings a CSF file has, edit the amount of strings in a label, as well as specifying their type and extra value and get detailed error messages.");
        }
        private void MnMnu_Settings_AdvancedMode_Click(object sender, EventArgs e) {
            bool advancedMode;

            advancedMode = FormUtilities.ChangeMenuItemCheckState(sender);
            this.SetAdvancedModeState(advancedMode, true);
            this.SetResetSettingsMenuItemEnabledState();
            Settings.Default.AdvancedMode = advancedMode;
            Settings.Default.Save();
        }
        private void MnMnu_Settings_DeleteLabelConfirmation_Click(object sender, EventArgs e) {
            Settings.Default.DeleteCsfLabelsConfirmation = FormUtilities.ChangeMenuItemCheckState(sender);
            Settings.Default.Save();
            this.SetResetSettingsMenuItemEnabledState();
        }
        private void MnMnu_Settings_DeleteCsfStringConfirmation_Click(object sender, EventArgs e) {
            Settings.Default.DeleteCsfStringsConfirmation = FormUtilities.ChangeMenuItemCheckState(sender);
            Settings.Default.Save();
            this.SetResetSettingsMenuItemEnabledState();
        }
        private void MnMnu_Settings_Reset_Click(object sender, EventArgs e) {
            {
                string csfFilesHistory;

                csfFilesHistory = Settings.Default.CsfFilesHistory;
                Settings.Default.Reset();
                Settings.Default.CsfFilesHistory = csfFilesHistory;
            }
            Settings.Default.Save();
            {
                bool advancedMode;

                advancedMode = Settings.Default.AdvancedMode;
                this.SetAdvancedModeState(advancedMode, this.AdvancedMode != advancedMode);
                this.AdvancedMode = advancedMode;
            }
            this.DeleteCsfLabelConfirmation = Settings.Default.DeleteCsfLabelsConfirmation;
            this.DeleteCsfStringConfirmation = Settings.Default.DeleteCsfStringsConfirmation;
            this.ShowHistory = Settings.Default.ShowHistory;
            this.SortByCsfLabeIndex = Settings.Default.CsfLabels_SortBy_Index;
            this.SortByCsfLabelName = Settings.Default.CsfLabels_SortBy_Name;
            this.WordWarp = Settings.Default.WordWarp;
            this.SortLstBox_CsfLabels_Items(this.LstBox_CsfLabels_SortByCsfLabelName);
            this.SetShowHistoryState(this.ShowHistory);
            this.CsfFileLanguageUInt = Settings.Default.CsfFileLanguage;
            this.CsfFileUnusedUInt = Settings.Default.CsfFileUnused;
            this.CsfFileVersionUInt = Settings.Default.CsfFileVersion;
            this.AppendExtraBytes = Settings.Default.CsfFileAppendExtraBytes;
            this.CmbBox_CsfFileLanguage_SortByNames = Settings.Default.CsfFileLanguagesSortByName;
            this.MnMnu_Settings_Reset.Enabled = false;
        }
        private void MnMnu_Settings_ShowHistory_Click(object sender, EventArgs e) {
            bool showHistory;

            showHistory = FormUtilities.ChangeMenuItemCheckState(sender);
            Settings.Default.ShowHistory = showHistory;
            Settings.Default.Save();
            this.SetShowHistoryState(showHistory);
            this.SetResetSettingsMenuItemEnabledState();
        }
        private void MnMnu_Settings_SortListBoxCsfFileLabelsNamesBy_CsfFileLabel_Index_Click(object sender, EventArgs e) {
            this.SetCsfLabelsSortOrder(true);
        }
        private void MnMnu_Settings_SortListBoxCsfFileLabelsNamesBy_CsfFileLabel_Name_Click(object sender, EventArgs e) {
            this.SetCsfLabelsSortOrder(false);
        }
        private void MnMnu_Settings_WordWarp_Click(object sender, EventArgs e) {
            Settings.Default.WordWarp = FormUtilities.ChangeMenuItemCheckState(sender);
            Settings.Default.Save();
            this.SetResetSettingsMenuItemEnabledState();
        }
        private void NmrcUpDwn_CsfFileLanguage_ValueChanged(object sender, EventArgs e) {
            if (this.csfFile_languageChanging) {
                uint value;

                value = this.CsfFileLanguageUInt;
                if (this.CsfFile != null) {
                    this.CsfFile.Language = (CSFFile.Languages)value;
                }
                this.SetResetSettingsMenuItemEnabledState();
                Settings.Default.CsfFileLanguage = value;
                Settings.Default.Save();
                return;
            }
            this.csfFile_languageChanging = true;
            if (!this.SetCmbBox_CsfFileLanguages_SelectedItem()) {
                this.CmbBox_CsfFileLanguage.SelectedIndex = 0;
            }
            this.NmrcUpDwn_CsfFileLanguage_ValueChanged(sender, e);
            this.csfFile_languageChanging = false;
        }
        private void NmrcUpDwn_CsfFileUnused_ValueChanged(object sender, EventArgs e) {
            uint value;

            value = this.CsfFileUnusedUInt;
            if (this.CsfFile != null) {
                this.CsfFile.Unused = value;
            }
            this.SetResetSettingsMenuItemEnabledState();
            Settings.Default.CsfFileUnused = value;
            Settings.Default.Save();
        }
        private void NmrcUpDwn_CsfFileVersion_ValueChanged(object sender, EventArgs e) {
            if (this.csfFile_versionChanging) {
                uint value;

                value = this.CsfFileVersionUInt;
                if (this.CsfFile != null) {
                    this.CsfFile.Version = (CSFFile.Versions)value;
                }
                this.SetResetSettingsMenuItemEnabledState();
                Settings.Default.CsfFileVersion = value;
                Settings.Default.Save();
                return;
            }
            this.csfFile_versionChanging = true;
            if (!this.SetCmbBox_CsfFileVersions_SelectedItem()) {
                this.CmbBox_CsfFileVersion.SelectedIndex = 0;
            }
            this.NmrcUpDwn_CsfFileVersion_ValueChanged(sender, e);
            this.csfFile_versionChanging = false;
        }

        private bool AdvancedMode {
            get {
                return this.MnMnu_Settings_AdvancedMode.Checked;
            }
            set {
                this.MnMnu_Settings_AdvancedMode.Checked = value;
            }
        }
        private bool AppendExtraBytes {
            get {
                return this.ChkBox_AppendExtraBytes.Checked;
            }
            set {
                this.ChkBox_AppendExtraBytes.Checked = value;
            }
        }
        private bool CmbBox_CsfFileLanguage_SortByNames {
            get {
                return this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Checked;
            }
            set {
                this.ChkBox_CmbBox_CsfFileLanguage_SortByNames.Checked = value;
            }
        }
        private int CsfFileHistoryCount {
            get {
                return this.CsfFileHistoryLastIndex - this.MnMnu_File_IO_History_Separator.Index - 1;
            }
        }
        private int CsfFileHistoryLastIndex {
            get {
                return this.MnMnu_File_ClearHistory.Index;
            }
        }
        private CSFFile.Languages CsfFileLanguage {
            get {
                return (CSFFile.Languages)this.CsfFileLanguageUInt;
            }
            set {
                this.CsfFileLanguageUInt = (uint)value;
            }
        }
        private uint CsfFileLanguageUInt {
            get {
                return Numbers.ConvertDecimalToUInt32(this.NmrcUpDwn_CsfFileLanguage.Value);
            }
            set {
                this.NmrcUpDwn_CsfFileLanguage.Value = value;
            }
        }
        private uint CsfFileUnusedUInt {
            get {
                return Numbers.ConvertDecimalToUInt32(this.NmrcUpDwn_CsfFileUnused.Value);
            }
            set {
                this.NmrcUpDwn_CsfFileUnused.Value = value;
            }
        }
        private CSFFile.Versions CsfFileVersion {
            get {
                return (CSFFile.Versions)this.CsfFileVersionUInt;
            }
            set {
                this.CsfFileVersionUInt = (uint)value;
            }
        }
        private uint CsfFileVersionUInt {
            get {
                return Numbers.ConvertDecimalToUInt32(this.NmrcUpDwn_CsfFileVersion.Value);
            }
            set {
                this.NmrcUpDwn_CsfFileVersion.Value = value;
            }
        }
        private bool DeleteCsfLabelConfirmation {
            get {
                return this.MnMnu_Settings_DeleteCsfLabelConfirmation.Checked;
            }
            set {
                this.MnMnu_Settings_DeleteCsfLabelConfirmation.Checked = value;
            }
        }
        private bool DeleteCsfStringConfirmation {
            get {
                return this.MnMnu_Settings_DeleteCsfStringConfirmation.Checked;
            }
            set {
                this.MnMnu_Settings_DeleteCsfStringConfirmation.Checked = value;
            }
        }
        private CSFLabel LstBox_CsfLabels_SelectedCsfLabel {
            get {
                return (CSFLabel)this.LstBox_CsfLabels.SelectedItem;
            }
        }
        private bool LstBox_CsfLabels_SortBy_CsfLabelName {
            get {
                return this.SortByCsfLabelName && !this.SortByCsfLabeIndex;
            }
        }
        private bool LstBox_CsfLabels_SortByCsfLabelName {
            get {
                return !this.AdvancedMode || this.LstBox_CsfLabels_SortBy_CsfLabelName;
            }
        }
        private bool ShowHistory {
            get {
                return this.MnMnu_Settings_ShowHistory.Checked;
            }
            set {
                this.MnMnu_Settings_ShowHistory.Checked = value;
            }
        }
        private bool SortByCsfLabeIndex {
            get {
                return this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index.Checked;
            }
            set {
                this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Index.Checked = value;
            }
        }
        private bool SortByCsfLabelName {
            get {
                return this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name.Checked;
            }
            set {
                this.MnMnu_Settings_SortListBoxCsfLabelsNamesBy_Name.Checked = value;
            }
        }
        private bool WordWarp {
            get {
                return this.MnMnu_Settings_WordWarp.Checked;
            }
            set {
                this.MnMnu_Settings_WordWarp.Checked = value;
            }
        }
    }
}