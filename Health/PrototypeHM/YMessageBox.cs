using System.Diagnostics;
using System.Windows.Forms;

namespace PrototypeHM
{
    internal static class YMessageBox
    {
        public static void Information(string message)
        {
            MessageBox.Show(message, @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warning(string message)
        {
            MessageBox.Show(message, @"Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string message)
        {
            Debug.WriteLine(message);
            MessageBox.Show(@"Произошла ошибка", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Dialog(string question)
        {
            DialogResult dialogResult = MessageBox.Show(question, @"Вопрос", MessageBoxButtons.YesNoCancel,
                                                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            return dialogResult;
        }
    }
}