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
using System.Collections.Generic;
using System.Diagnostics;//用于打开浏览器

namespace TODO
{
    public partial class Form1 : Form
    {
        public UserData myuser;//本用户

        public string temp="";//list创建的时候公用的字符串对象
        public string temp_task_str="";//任务创建的时候传输的公共字符串
        public string temp_admin_str = "";//传递转移管理员权限传输的公共字符串

        public string left_content;//左边显示栏显示的内容，有class，task两种
        public int list_num = 0;//展示到了第几个list button
        public int class_num = 0;//展示到了第几个class button
        public int class_task_show_bios=0;//显示class_task的时候的偏置
        public int choose_list_index = -1;//被选中的列表，用于删除
        public int choose_list_index2 = -1;//选中的class

        public DataManager manager = new DataManager();//全局唯一数据库对象


        public Form1(UserData user)
        {
            manager.myuser_ = user;
            myuser = user;
            InitializeComponent();
            //预处理，导入初始数据
            preprocessing();
        }
        /// <summary>
        /// 预处理：导入所有课程和用户个人清单、课程、任务数据
        /// </summary>
        /// <returns></returns>
        private void preprocessing()
        {
            //更新所有个人信息
            manager.update_all();
            //对已有的list进行可视化
            for(int i=0;i<manager.lists.Count;i++)
            {
                temp = manager.lists[i].name;
                add_list_show();
            }
            //对已有的课程进行可视化
            for(int i=0;i<manager.person_classes.Count;i++)
            {
                int temp_index = manager.get_all_class_index(manager.person_classes[i]);
                if(temp_index>=0)
                {
                    manager.all_classes[temp_index].isSelected = true;
                    add_class_show(temp_index);
                }
            }
        }
        #region 系统基础配置
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
        #endregion

        #region 左侧菜单显示
        //
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
            MovePanel(admin_button);
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
        #endregion

        #region 左边的显示栏
        /// <summary>
        /// 列表：左侧表展示list中的task
        /// </summary>
        /// <returns></returns>
        private void show_list_info(object sender, MouseEventArgs e)
        {
            //为了确定选中的是谁
            Button btn = (Button)sender;
            string name = btn.Name.Substring(6);
            choose_list_index = Convert.ToInt32(name);
            StudentList cur_list = manager.lists[choose_list_index - 1];
            refresh();//清空显示栏
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;//图片不知道为什么只能显示在左边
            //color_imageList.ImageSize = new Size(10, 10);
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < cur_list.taskIDs.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                int index = manager.get_list_task_index(cur_list.taskIDs[i]);
                if (index != -1)
                {
                    //完成了是绿色(索引为3)，逾期未完成是红色（索引为1），未逾期未完成是黄色（索引为2）
                    if (manager.list_tasks[index].is_finished == true)
                    { lvi.ImageIndex = 3; }
                    else
                    {
                        if (manager.list_tasks[index].due_time > DateTime.Now)
                        {
                            lvi.ImageIndex = 2;
                        }
                        else lvi.ImageIndex = 1;
                    }
                    lvi.Text = "     " + manager.list_tasks[index].name;//对应文字
                }
                else
                {
                    lvi.Text = "没有任务";//对应文字
                }
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "task";
        }
        /// <summary>
        /// 课程：点击“加入课程"按钮，左边显示栏显示所有的课程
        /// </summary>
        /// <returns></returns>
        private void add_class_button_Click(object sender, EventArgs e)
        {
            refresh();//清空原有内容
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            //this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < manager.all_classes.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                
                lvi.Text ="     " +manager.all_classes[i].name;//对应文字
                //没选的是黄色（索引为5），选了的是红色（索引为1）
                if(manager.all_classes[i].isSelected)
                {
                    lvi.ImageIndex =1 ;
                }
                else { lvi.ImageIndex = 5; }
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "class";
        }

        /// <summary>
        /// 课程：左侧listview显示课程的任务
        /// </summary>
        /// <returns></returns>
        private void show_class_tasks(object sender, EventArgs e)
        {
            //为了确定选中的是谁
            Button btn = (Button)sender;
            string name = btn.Name.Substring(6);
            choose_list_index2 = Convert.ToInt32(name);
            //listview展示所有的课程任务
            int temp_index = manager.get_all_class_index(manager.person_classes[choose_list_index2 - 1]);
            StudentClass cur_class = manager.all_classes[temp_index]; 
            refresh();//清空显示栏
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            //this.color_imageList.ImageSize = new Size(10, 10); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < cur_class.alltaskIDs.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                
                int index = manager.get_class_task_index(cur_class.alltaskIDs[i]);
                if (index != -1)
                {
                    //完成了是绿色(索引为3)，逾期未完成是红色（索引为1），未逾期未完成是黄色（索引为2）
                    if (manager.class_tasks[index].is_finished == true)
                    { lvi.ImageIndex = 3; }
                    else
                    {
                        if (manager.class_tasks[index].due_time > DateTime.Now)
                        {
                            lvi.ImageIndex = 2;
                        }
                        else lvi.ImageIndex = 1;
                    }
                    lvi.Text = "     "+manager.class_tasks[index].name;//对应文字
                }
                else
                {
                    lvi.Text = "没有任务";//对应文字
                }
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "class_task";
        }

        /// <summary>
        /// 点击“所有任务”按钮：左侧listview显示用户所有的任务
        /// </summary>
        /// <returns></returns>
        private void all_task_button_MouseClick(object sender, MouseEventArgs e)
        {
            //显示所有任务
            refresh();//清空显示栏
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            //this.color_imageList.ImageSize = new Size(10, 10); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();
            //显示列表任务
            for (int i = 0; i < manager.list_tasks.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //完成了是绿色(索引为3)，逾期未完成是红色（索引为1），未逾期未完成是黄色（索引为2）
                if (manager.list_tasks[i].is_finished == true) 
                { lvi.ImageIndex = 3; }
                else {
                    if (manager.list_tasks[i].due_time > DateTime.Now)
                    {
                        lvi.ImageIndex = 2;
                    }
                    else lvi.ImageIndex = 1;
                }

                lvi.Text ="     "+ manager.list_tasks[i].name;//对应文字
                this.left_display_view.Items.Add(lvi);
            }
            //显示课程任务
            for (int i = 0; i < manager.class_tasks.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //完成了是绿色(索引为3)，逾期未完成是红色（索引为1），未逾期未完成是黄色（索引为2）
                if (manager.class_tasks[i].is_finished == true)
                { lvi.ImageIndex = 3; }
                else
                {
                    if (manager.class_tasks[i].due_time > DateTime.Now)
                    {
                        lvi.ImageIndex = 2;
                    }
                    else lvi.ImageIndex = 1;
                }

                lvi.Text = "     "+manager.class_tasks[i].name;//对应文字

                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "all_task";
        }

        /// <summary>
        /// 点击左边显示栏的一项触发的事件
        /// </summary>
        /// <returns></returns>
        private void left_display_view_SelectedIndexChanged(object sender, EventArgs e)
        {
            //左边显示栏是“课程”，不是“任务”
            if (left_content == "class")
            {
                //选中list_view当中的行变化
                try
                {
                    ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
                    if (indexes.Count > 0)
                    {
                        int index = indexes[0];
                        string sPartNo = this.left_display_view.Items[index].SubItems[0].Text;//获取第一列的值
                                                                                              //string sPartName = this.left_display_view.Items[index].SubItems[1].Text;//获取第二列的值

                        //右侧生成对应课程的内容
                        show_class_info(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败！\n" + ex.Message, "提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
            }
            
            //左边显示栏是“任务”，而不是课程
            else if (left_content == "task")
            {
                //选中list_view当中的行变化
                try
                {
                    ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
                    if (indexes.Count > 0)
                    {
                        int index = indexes[0];
                        string sPartNo = this.left_display_view.Items[index].SubItems[0].Text;//获取第一列的值

                        //右侧生成对应任务的内容
                        show_task_info(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败！\n" + ex.Message, "提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
            }

            //左边显示栏是“管理员的课程”
            else if (left_content == "admin")
            {
                //什么都不用干
            }
            //左边显示栏是“课程作业”
            else if(left_content=="class_task")
            {
                //选中list_view当中的行变化
                try
                {
                    ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
                    if (indexes.Count > 0)
                    {
                        int index = indexes[0];
                        string sPartNo = this.left_display_view.Items[index].SubItems[0].Text;//获取第一列的值

                        //右侧生成对应任务的内容
                        //确定选中的task
                        index = indexes[0] - class_task_show_bios;//对应该class中的索引,此处class_task_show_bios没有用
                        class_task_show_bios = 0;
                        int temp_index = manager.get_all_class_index(manager.person_classes[choose_list_index2 - 1]);
                        StudentClass cur_class = manager.all_classes[temp_index];
                        int task_index = manager.get_class_task_index(cur_class.alltaskIDs[index]);//class_tasks中的索引
                        show_class_task_info(sender, e,task_index);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败！\n" + ex.Message, "提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
            }
            else if(left_content=="all_task")
            {
                //选中list_view当中的行变化
                try
                {
                    ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
                    if (indexes.Count > 0)
                    {
                        int index = indexes[0];
                        if(index< manager.list_tasks.Count)
                        {
                            //右侧生成对应list任务的内容
                            show_task_info(sender, e);
                        }
                        else
                        {
                            //右侧生成对应课程任务的内容
                            class_task_show_bios = manager.list_tasks.Count;//结束之后会自动恢复0
                            index = indexes[0] - class_task_show_bios;//对应该class中的索引,此处class_task_show_bios没有用
                            class_task_show_bios = 0;
                            show_class_task_info(sender, e, index);
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败！\n" + ex.Message, "提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
            }
        }
        #endregion

        #region 右边的显示栏
        

        /// <summary>
        /// 课程：右侧label显示课程的信息
        /// </summary>
        /// <returns></returns>
        private void show_class_info(object sender, EventArgs e)
        {
            //清除已有内容
            right_display_panel.Controls.Clear();
            //right_display_panel.CreateGraphics().Clear(Color.White);
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
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中课程的index
            int index = indexes[0];
            //判断是否选了那门课
            if (!button.Enabled) { button.Enabled = true; }
            if (!manager.all_classes[index].isSelected)
            {
                button.Text = "加入课程";
                button.MouseClick += this.add_new_class;
            }
            else
            {
                button.Text = "删除课程";
                button.MouseClick += this.del_new_class;
            }
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = new Font("微软雅黑", 11);
            //位置和大小
            button.Location = new Point(this.right_display_panel.Width - width - 10, 40);
            button.Size = new Size(width, height);

            this.right_display_panel.Controls.Add(button);

            //label
            Label label = new Label();
            label.Name = "label1";
            label.Text = manager.all_classes[index].name;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("微软雅黑", 11);
            label.Location = new Point(10, 40);
            label.AutoSize = true;
            this.right_display_panel.Controls.Add(label);

            //划分线2
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 90, this.right_display_panel.Width, 90);

            //课程介绍：label2
            //@warning:需要更改内容
            Label label2 = new Label();
            label2.Name = "label2";
            //label2.Text = "课程介绍：\r\n\r\n课程教师：黄舟老师\r\n\r\n课程学分：2学分\r\n\r\n课程描述：软件工程是一门非常有用的课程，";
            //label2.Text += "它使得软件开发变得专业化，规范化，使得大型软件开发成为可能。\r\n\r\n课程难度：适中";
            label2.Text = "课程介绍：\r\n\r\n课程教师：";
            if (manager.all_classes[index].teacher_name!=null) { label2.Text += manager.all_classes[index].teacher_name+"老师"; }
            else { label2.Text += "无"; }
            label2.Text += "\r\n\r\n课程学分：";
            if (manager.all_classes[index].score!=0) { label2.Text += manager.all_classes[index].score.ToString()+"学分"; }
            else { label2.Text += "无"; }
            label2.Text += "\r\n\r\n课程描述："+ manager.all_classes[index].description; 

            label2.TextAlign = ContentAlignment.TopLeft;
            label2.Font = new Font("微软雅黑", 11);
            label2.Location = new Point(0, 95);
            label2.Size = new Size(this.right_display_panel.Width - 20, this.right_display_panel.Height - 100);
            this.right_display_panel.Controls.Add(label2);
        }

        /// <summary>
        /// 课程：右侧label显示课程的任务
        /// </summary>
        /// <returns></returns>
        private void show_class_task_info(object sender, EventArgs e, int task_index)
        {

            //清空原有内容
            right_display_panel.Controls.Clear();
            //展示任务信息
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            //划分线1
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 20, this.right_display_panel.Width, 20);

            //label
            Label label = new Label();
            label.Name = "label1";
            label.Text = "任务名：" + manager.class_tasks[task_index].name;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("微软雅黑", 11);
            label.Location = new Point(5, 40);
            label.Size = new Size(200, 20);
            this.right_display_panel.Controls.Add(label);

            //划分线2
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 80, this.right_display_panel.Width, 80);

            //任务介绍：label2
            Label label2 = new Label();
            label2.Name = "label2";
            //label2.Text = "任务介绍：\r\n\r\n任务开始时间：2020.11.25\r\n\r\n任务到期时间：2021.01.24\r\n\r\n任务描述：软件工程大作业是一个非常有挑战的工作，";
            //label2.Text += "它要求我们把软件工程课程学到的东西都融会贯通";
            if (task_index < 0) { label2.Text = "任务索引出错"; }
            else
            {
                label2.Text = "任务介绍：\r\n\r\n任务开始时间：" + manager.class_tasks[task_index].start_time.ToString();
                label2.Text += "\r\n\r\n任务到期时间：" + manager.class_tasks[task_index].due_time.ToString();
                label2.Text += "\r\n\r\n任务描述：" + manager.class_tasks[task_index].description;
            }
            label2.TextAlign = ContentAlignment.TopLeft;
            label2.Font = new Font("微软雅黑", 11);
            label2.Location = new Point(0, 85);
            label2.Size = new Size(this.right_display_panel.Width - 20, this.right_display_panel.Height - 100);
            this.right_display_panel.Controls.Add(label2);

            //提交按钮
            int width = 100;
            int height = 50;
            Button button = new Button();
            button.Name = "button" + manager.class_tasks[task_index].task_id.ToString();//通过按钮名字传递参数
            button.Text = "上交作业";
            button.MouseClick += this.submit_homework;

            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = new Font("微软雅黑", 11);
            //位置和大小
            button.Location = new Point(this.right_display_panel.Width - width - 40, 300);
            button.Size = new Size(width, height);

            //完成按钮
            Button button1 = new Button();
            button1.Name = "button_finish" + manager.class_tasks[task_index].task_id.ToString();//通过按钮名字传递参数
            button1.Text = "作业完成";
            button1.MouseClick += this.finish_class_task;

            button1.TextAlign = ContentAlignment.MiddleCenter;
            button1.Font = new Font("微软雅黑", 11);
            //位置和大小
            button1.Location = new Point(40, 300);
            button1.Size = new Size(width, height);

            this.right_display_panel.Controls.Add(button);
            this.right_display_panel.Controls.Add(button1);
            button.BringToFront();
            button1.BringToFront();
        }
        /// <summary>
        /// 列表：右侧label显示清单的任务
        /// </summary>
        /// <returns></returns>
        private void show_task_info(object sender, EventArgs e)
        {
            //确定选中的task
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中课程的index
            int index = indexes[0];//对应该list中的索引
            int temp_index=-1;
            Task cur_task=new Task();
            //如果是task的情况
            if (this.left_content=="task")
            {
                StudentList cur_list = manager.lists[choose_list_index - 1];
                temp_index = manager.get_list_task_index(cur_list.taskIDs[index]);//list_tasks中的索引
                cur_task = manager.list_tasks[temp_index];
            }

            //如果是all_task的情况
            else if(this.left_content=="all_task")
            {
                temp_index = index;
                cur_task = manager.list_tasks[temp_index];
            }
            else { Debug.Assert(false); }
            right_display_panel.Controls.Clear();
            //展示任务信息
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            //划分线1
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 20, this.right_display_panel.Width, 20);

            //label
            Label label = new Label();
            label.Name = "label1";
            label.Text = "任务名：" + cur_task.name;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("微软雅黑", 11);
            label.Location = new Point(10, 40);
            label.Size = new Size(200, 20);
            this.right_display_panel.Controls.Add(label);

            //修改按钮
            int width = 100;
            int height = 50;
            Button button = new Button();
            button.Name = "button" + cur_task.task_id.ToString();//传递在manager.list_tasks中的索引
            button.Text = "修改";
            button.MouseClick += this.modify_task_info;

            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = new Font("微软雅黑", 11);
            //位置和大小
            button.Location = new Point(this.right_display_panel.Width - width - 40, 300);
            button.Size = new Size(width, height);

            //完成按钮
            Button button1 = new Button();
            Debug.Assert(temp_index!=-1);
            button1.Name = "button_finish" + manager.list_tasks[temp_index].task_id.ToString();//通过按钮名字传递参数
            button1.Text = "完成";
            button1.MouseClick += this.finish_personal_task;

            button1.TextAlign = ContentAlignment.MiddleCenter;
            button1.Font = new Font("微软雅黑", 11);
            //位置和大小
            button1.Location = new Point(40, 300);
            button1.Size = new Size(width, height);

            this.right_display_panel.Controls.Add(button);
            this.right_display_panel.Controls.Add(button1);
            button.BringToFront();
            button1.BringToFront();

            //划分线2
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 80, this.right_display_panel.Width, 80);

            //任务介绍：label2
            Label label2 = new Label();
            label2.Name = "label2";
            //label2.Text = "任务介绍：\r\n\r\n任务开始时间：2020.11.25\r\n\r\n任务到期时间：2021.01.24\r\n\r\n任务描述：软件工程大作业是一个非常有挑战的工作，";
            //label2.Text += "它要求我们把软件工程课程学到的东西都融会贯通";
            if (true)
            {
                label2.Text = "任务介绍：\r\n\r\n任务开始时间：" + cur_task.start_time.ToString();
                label2.Text += "\r\n\r\n任务到期时间：" + cur_task.due_time.ToString();
                label2.Text += "\r\n\r\n任务描述：" + cur_task.description;
            }
            label2.TextAlign = ContentAlignment.TopLeft;
            label2.Font = new Font("微软雅黑", 11);
            label2.Location = new Point(0, 85);
            label2.Size = new Size(this.right_display_panel.Width - 20, this.right_display_panel.Height - 100);
            this.right_display_panel.Controls.Add(label2);
        }
        #endregion

        #region 添加、删除课程
        private void add_new_class(object sender, MouseEventArgs e)
        {
            //加入新课程
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中课程的index
            int index = indexes[0];
            manager.add("person_classes", manager.all_classes[index]);//加入课程

            Button temp_button = (Button)sender;
            temp_button.Text = "删除课程";
            temp_button.Enabled = false;//不能再点
            //展示加入的新课程
            add_class_show(index);
            this.left_display_view.SelectedItems[0].ImageIndex = 1;
        }
        private void del_new_class(object sender, MouseEventArgs e)
        {
            //删除已有的课程
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中课程的index
            int index = indexes[0];
            ////判断是否可以删除，如果是课程管理员，不能删除
            //if(manager.all_classes[index].admin_id==this.myuser.user_id)
            //{
            //    //产生弹窗
            //    try
            //    {
            //        MessageBox.Show("管理员不能删除本课程！");
            //    }
            //    catch (Exception msg) //异常处理
            //    {
            //        MessageBox.Show(msg.Message);
            //    }
            //    return;
            //}

            //清掉所有任务
            StudentClass cur_class = manager.all_classes[index];
            bool temp=manager.delete("person_classes", cur_class);

            Button button1 = (Button)sender;
            button1.Text = "加入课程";

            this.class_slide.Controls.Clear();
            this.class_slide.Size = new Size(this.add_list_button.Size.Width, class_slide.Size.Height - this.add_list_button.Height);
            this.add_class_button.Location = new Point(0, 0);
            this.class_slide.Controls.Add(this.add_class_button);

            class_num = 0;
            //放入其他组件
            for (int i = 0; i < manager.person_classes.Count; i++)
            {
                int temp_index = manager.get_all_class_index(manager.person_classes[i]);
                if (temp_index >= 0)
                {
                    add_class_show(temp_index);
                }
            }

            this.left_display_view.SelectedItems[0].ImageIndex = 5;
        }
        public void add_class_show(int index)
        {
            class_num += 1;
            this.class_slide.Size = new Size(this.class_slide.Size.Width, (manager.person_classes.Count+1)*this.add_list_button.Height);
            Button button = new Button();
            button.Name = "button" + class_num.ToString();
            //文字
            button.Text = manager.all_classes[index].name;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = this.add_class_button.Font;
            button.ForeColor = this.add_class_button.ForeColor;
            //位置和大小
            button.Location = new Point(0, class_num* this.add_list_button.Height);
            button.Size = this.add_class_button.Size;
            //背景
            button.BackColor = this.add_class_button.BackColor;
            //按钮边缘
            button.FlatAppearance.BorderSize = 0;
            button.Padding = this.add_class_button.Padding;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.show_class_tasks);
            button.MouseEnter += new System.EventHandler(this.classbuttonEnter);
            this.class_slide.Controls.Add(button);
        }
        #endregion

        #region 添加、删除清单或者清单任务
        private void delete_menu_Click(object sender, EventArgs e)
        {
            if (this.left_display_view.SelectedItems.Count == 0)
                return;
            var selectedItem = this.left_display_view.SelectedItems[0];
            try
            {
                if (MessageBox.Show("确认要删除该项?", "提示",
                            System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Question) ==
                            System.Windows.Forms.DialogResult.Yes)
                {
                    //移除对应项
                    foreach (ListViewItem item in this.left_display_view.SelectedItems)
                    {
                        this.left_display_view.Items.Remove(item);
                    }
                    //@warning:
                    refresh();//刷新左右两个显示栏
                    //更改数据库
                }
                MessageBox.Show(this, "成功删除！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception msg) //异常处理
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void list_delete_menu_Click(object sender, EventArgs e)
        {
            /*删除对应的list*/
            StudentList cur_list = manager.lists[choose_list_index - 1];
            manager.delete("lists", cur_list);//所有任务都交给manager
            list_num= 0;
            //清空所有组件
            this.file_slide.Controls.Clear();
            this.add_list_button.Location = new Point(0, 0);
            this.file_slide.Controls.Add(this.add_list_button);
            for (int i = 0; i < manager.lists.Count; i++)
            {
                temp = manager.lists[i].name;
                add_list_show();
            }
        }
        
        /// <summary>
        /// 列表：从list右键添加任务，弹出AddTask窗口
        /// </summary>
        /// <returns></returns>
        private void add_task_menu_Click(object sender, EventArgs e)
        {
            //跳出新的列表界面，将列表信息传递过来
            AddTask addtask = new AddTask();
            addtask.AfterMsgChange += this.AfterTxtChange2;
            addtask.ShowDialog();

            if (temp_task_str.Length == 0)
            {
                return;//没有输入则返回
            }
            //创建新的task
            string[] temp_str = temp_task_str.Split('*'); temp_task_str = "";
            Task new_task = new Task();
            new_task.name = temp_str[0];
            new_task.start_time = DateTime.Now;
            new_task.due_time = str2date(temp_str[1]);

            new_task.description = temp_str[2];
            new_task.parent_id = manager.lists[choose_list_index - 1].list_id;

            if (manager.list_tasks.Count > 0)
            { new_task.task_id = manager.list_tasks[manager.list_tasks.Count - 1].task_id + 1; }
            else
            {
                new_task.task_id = 0;
            }
            manager.add("list_tasks", new_task);
            StudentList cur_list = manager.lists[choose_list_index - 1];//当前列表

            //点击加入课程
            refresh();//清除原有内容
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            //this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < cur_list.taskIDs.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                int index = manager.get_list_task_index(cur_list.taskIDs[i]);
                if (index != -1)
                {
                    //完成了是绿色(索引为3)，逾期未完成是红色（索引为1），未逾期未完成是蓝色（索引为2）
                    if (manager.list_tasks[index].is_finished == true)
                    { lvi.ImageIndex = 3; }
                    else
                    {
                        if (manager.list_tasks[index].due_time > DateTime.Now)
                        {
                            lvi.ImageIndex = 2;
                        }
                        else lvi.ImageIndex = 1;
                    }
                    lvi.Text = "  "+manager.list_tasks[index].name;//对应文字
                }
                else
                {
                    lvi.Text = "不存在的任务";//对应文字
                    lvi.ImageIndex = 0;
                }
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "task";
        }
        /// <summary>
        /// 列表：点击”+“号，增加新的列表
        /// </summary>
        /// <returns></returns>
        /// 
        private void add_list_button_MouseClick(object sender, MouseEventArgs e)
        {
            //点击增加任务列表
            //跳出新的列表界面，将列表信息传递过来
            AddList addlist = new AddList();
            addlist.AfterMsgChange += this.AfterTxtChange;
            addlist.ShowDialog();

            if (temp.Length == 0)
            {
                return;//没有输入则返回
            }
            
            //创建新的StudentList
            StudentList new_list = new StudentList();
            new_list.name = temp;
            if (manager.lists.Count != 0)
            {
                new_list.list_id = manager.lists[manager.lists.Count - 1].list_id + 1;
            }
            else
            {
                new_list.list_id = 1;
            }
            manager.add("lists", new_list);
            //将新的list button展示出来
            add_list_show();
        }
        /// <summary>
        /// 列表：增加列表对应的可视化效果
        /// </summary>
        /// <returns></returns>
        public void add_list_show()
        {
            //更新大小
            list_num += 1;
            this.file_slide.Size = new Size(file_slide.Width, add_list_button.Size.Height * (manager.lists.Count+1));

            Button button = new Button();
            button.Name = "button" + list_num.ToString();
            //文字
            button.Text = temp; temp = "";
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = this.add_class_button.Font;
            button.ForeColor = this.add_class_button.ForeColor;
            //位置和大小
            button.Location = new Point(0, (list_num-1)* add_list_button.Size.Height);
            button.Size = this.add_list_button.Size;
            //背景
            button.BackColor = this.add_class_button.BackColor;
            //按钮边缘
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.show_list_info);
            button.MouseEnter += new System.EventHandler(this.listbuttonEnter);
            //右键删除事件
            button.ContextMenuStrip = this.list_contextMenuStrip;

            this.file_slide.Controls.Add(button);

            //更新+号的位置
            this.add_list_button.Location = new Point(0, this.file_slide.Height-add_list_button.Size.Height);
        }
        #endregion

        #region 辅助函数

        /// <summary>
        /// 用户：点击注销，回到登陆界面
        /// </summary>
        /// <returns></returns>
        private void logout_button_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            Application.ExitThread();
        }
        //将字符串转化为Datetime
        public DateTime str2date(string str)
        {
            string[] temp = str.Split('.');
            //@warning:此处可以增加检查代码，否则有漏洞
            string dateString = temp[0] + temp[1];
            if (temp[1].Length == 1)
            {
                dateString = temp[0] + "0" + temp[1];
            }
            if(temp[2].Length == 1)
            {
                dateString += "0"+temp[2];
            }
            else
            {
                dateString += temp[2];
            }
            DateTime dt = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            return dt;
        }
        //退出按钮
        private void Exit_Button_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
        public void AfterTxtChange(object sender, EventArgs e)
        {
            //拿到addlist传来的文本，强转数据类型
            TextBoxMsgChangeEventArg arg = e as TextBoxMsgChangeEventArg;
            this.temp = arg.Text;//交给公共变量
        }
        public void AfterTxtChange2(object sender, EventArgs e)
        {
            //拿到addtask传来的文本，强转数据类型
            TextBoxMsgChangeEventArg arg = e as TextBoxMsgChangeEventArg;
            this.temp_task_str = arg.Text;//交给公共变量
        }
        public void AfterTxtChange3(object sender, EventArgs e)
        {
            //拿到transferform传来的文本，强转数据类型
            TextBoxMsgChangeEventArg arg = e as TextBoxMsgChangeEventArg;
            this.temp_admin_str = arg.Text;//交给公共变量
        }
        public void listbuttonEnter(object sender, EventArgs e)
        {
            ////为了确定选中的是谁
            //Button btn = (Button)sender;
            //string name = btn.Name.Substring(6);
            //choose_list_index = Convert.ToInt32(name);
        }

        public void classbuttonEnter(object sender, EventArgs e)
        {
            ////为了确定选中的是谁
            //Button btn = (Button)sender;
            //string name = btn.Name.Substring(6);
            //choose_list_index2 = Convert.ToInt32(name);
        }
        
        private void refresh_button_MouseClick(object sender, MouseEventArgs e)
        {
            refresh();
        }
        private void refresh()
        {
            //刷新左右两个显示栏,不负责和数据库交互
            //清除原有内容
            this.left_display_view.Clear();
            this.right_display_panel.Controls.Clear();
            this.right_display_panel.CreateGraphics().Clear(Color.White);
        }

        private void left_display_view_MouseClick(object sender, MouseEventArgs e)
        {
            //禁止多选
            this.left_display_view.MultiSelect = false;
            //鼠标右键,使用菜单，发布作业或者转让管理权
            if ((e.Button == MouseButtons.Right)&&(this.left_content=="admin"))
            {
                //选中列表中数据才显示 空白处不显示
                int index = this.left_display_view.SelectedIndices[0];//选中的编号
                String itemName = this.left_display_view.SelectedItems[0].Text; //获取选中课程名
                Point p = new Point(e.X, e.Y);
                this.contextMenuStrip3.Show(this.left_display_view, p);
            }
        }
        #endregion

        #region 管理员权限
        /// <summary>
        /// 管理员：点击“管理员”按钮进入管理员界面
        /// </summary>
        /// <returns></returns>
        private void admin_button_Click(object sender, EventArgs e)
        {
            //显示所有任务
            refresh();//清空显示栏
            this.left_display_view.View = View.List;

            //this.left_display_view.SmallImageList = this.color_imageList;
            //this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();
            
            //显示管理的课程
            for (int i = 0; i < myuser.administrator_list.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = manager.list_tasks.Count + i;
                int temp_index = manager.get_all_class_index(myuser.administrator_list[i]);
                if (temp_index >= 0)
                {
                    lvi.Text = manager.all_classes[temp_index].name;//对应文字
                }
                else { lvi.Text = "没有这门课"; }
                lvi.ImageIndex = 0;
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "admin";
        }
        /// <summary>
        /// 管理员：右键管理课程，进入发布作业的界面
        /// </summary>
        /// <returns></returns>
        private void SendTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //右键管理课程，进入发布作业的界面
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
            int index = indexes[0];
            string sPartNo = this.left_display_view.Items[index].SubItems[0].Text;//获取第一列的值

            AddTask addtask = new AddTask();
            addtask.AfterMsgChange += this.AfterTxtChange2;
            addtask.ShowDialog();

            if (temp_task_str.Length == 0)
            {
                return;//没有输入则返回
            }
            //创建新的task
            string[] temp_str = temp_task_str.Split('*'); temp_task_str = "";
            Task new_task = new Task();
            new_task.name = temp_str[0];
            new_task.start_time = DateTime.Now;
            new_task.due_time = str2date(temp_str[1]);

            new_task.description = temp_str[2];
            new_task.parent_id = myuser.administrator_list[index];
            manager.add("person_class_tasks", new_task);
        }
        /// <summary>
        /// 管理员：右键“转让管理员权限”
        /// </summary>
        /// <returns></returns>
        private void TransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //右键“转让管理员权限”
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
            int index = indexes[0];
            string sPartNo = this.left_display_view.Items[index].SubItems[0].Text;//获取第一列的值
            //获取所有选课用户
            List<UserData> users = manager.get_class_user(myuser.administrator_list[index]);
            TransferForm transfer = new TransferForm();
            transfer.users = users;
            transfer.myid = myuser.user_id;
            transfer.AfterMsgChange += this.AfterTxtChange3;
            transfer.ShowDialog();

            if (temp_admin_str.Length == 0)
            {
                return;//没有输入则返回
            }
            int user_index = Convert.ToInt32(temp_admin_str);//选择的用户索引
            temp_admin_str = "";

            manager.tranfer_admin(myuser.user_id, users[user_index].user_id, myuser.administrator_list[index]);
        }

        #endregion

        /// <summary>
        /// 列表：右键重命名list
        /// </summary>
        /// <returns></returns>
        private void listRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //右键重命名list
            StudentList cur_list = manager.lists[choose_list_index - 1];

            //跳出新的列表界面，将列表信息传递过来
            AddList addlist = new AddList(cur_list.name);
            addlist.AfterMsgChange += this.AfterTxtChange;
            addlist.ShowDialog();

            if (temp.Length == 0)
            {
                return;//没有输入则返回
            }

            //创建新的StudentList
            StudentList new_list = new StudentList();
            new_list.name = temp;
            new_list.list_id = cur_list.list_id;
            manager.modify("lists", new_list);

            //根据新的名字可视化
            list_num = 0;
            this.file_slide.Controls.Clear();
            this.add_list_button.Location = new Point(0, 0);
            this.file_slide.Controls.Add(this.add_list_button);
            for (int i = 0; i < manager.lists.Count; i++)
            {
                temp = manager.lists[i].name;
                add_list_show();
            }
        }
        private void modify_task_info(object sender, EventArgs e)
        {
            //弹出更改任务信息的窗口
            Button button = (Button)sender;
            int temp_index = Convert.ToInt32(button.Name.Substring(6));//task_id
            temp_index = manager.get_list_task_index(temp_index);//在manager.list_tasks中的索引
            Task cur_task = manager.list_tasks[temp_index];//当前的任务

            string content =cur_task.name+ "*" + cur_task.due_time.ToString("yyyy.MM.dd") +"*"+ cur_task.description;

            AddTask addtask = new AddTask(content);//打开修改页面
            addtask.AfterMsgChange += this.AfterTxtChange2;
            addtask.ShowDialog();

            if (temp_task_str.Length == 0)
            {
                return;//没有输入则返回
            }
            //创建新的task
            string[] temp_str = temp_task_str.Split('*'); temp_task_str = "";
            Task new_task = new Task();
            new_task.name = temp_str[0];
            new_task.start_time = DateTime.Now;
            new_task.due_time = str2date(temp_str[1]);

            new_task.description = temp_str[2];
            new_task.parent_id = manager.lists[choose_list_index - 1].list_id;
            new_task.task_id = cur_task.task_id;

            manager.modify("list_tasks", new_task);//task_id没有变化，可以用于检索

            //根据新的内容可视化
            show_task_info(sender, e);
        }
        private void submit_homework(object sender, EventArgs e)
        {
            //上交作业
            Button button = (Button)sender;
            string url = "10.128.169.239:5000/filesubmit?taskid=";
            url += button.Name.Substring(6);
            Process.Start(url);//在浏览器打开链接
        }
        private void finish_personal_task(object sender, EventArgs e)
        {
            //完成task
            Button button = (Button)sender;
            int temp_index = Convert.ToInt32(button.Name.Substring(13));//task_id
            temp_index = manager.get_list_task_index(temp_index);//在manager.list_tasks中的索引
            if(manager.finish(manager.list_tasks[temp_index]))
            {
                ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
                ListViewItem lvi = new ListViewItem();
                //lvi.ImageIndex = 3;
                //int index = this.left_display_view.SelectedIndices[0];
                //this.left_display_view.Items.Insert(index, lvi);
                //this.left_display_view.Items.RemoveAt(index + 1);
                this.left_display_view.SelectedItems[0].ImageIndex = 3;
            }
        }
        private void finish_class_task(object sender, EventArgs e)
        {
            //完成task
            Button button = (Button)sender;
            int temp_index = Convert.ToInt32(button.Name.Substring(13));//task_id
            temp_index = manager.get_class_task_index(temp_index);//在manager.list_tasks中的索引
            if(manager.finish(manager.list_tasks[temp_index]))
            {
                ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中的index
                this.left_display_view.SelectedItems[0].ImageIndex = 3;
            }
        }
    }
}

//目前存在的明显问题
//1.修改功能（List和Task）
//2.增加上传作业的按钮（打开超链接）
//3.完成情况的显示（不同颜色的图标）,目前只能用空格的方式
//4.发布作业之后还是无法响应

/// <summary>
/// 列表：单击左侧表项触发的事件，在右边的显示栏展示列表中的task的内容
/// </summary>
/// <returns></returns>