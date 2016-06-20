using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 图书馆管理系统
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f1 = new Form1();
            f1.ShowDialog();
            if (f1.DialogResult == DialogResult.OK)
            {
                Application.Run(new Main(f1.rank,f1.mark1,f1.mark2,f1.name));
            }
            else
            {
                return;
            }
        }
    }
}
