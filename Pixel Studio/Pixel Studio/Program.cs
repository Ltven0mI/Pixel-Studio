using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio
{
    static class Program
    {
        public static PSWindow PSWindow { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ThemeManager.InitThemes();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PSWindow = new PSWindow();
            Application.Run(PSWindow);
        }
    }
}
