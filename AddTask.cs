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
    public partial class AddTask : Form
    {
        public event EventHandler AfterMsgChange;//事件句柄
        public AddTask()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //点击提交
            //点击发送给上层页面
            string str = this.textBox1.Text+"*"+ this.textBox2.Text+"*"+ this.textBox3.Text;
            AfterMsgChange(this, new TextBoxMsgChangeEventArg() { Text = str });
            this.Close();
        }
    }
}
