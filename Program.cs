using System.Diagnostics;

namespace QuickTick
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Process[] processes = Process.GetProcessesByName("Quick Tick");
            if (processes.Length > 1) { return; }
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}