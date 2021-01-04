/********************************************************************
 * Project: PKU Todo List
 * @file: Form1.cs
 * @说明：
 * 1.Form1为本项目的主窗口类
 * 
 * *******************************************************************/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TODO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //鼠标拖动整体窗口函数（由于去除了边框）
        //Constants in Windows API
        //0x84 = WM_NCHITTEST - Mouse Capture Test
        //0x1 = HTCLIENT - Application Client Area
        //0x2 = HTCAPTION - Application Title Bar
        public const int WM_NCLBUTTONDOWN = 0xA1;//非客户区
        public const int HT_CAPTION = 0x2;//标题栏
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //退出按钮
        private void Exit_Button_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
        //文件菜单
        private void File_Button_MouseEnter(object sender, EventArgs e)
        {
            this.file_slide.Visible = true;
            MovePanel(person_list_button);
        }

        private void file_slide_MouseLeave(object sender, EventArgs e)
        {
            if (file_slide.Visible) this.file_slide.Visible = false;
        }

        //选择指示
        private void MovePanel(Control button)
        {
            this.slide_panel.Top = button.Top;
            //this.Height = button.Height;
        }

        private void collection_button_MouseEnter(object sender, EventArgs e)
        {
            MovePanel(collection_button);
            if (file_slide.Visible) file_slide.Visible = false;
            if (class_slide.Visible) class_slide.Visible = false;
        }

        private void foot_button_MouseEnter(object sender, EventArgs e)
        {
            MovePanel(foot_button);
            if (file_slide.Visible) file_slide.Visible = false;
            if (class_slide.Visible) class_slide.Visible = false;
        }

        private void all_task_button_MouseEnter(object sender, EventArgs e)
        {
            MovePanel(all_task_button);
            if (file_slide.Visible) file_slide.Visible = false;
            if (class_slide.Visible) class_slide.Visible = false;

        }

        private void class_button_MouseEnter(object sender, EventArgs e)
        {
            MovePanel(class_button);
            if (file_slide.Visible) file_slide.Visible = false;
            this.class_slide.Visible = true;
        }

        private void class_slide_MouseLeave(object sender, EventArgs e)
        {
            if (class_slide.Visible) this.class_slide.Visible = false;
        }
        private void show_class_info(object sender, EventArgs e)
        {
            //展示课程信息
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            //划分线1
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 20, this.right_display_panel.Width, 20);
            //选课按钮
            int width = 80;
            int height = 30;
            Button button = new Button();
            button.Name = "button1";
            //文字
            button.Text = "加入课程";
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = new Font("微软雅黑", 11);
            //位置和大小
            button.Location = new Point(this.right_display_panel.Width - width - 10,40);
            button.Size = new Size(width, height);
            this.right_display_panel.Controls.Add(button);

            //label
            Label label = new Label();
            label.Name = "label1";
            label.Text = "软件工程";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("微软雅黑", 11);          
            label.Location= new Point(10, 40);
            this.right_display_panel.Controls.Add(label);

            //划分线2
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 90, this.right_display_panel.Width, 90);

            //课程介绍：label2
            Label label2 = new Label();
            label2.Name = "label2";
            label2.Text = "课程介绍：\r\n\r\n课程教师：黄舟老师\r\n\r\n课程学分：2学分\r\n\r\n课程描述：软件工程是一门非常有用的课程，";
            label2.Text += "它使得软件开发变得专业化，规范化，使得大型软件开发成为可能。\r\n\r\n课程难度：适中";
            label2.TextAlign = ContentAlignment.TopLeft;
            label2.Font = new Font("微软雅黑", 11);
            label2.Location = new Point(0, 95);
            label2.Size = new Size(this.right_display_panel.Width - 20, this.right_display_panel.Height - 100);
            this.right_display_panel.Controls.Add(label2);

        }
    
        private void add_class_button_Click(object sender, EventArgs e)
        {
            //点击加入课程
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            
            int class_total = 10;//课程数目
            for (int i = 0; i < class_total; i++)
            {
                int width = this.left_display_panel.Width - 20;//之所以减20，因为有滑动轮遮拦
                int height = 50;
                Button button = new Button();
                button.Name = "button" + (i + 1).ToString();
                //文字
                button.Text = "课程名：软件工程";
                button.TextAlign = ContentAlignment.MiddleLeft;
                button.Font=new Font("微软雅黑", 11);
                //位置和大小
                button.Location = new Point(0, i * height);
                button.Size = new Size(width, height);
                //图片，显示是否完成
                button.Image = ((System.Drawing.Image)(resources.GetObject("red_button.Image")));
                button.ImageAlign = ContentAlignment.MiddleRight;
                button.Click += new System.EventHandler(this.show_class_info);
                this.left_display_panel.Controls.Add(button);
            }
        }
    }
}
