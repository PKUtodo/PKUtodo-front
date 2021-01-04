using System;
using System.Windows.Forms;

namespace TODO
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //点击登录
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            Application.ExitThread();
            //this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //点击注册
            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            //Application.ExitThread();
            this.Show();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
