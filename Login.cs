using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using System.IO;

namespace TODO
{
    public partial class Login : Form
    {
        public string path = "user.txt";//保存用户名(第一行)、密码(第二行)的文件
        string password = "";
        public Login()
        {
            InitializeComponent();
            string temp = Directory.GetCurrentDirectory();
            temp = temp.Substring(0, temp.Length - 9);
            path = temp + path;
            if (File.Exists(path))
            {
                try
                {
                    //查找是否有user.txt，如果有，读取信息显示
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line = sr.ReadLine();
                        password = sr.ReadLine();
                        if((line.Length>0)&&(password.Length!=0))
                        {
                            textBox1.Text = line;
                            textBox2.Text = password;
                        }
                    }
                }
                catch (Exception e)
                {
                    // 向用户显示出错消息
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //为了调试代码使用的临时代码，可以直接进入
            //Form1 form = new Form1();
            //UserData myuser = new UserData();
            //form.myuser = myuser;
            //this.Hide();
            //form.ShowDialog();
            //Application.ExitThread();
            
            //正常使用的代码，可以利用上次登陆记录
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
                        //生成用户对象
                        UserData user = new UserData();
                        user.user_id = obj["data"].Value<int>("user_id");
                        user.password = textBox2.Text;
                        user.email = obj["data"].Value<string>("email");
                        user.name = obj["data"].Value<string>("name");

                        //保存记录到本地
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.WriteLine(textBox1.Text);
                            sw.WriteLine(password);
                        }

                        //点击登录
                        Form1 form1 = new Form1(user);
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
