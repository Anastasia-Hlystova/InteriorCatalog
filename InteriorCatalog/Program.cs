using InteriorCatalog;
using System;
using System.Windows.Forms;

namespace InteriorCatalog
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Обработка необработанных исключений
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) =>
                ShowError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                ShowError(e.ExceptionObject as Exception);

            Application.Run(new MainForm());
        }

        private static void ShowError(Exception ex)
        {
            MessageBox.Show($"Произошла непредвиденная ошибка: {ex?.Message}",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}