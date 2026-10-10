using System.Windows.Controls; // needs for TextBox

namespace Vehicles.WpfApp.UIHelpers
{
    public static class LogHelper
    {
        public static void AppendLog(TextBox textBox, string message)
        {
            if (textBox != null)
            {
                textBox.AppendText(message + "\n");
                textBox.ScrollToEnd();
            }
        }
    }
}