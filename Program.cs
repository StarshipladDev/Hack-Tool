using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bruteforce1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// It sets visual styles from pre-fabs then runs a new instance of  <see cref="Form1"/>
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
