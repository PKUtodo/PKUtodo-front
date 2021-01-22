using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TODO
{
    public class TextBoxMsgChangeEventArg : EventArgs
    {
        public string Text { get; set; }
    }
    public partial class AddList : Form
    {
        public event EventHandler AfterMsgChange;//事件句柄
        public AddList()
        {
            InitializeComponent();
        }
        public AddList(string str1, string str2)
        {
            this.str1 = str1;
            this.str2 = str2;
            InitializeComponent();
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            //点击发送给上层页面
            AfterMsgChange(this, new TextBoxMsgChangeEventArg() { Text = this.textBox1.Text });
            this.Close();
        }
    }
}
