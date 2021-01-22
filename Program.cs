using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TODO
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
            Application.Run(new Login());
            //HTTP.HttpPost("type=verify&verify_code=6668&name=xiaotian&email=tianyp@pku.edu.cn&password=123456");
        }
    }
}
