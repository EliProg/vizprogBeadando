using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolTimetable.Helpers
{
    internal static class UiMessageBox
    {
        public static async Task<Wpf.Ui.Controls.MessageBoxResult> Question(string content,
            string title)
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "Igen",
                CloseButtonText = "Nem",
                WindowStyle = WindowStyle.SingleBorderWindow,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            return await uiMessageBox.ShowDialogAsync();
        }

        public static async Task Show(string content, string title)
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                WindowStyle = WindowStyle.SingleBorderWindow,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            await uiMessageBox.ShowDialogAsync();
        }
    }
}
