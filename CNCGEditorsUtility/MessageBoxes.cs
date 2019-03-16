using System.ComponentModel;
using System.Windows.Forms;

namespace CNCGEditors.Windows.Forms {
    internal static class MessageBoxes {
        public static DialogResult ShowMessageBoxError(this IWin32Window owner, string message) {
            return ShowMessageBox(owner, message, MessageBoxIcon.Error);
        }
        public static void ShowMessageBoxInformation(this IWin32Window owner, string message) {
            ShowMessageBox(owner, message, MessageBoxIcon.Information);
        }
        private static DialogResult ShowMessageBox(IWin32Window owner, string message, MessageBoxIcon messageBoxIcon) {
            MessageBoxButtons messageBoxButtons;

            switch (messageBoxIcon) {
                case MessageBoxIcon.Error:
                    messageBoxButtons = MessageBoxButtons.RetryCancel;
                    break;
                case MessageBoxIcon.Question:
                    messageBoxButtons = MessageBoxButtons.YesNo;
                    break;
                case MessageBoxIcon.None:
                case MessageBoxIcon.Warning:
                case MessageBoxIcon.Information:
                    messageBoxButtons = MessageBoxButtons.OK;
                    break;
                default:
                    throw new InvalidEnumArgumentException("messageBoxIcon", (int)messageBoxIcon, messageBoxIcon.GetType());
            }
            return MessageBox.Show(owner, message, string.Empty, messageBoxButtons, messageBoxIcon);
        }
    }
}