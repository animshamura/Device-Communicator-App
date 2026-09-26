using System;
using System.Windows.Forms;

namespace DeviceCommunicatorApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enables visual styles for modern Windows OS controls
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Runs the main message loop and launches Form1
            Application.Run(new Form1());
        }
    }
}