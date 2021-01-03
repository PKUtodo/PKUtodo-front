using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            JObject obj = HTTP.HttpPost(JSONHelper.CreateJson("test", "maruiping@pku.edu.cn"));
            //Console.WriteLine(obj.ToString());
        }
    }
}
