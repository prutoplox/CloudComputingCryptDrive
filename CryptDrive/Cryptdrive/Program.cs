using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Cryptdrive
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
