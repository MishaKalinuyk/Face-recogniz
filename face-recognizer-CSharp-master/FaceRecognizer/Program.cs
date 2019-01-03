using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognizer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </autumn>
        [STAThread]
        static void Main()
        {
	    Applicetion.Exit();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
