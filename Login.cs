using Newtonsoft.Json.Linq;
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
            //为了调试代码使用的临时代码，可以直接进入
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
            Application.ExitThread();
            
            //正常使用的代码
            try
            {
                JObject obj =  HTTP.HttpPost(JSONHelper.CreateJson(MessageType.login, textBox1.Text, textBox2.Text));
                if(obj != null)
                {
                    if((obj.Value<int>("success")) == 0)
                    {
                        MessageBox.Show(obj.Value<string>("error_msg"), "ERROR");
                    }
                    else if ((obj.Value<int>("success")) == 1)
                    {
                        UserData.user_id = obj.Value<int>("user_id");
                        UserData.password = textBox2.Text;
                        //点击登录
                        Form1 form1 = new Form1();
                        this.Hide();
                        form1.ShowDialog();
                        Application.ExitThread();
                        //this.Show();
                    }
                    else
                    {
                        MessageBox.Show("出现未知错误", "ERROR");
                    }
                }
                else
                {
                    MessageBox.Show("登陆失败！", "ERROR");
                }
            }
            catch
            {
                MessageBox.Show("出现未知错误", "ERROR");
            }
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

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0)
                textBox1.Text = "邮箱";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
                textBox2.Text = "密码";
        }
    }
}
