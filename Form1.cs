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

namespace TODO
{
    public partial class Form1 : Form
    {
        public UserData myuser;//本用户

        public string temp="";//list创建的时候公用的字符串对象
        public string temp_task_str="";//任务创建的时候传输的公共字符串
        public string left_content;//左边显示栏显示的内容，有class，task两种
        public int list_num = 0;//初始化列表数目
        public int choose_list_index = -1;//被选中的列表，用于删除
        public int choose_list_index2 = -1;//选中的class

        public List<StudentList> lists=new List<StudentList>();//所有的表单
        public List<StudentClass> classes = new List<StudentClass>();//所有个人的课程
        public List<Task> tasks= new List<Task>();//所有的私人任务和选课的公有任务（拷贝到本地）
        public List<StudentClass> all_classes = new List<StudentClass>();//所有的学校课程
        public List<Task> all_tasks = new List<Task>();//所有课程中的任务

        public Form1()
        {
            InitializeComponent();
            preprocessing();
        }
        /// <summary>
        /// 预处理：导入所有课程和用户个人清单、课程、任务数据
        /// </summary>
        /// <returns></returns>
        private void preprocessing()
        {
            //第一步：导入所有课程数据
            for (int i = 0; i < 10; i++)
            {
                StudentClass new_class = new StudentClass();
                new_class.class_id = i;
                new_class.name = "软件工程" + i.ToString();
                new_class.score = 3;
                new_class.description = "课程介绍：\r\n\r\n课程教师：黄舟老师\r\n\r\n课程学分：2学分\r\n\r\n课程描述：软件工程是一门非常有用的课程，";
                new_class.description += "它使得软件开发变得专业化，规范化，使得大型软件开发成为可能。\r\n\r\n课程难度：适中";
                Task new_task = new Task();
                new_task.description = "软件工程作业对学习很有帮助。";
                new_task.due_time = DateTime.Now;
                new_task.start_time = DateTime.Now;
                new_task.name = "软件工程" + i.ToString() + "作业" + i.ToString();
                new_task.parent_type = "class";
                new_task.parent_id = i;
                if (all_tasks.Count > 0)
                {
                    new_task.task_id = all_tasks[all_tasks.Count - 1].task_id + 1;
                }
                else { new_task.task_id = 0; }
                all_tasks.Add(new_task);
                new_class.alltaskIDs.Add(new_task.task_id);
                all_classes.Add(new_class);
            }
            //第二步：导入用户个人清单，连同清单中的任务
            //第三步：导入用户个人课程，连同个人课程中的任务
            //
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
        /// 课程：点击“加入课程"按钮，左边显示栏显示所有的课程
        /// </summary>
        /// <returns></returns>
        private void add_class_button_Click(object sender, EventArgs e)
        {
            refresh();//清空原有内容
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < all_classes.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                lvi.Text = all_classes[i].name;//对应文字
                lvi.ImageIndex = 0;
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
            //listview展示所有的课程任务
            StudentClass cur_class = classes[choose_list_index2 - 1];
            refresh();//清空显示栏
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < cur_class.alltaskIDs.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                int index = get_all_task_index(cur_class.alltaskIDs[i]);
                if (index != -1)
                {
                    lvi.Text = all_tasks[index].name;//对应文字
                }
                else
                {
                    lvi.Text = "没有任务";//对应文字
                }
                lvi.ImageIndex = 0;
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "task";
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
            this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < tasks.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                lvi.Text = tasks[i].name;//对应文字
                lvi.ImageIndex = 0;
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "task";
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
                                                                                              //string sPartName = this.left_display_view.Items[index].SubItems[1].Text;//获取第二列的值

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
        }
        #endregion

        #region 右边的显示栏
        /// <summary>
        /// 列表：单击左侧表项触发的事件，在右边的显示栏展示列表中的task的内容
        /// </summary>
        /// <returns></returns>
        private void show_list_info(object sender, MouseEventArgs e)
        {
            StudentList cur_list = lists[choose_list_index - 1];
            refresh();//清空显示栏
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < cur_list.taskIDs.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                int index = get_task_index(cur_list.taskIDs[i]);
                if (index != -1)
                {
                    lvi.Text = tasks[index].name;//对应文字
                }
                else
                {
                    lvi.Text = "没有任务";//对应文字
                }
                lvi.ImageIndex = 0;
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "task";
        }

        /// <summary>
        /// 课程：右侧label显示课程的信息
        /// </summary>
        /// <returns></returns>
        private void show_class_info(object sender, EventArgs e)
        {
            //清除已有内容
            right_display_panel.Controls.Clear();
            right_display_panel.CreateGraphics().Clear(Color.White);
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
            bool class_exists = false;
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中课程的index
            int index = indexes[0];
            //判断是否选了那门课
            for (int i = 0; i < classes.Count; i++)
            {
                if (classes[i].name == all_classes[index].name) { class_exists = true; break; }
            }
            if (!button.Enabled) { button.Enabled = true; }
            if (!class_exists)
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
            label.Text = "软件工程";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("微软雅黑", 11);
            label.Location = new Point(10, 40);
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

        /// <summary>
        /// 列表：右侧label显示清单的任务
        /// </summary>
        /// <returns></returns>
        private void show_task_info(object sender, EventArgs e)
        {
            //展示任务信息
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            //划分线1
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 20, this.right_display_panel.Width, 20);

            //label
            Label label = new Label();
            label.Name = "label1";
            label.Text = "任务名：软件工程大作业";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("微软雅黑", 11);
            label.Location = new Point(10, 40);
            label.Size = new Size(200, 20);
            this.right_display_panel.Controls.Add(label);

            //划分线2
            this.right_display_panel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, 80, this.right_display_panel.Width, 80);

            //任务介绍：label2
            Label label2 = new Label();
            label2.Name = "label2";
            label2.Text = "任务介绍：\r\n\r\n任务开始时间：2020.11.25\r\n\r\n任务到期时间：2021.01.24\r\n\r\n任务描述：软件工程大作业是一个非常有挑战的工作，";
            label2.Text += "它要求我们把软件工程课程学到的东西都融会贯通";
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
            classes.Add(all_classes[index]);
            //讲新课程中的所有task从all_tasks放入tasks
            for (int i = 0; i < all_classes[index].alltaskIDs.Count; i++)
            {
                int temp_index = get_all_task_index(all_classes[index].alltaskIDs[i]);
                Task temp_task = all_tasks[temp_index];
                //重新生成task放入tasks
                Task new_task = new Task();
                new_task.description = temp_task.description;
                new_task.due_time = temp_task.due_time;
                new_task.start_time = temp_task.start_time;
                new_task.parent_type = "class";
                new_task.name = temp_task.name;
                if (tasks.Count > 0)
                {
                    new_task.task_id = tasks[tasks.Count - 1].task_id + 1;
                }
                else { new_task.task_id = 0; }
                new_task.parent_id = all_classes[index].class_id;
                tasks.Add(new_task);
            }
            Button temp_button = (Button)sender;
            temp_button.Text = "删除课程";
            temp_button.Enabled = false;//不能再点

            this.class_slide.Size = new Size(this.class_slide.Size.Width, this.class_slide.Size.Height + this.add_list_button.Height);
            Button button = new Button();
            button.Name = "button" + classes.Count.ToString();
            //文字
            button.Text = all_classes[index].name;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = this.add_class_button.Font;
            button.ForeColor = this.add_class_button.ForeColor;
            //位置和大小
            button.Location = new Point(0, this.add_list_button.Location.Y + classes.Count * this.add_list_button.Height);
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
        private void del_new_class(object sender, MouseEventArgs e)
        {
            //删除已有的课程
            ListView.SelectedIndexCollection indexes = this.left_display_view.SelectedIndices;//选中课程的index
            int index = indexes[0];
            //清掉所有任务
            StudentClass cur_class = classes[get_class_index(all_classes[index].class_id)];
            for (int j = 0; j < cur_class.alltaskIDs.Count; j++)
            {
                int temp_index = get_all_task_index(cur_class.alltaskIDs[j]);
                if (temp_index != -1)
                {
                    tasks.RemoveAt(temp_index);
                }
            }
            classes.RemoveAt(get_class_index(all_classes[index].class_id));

            Button button1 = (Button)sender;
            button1.Text = "加入课程";

            this.class_slide.Controls.Clear();
            this.class_slide.Size = new Size(this.add_list_button.Size.Width, class_slide.Size.Height - this.add_list_button.Height);
            this.add_class_button.Location = new Point(0, 0);
            this.class_slide.Controls.Add(this.add_class_button);

            //放入其他组件
            for (int i = 0; i < classes.Count; i++)
            {
                Button button = new Button();
                button.Name = "button" + (i + 1).ToString();
                //文字
                button.Text = classes[i].name;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Font = this.add_class_button.Font;
                button.ForeColor = this.add_class_button.ForeColor;
                //位置和大小
                button.Location = new Point(0, (i + 1) * this.add_class_button.Height);
                button.Size = this.add_list_button.Size;
                //背景
                button.BackColor = this.add_class_button.BackColor;
                //按钮边缘
                button.FlatAppearance.BorderSize = 0;
                button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.show_class_tasks);
                button.MouseEnter += new System.EventHandler(this.classbuttonEnter);
                this.class_slide.Controls.Add(button);
            }
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
            //删除对应的list
            //删除对应的task
            StudentList cur_list = lists[choose_list_index - 1];
            for (int j = 0; j < cur_list.taskIDs.Count; j++)
            {
                int index = get_task_index(cur_list.taskIDs[j]);
                if (index != -1)
                {
                    tasks.RemoveAt(index);
                }
            }
            this.file_slide.Controls.Clear();
            this.file_slide.Size = new Size(this.add_list_button.Size.Width, file_slide.Size.Height - this.add_list_button.Height);
            list_num -= 1;
            this.add_list_button.Location = new Point(0, this.add_list_button.Location.Y - this.add_list_button.Height);
            this.file_slide.Controls.Add(this.add_list_button);
            lists.RemoveAt(choose_list_index - 1);

            //放入其他组件
            for (int i = 0; i < lists.Count; i++)
            {
                Button button = new Button();
                button.Name = "button" + (i + 1).ToString();
                //文字
                button.Text = lists[i].name;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Font = this.add_class_button.Font;
                button.ForeColor = this.add_class_button.ForeColor;
                //位置和大小
                button.Location = new Point(0, i * 50);
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
            DateTime temp_time;
            //DateTime.TryParse(temp_str[1], out temp_time);//这对时间字符串有要求
            //new_task.due_time = temp_time;
            new_task.due_time = DateTime.Now;
            new_task.description = temp_str[2];
            new_task.parent_type = "list";
            new_task.parent_id = lists[choose_list_index - 1].list_id;

            if (tasks.Count > 0)
            { new_task.task_id = tasks[tasks.Count - 1].task_id + 1; }
            else
            {
                new_task.task_id = 0;
            }
            tasks.Add(new_task);
            lists[choose_list_index - 1].taskIDs.Add(new_task.task_id);//目前所在的清单加入新的编号
            StudentList cur_list = lists[choose_list_index - 1];//当前列表

            //点击加入课程
            refresh();//清除原有内容
            this.left_display_view.View = View.List;

            this.left_display_view.SmallImageList = this.color_imageList;
            this.color_imageList.ImageSize = new Size(10, 30); // 这实际上是图片的占位，可能导致图片无法显示
            this.left_display_view.BeginUpdate();

            for (int i = 0; i < cur_list.taskIDs.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                int index = get_task_index(cur_list.taskIDs[i]);
                if (index != -1)
                {
                    lvi.Text = tasks[index].name;//对应文字
                }
                else
                {
                    lvi.Text = "不存在的任务";//对应文字
                }
                lvi.ImageIndex = 0;
                this.left_display_view.Items.Add(lvi);
            }

            this.left_display_view.EndUpdate();
            //更改显示内容
            this.left_content = "task";

            //传输写入数据库
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
            //创建新的列表
            list_num += 1;
            this.file_slide.Size = new Size(file_slide.Width, add_list_button.Size.Height * (list_num + 1));

            //创建新的StudentList
            StudentList new_list = new StudentList();
            new_list.name = temp;
            if (lists.Count != 0)
            {
                new_list.list_id = lists[lists.Count - 1].list_id + 1;
            }
            else
            {
                new_list.list_id = 1;
            }
            lists.Add(new_list);

            Button button = new Button();
            button.Name = "button" + lists.Count.ToString();
            //文字
            button.Text = temp; temp = "";
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = this.add_class_button.Font;
            button.ForeColor = this.add_class_button.ForeColor;
            //位置和大小
            button.Location = new Point(0, this.add_list_button.Location.Y);
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
            //ContextMenuStrip cms = new ContextMenuStrip();//创建菜单对象
            //ToolStripMenuItem tsmi = new ToolStripMenuItem("删除");
            //tsmi.Click += delete_list_Clickevent;//指定单击事件要执行额方法
            //cms.Items.Add(tsmi);//将子菜单与主菜单绑定
            //button.ContextMenuStrip = cms;//将主菜单与控件绑定


            this.file_slide.Controls.Add(button);

            //更新+号的位置
            this.add_list_button.Location = new Point(0, add_list_button.Location.Y + add_list_button.Size.Height);

            //传输写入数据库

        }
        #endregion

        #region 根据ID查找数据
        private int get_task_index(int task_id)
        {
            //通过task_id找到在tasks中的ID
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].task_id == task_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回task_id
            return -1;
        }
        private int get_list_index(int list_id)
        {
            //通过list_id找到在lists中的ID
            for (int i = 0; i < lists.Count; i++)
            {
                if (lists[i].list_id == list_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回list_id
            return -1;
        }
        private int get_class_index(int class_id)
        {
            //通过class_id找到在classes中的ID
            for (int i = 0; i < classes.Count; i++)
            {
                if (classes[i].class_id == class_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回class_id
            return -1;
        }
        private int get_all_task_index(int task_id)
        {
            //通过task_id找到在all_tasks中的ID
            for (int i = 0; i < all_tasks.Count; i++)
            {
                if (all_tasks[i].task_id == task_id)
                {
                    return i;
                }
            }
            //如果没有找到，task_id
            return -1;
        }
        #endregion

        #region 辅助函数
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
            //拿到addlist传来的文本，强转数据类型
            TextBoxMsgChangeEventArg arg = e as TextBoxMsgChangeEventArg;
            this.temp_task_str = arg.Text;//交给公共变量
        }

        public void listbuttonEnter(object sender, EventArgs e)
        {
            //为了确定选中的是谁
            Button btn = (Button)sender;
            string name = btn.Name.Substring(6);
            choose_list_index = Convert.ToInt32(name);
        }

        public void classbuttonEnter(object sender, EventArgs e)
        {
            //为了确定选中的是谁
            Button btn = (Button)sender;
            string name = btn.Name.Substring(6);
            choose_list_index2 = Convert.ToInt32(name);
        }
        
        private void refresh_button_MouseClick(object sender, MouseEventArgs e)
        {
            refresh();
        }
        private void refresh()
        {
            //刷新左右两个显示栏
            //清除原有内容
            this.left_display_view.Clear();
            this.right_display_panel.Controls.Clear();
            this.right_display_panel.CreateGraphics().Clear(Color.White);
            //导入新内容
        }


        private void left_display_view_MouseClick(object sender, MouseEventArgs e)
        {
            //禁止多选
            this.left_display_view.MultiSelect = false;
            //鼠标右键
            if (e.Button == MouseButtons.Right)
            {
                //选中列表中数据才显示 空白处不显示
                String itemName = this.left_display_view.SelectedItems[0].Text; //获取选中课程名
                Point p = new Point(e.X, e.Y);
                contextMenuStrip1.Show(this.left_display_view, p);
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
            //点击进入管理员界面
            if(myuser.administrator_list.Count!=0)
            {
                AdministratorForm admin_form = new AdministratorForm();
                this.Hide();
                admin_form.ShowDialog();
                Application.ExitThread();
            }
        }
        #endregion
    }
}

//目前没有完成的东西：
//1.对于管理员，如何增加课程任务
//2.班级权限的问题

/// <summary>
/// 列表：单击左侧表项触发的事件，在右边的显示栏展示列表中的task的内容
/// </summary>
/// <returns></returns>