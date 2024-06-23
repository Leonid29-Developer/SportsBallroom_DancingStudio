using SportsBallroom_DancingStudio.Forms;
using System;
using System.Windows.Forms;

namespace SportsBallroom_DancingStudio
{
    internal static class Program
    {
        /// <summary>  Главная точка входа для приложения. </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Authorization());
        }
    }
}