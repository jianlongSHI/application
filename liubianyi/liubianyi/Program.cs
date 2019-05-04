using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using test1;
using liubianyi;
using 自动方式压力平衡条件设定;
namespace liubianyi
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
            Application.Run(new Form4a());
        }
    }
}
