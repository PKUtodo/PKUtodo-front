using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TODO
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                textBox1.Text = "邮箱";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
                textBox2.Text = "验证码";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
                textBox3.Text = "用户名";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 0)
                textBox4.Text = "密码";
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 0)
                textBox5.Text = "确认密码";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JObject obj = HTTP.HttpPost(JSONHelper.CreateJson(MessageType.verify, textBox1.Text));
            if(obj != null)
            {
                if(obj.Value<int>("success") == 1)
                {
                    MessageBox.Show("验证码发送成功！");
                }
                else if(obj.Value<int>("success") == 0)
                {
                    MessageBox.Show(obj.Value<string>("error_msg"));
                }
                else
                {
                    MessageBox.Show("出现未知错误","ERROR");
                }
            }
            else
            {
                MessageBox.Show("邮件发送失败！", "ERROR");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == textBox5.Text)
                {
                    JObject obj = HTTP.HttpPost(JSONHelper.CreateJson(MessageType.set_up, textBox1.Text, Convert.ToInt32(textBox2.Text), textBox3.Text, textBox4.Text));
                    if (obj != null)
                    {
                        if(obj.Value<int>("success") == 1)
                        {
                            UserData.user_id = obj.Value<int>("user_id");
                            UserData.password = textBox5.Text;
                            //点击登录
                            Form1 form1 = new Form1();
                            this.Hide();
                            form1.ShowDialog();
                            Application.ExitThread();
                            //this.Show();
                        }
                        else if(obj.Value<int>("success") == 0)
                        {
                            MessageBox.Show(obj.Value<string>("error_msg"));
                        }
                        else
                        {
                            MessageBox.Show("出现未知错误", "ERROR");
                        }
                    }
                    else
                    {
                        MessageBox.Show("注册失败！", "ERROR");
                    }

                }
                else
                {
                    MessageBox.Show("密码输入不相同！", "ERROR");
                }
            }
            catch
            {
                MessageBox.Show("出错了！", "ERROR");
            }
        }
    }
}
