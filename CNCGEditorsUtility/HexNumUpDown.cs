using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CNCGEditors.Windows.Forms {
    internal class HexNumUpDown : NumericUpDown {
        private const string HexNumPrefix = "0x";
        private TextBox m_editBox;

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool MessageBeep(int type);

        public HexNumUpDown() {
            this.Hexadecimal = true;
            this.m_editBox = (TextBox)this.Controls[1];
        }

        protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e) {
            if (this.Hexadecimal) {
                if (e.KeyChar != 'x' || this.m_editBox.SelectionStart != 1 || this.Text[0] != '0' ||
                    (this.m_editBox.SelectionLength + 1 != this.Text.Length && this.Text[this.m_editBox.SelectionLength + 1] == 'x')) {
                    string keyInput;
                    NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;

                    keyInput = e.KeyChar.ToString();
                    if (keyInput == numberFormatInfo.NumberDecimalSeparator || keyInput == numberFormatInfo.NumberGroupSeparator || keyInput == numberFormatInfo.NegativeSign) {
                        this.OnKeyPress(e);
                        e.Handled = true;
                        MessageBeep(0);
                        return;
                    }
                    base.OnTextBoxKeyPress(source, e);
                }
            }
            else {
                base.OnTextBoxKeyPress(source, e);
            }
        }
        protected override void UpdateEditText() {
            if (!this.Hexadecimal || string.IsNullOrEmpty(this.Text)) {
                base.UpdateEditText();
                return;
            }
            this.Text = "0x" + Convert.ToUInt32(this.Value).ToString("X8");
        }
        protected override void ValidateEditText() {
            if (!this.Hexadecimal) {
                base.ValidateEditText();
                return;
            }
            try {
                string text;

                text = this.Text;
                if (!string.IsNullOrEmpty(text)) {
                    decimal value;

                    if (text == HexNumPrefix) {
                        MessageBeep(0);
                        return;
                    }
                    if (text.StartsWith(HexNumPrefix, StringComparison.Ordinal)) {
                        text = text.Substring(HexNumPrefix.Length);
                    }
                    value = Convert.ToDecimal(Convert.ToUInt32(text, 16));
                    value = Math.Max(value, this.Minimum);
                    value = Math.Min(value, this.Maximum);
                    this.Value = value;
                }
            }
            catch {
                // ignored
            }
            this.UserEdit = false;
            this.UpdateEditText();
        }
    }
}