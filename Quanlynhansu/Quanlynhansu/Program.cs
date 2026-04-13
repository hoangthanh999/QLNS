using System;
using System.Windows.Forms;
using Quanlynhansu.Forms;

namespace Quanlynhansu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi động form đăng nhập
            Application.Run(new frmLogin());
        }
    }
}
