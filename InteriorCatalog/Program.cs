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

            // ��������� �������������� ����������
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) =>
                ShowError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                ShowError(e.ExceptionObject as Exception);

            Application.Run(new MainForm());
        }

        private static void ShowError(Exception ex)
        {
            MessageBox.Show($"��������� �������������� ������: {ex?.Message}",
                "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}