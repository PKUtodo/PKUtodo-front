namespace TODO
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.left_panel = new System.Windows.Forms.Panel();
            this.purple_button = new System.Windows.Forms.Button();
            this.blue_button = new System.Windows.Forms.Button();
            this.grey_button = new System.Windows.Forms.Button();
            this.yellow_button = new System.Windows.Forms.Button();
            this.green_button = new System.Windows.Forms.Button();
            this.red_button = new System.Windows.Forms.Button();
            this.refresh_button = new System.Windows.Forms.Button();
            this.logout_button = new System.Windows.Forms.Button();
            this.menu_label = new System.Windows.Forms.Label();
            this.menu_panel = new System.Windows.Forms.Panel();
            this.class_button = new System.Windows.Forms.Button();
            this.all_task_button = new System.Windows.Forms.Button();
            this.foot_button = new System.Windows.Forms.Button();
            this.collection_button = new System.Windows.Forms.Button();
            this.slide_panel = new System.Windows.Forms.Panel();
            this.person_list_button = new System.Windows.Forms.Button();
            this.title_label = new System.Windows.Forms.Label();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.file_slide = new System.Windows.Forms.Panel();
            this.add_list_button = new System.Windows.Forms.Button();
            this.right_display_panel = new System.Windows.Forms.Panel();
            this.search_label = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.class_slide = new System.Windows.Forms.Panel();
            this.add_class_button = new System.Windows.Forms.Button();
            this.left_display_view = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delete_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.color_imageList = new System.Windows.Forms.ImageList(this.components);
            this.list_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.list_delete_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.add_task_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.left_panel.SuspendLayout();
            this.menu_panel.SuspendLayout();
            this.file_slide.SuspendLayout();
            this.class_slide.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.list_contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // left_panel
            // 
            this.left_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(55)))), ((int)(((byte)(67)))));
            this.left_panel.Controls.Add(this.purple_button);
            this.left_panel.Controls.Add(this.blue_button);
            this.left_panel.Controls.Add(this.grey_button);
            this.left_panel.Controls.Add(this.yellow_button);
            this.left_panel.Controls.Add(this.green_button);
            this.left_panel.Controls.Add(this.red_button);
            this.left_panel.Controls.Add(this.refresh_button);
            this.left_panel.Controls.Add(this.logout_button);
            this.left_panel.Controls.Add(this.menu_label);
            this.left_panel.Controls.Add(this.menu_panel);
            this.left_panel.Controls.Add(this.title_label);
            this.left_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.left_panel.Location = new System.Drawing.Point(0, 0);
            this.left_panel.Margin = new System.Windows.Forms.Padding(1);
            this.left_panel.Name = "left_panel";
            this.left_panel.Size = new System.Drawing.Size(229, 652);
            this.left_panel.TabIndex = 5;
            this.left_panel.MouseEnter += new System.EventHandler(this.file_slide_MouseLeave);
            // 
            // purple_button
            // 
            this.purple_button.Image = ((System.Drawing.Image)(resources.GetObject("purple_button.Image")));
            this.purple_button.Location = new System.Drawing.Point(170, 419);
            this.purple_button.Name = "purple_button";
            this.purple_button.Size = new System.Drawing.Size(27, 27);
            this.purple_button.TabIndex = 17;
            this.purple_button.UseVisualStyleBackColor = true;
            this.purple_button.Visible = false;
            // 
            // blue_button
            // 
            this.blue_button.Image = ((System.Drawing.Image)(resources.GetObject("blue_button.Image")));
            this.blue_button.Location = new System.Drawing.Point(137, 419);
            this.blue_button.Name = "blue_button";
            this.blue_button.Size = new System.Drawing.Size(27, 27);
            this.blue_button.TabIndex = 16;
            this.blue_button.UseVisualStyleBackColor = true;
            this.blue_button.Visible = false;
            // 
            // grey_button
            // 
            this.grey_button.Image = ((System.Drawing.Image)(resources.GetObject("grey_button.Image")));
            this.grey_button.Location = new System.Drawing.Point(104, 419);
            this.grey_button.Name = "grey_button";
            this.grey_button.Size = new System.Drawing.Size(27, 27);
            this.grey_button.TabIndex = 15;
            this.grey_button.UseVisualStyleBackColor = true;
            this.grey_button.Visible = false;
            // 
            // yellow_button
            // 
            this.yellow_button.Image = ((System.Drawing.Image)(resources.GetObject("yellow_button.Image")));
            this.yellow_button.Location = new System.Drawing.Point(71, 419);
            this.yellow_button.Name = "yellow_button";
            this.yellow_button.Size = new System.Drawing.Size(27, 27);
            this.yellow_button.TabIndex = 14;
            this.yellow_button.UseVisualStyleBackColor = true;
            this.yellow_button.Visible = false;
            // 
            // green_button
            // 
            this.green_button.Image = ((System.Drawing.Image)(resources.GetObject("green_button.Image")));
            this.green_button.Location = new System.Drawing.Point(37, 419);
            this.green_button.Name = "green_button";
            this.green_button.Size = new System.Drawing.Size(27, 27);
            this.green_button.TabIndex = 13;
            this.green_button.UseVisualStyleBackColor = true;
            this.green_button.Visible = false;
            // 
            // red_button
            // 
            this.red_button.Image = ((System.Drawing.Image)(resources.GetObject("red_button.Image")));
            this.red_button.Location = new System.Drawing.Point(4, 419);
            this.red_button.Name = "red_button";
            this.red_button.Size = new System.Drawing.Size(27, 27);
            this.red_button.TabIndex = 12;
            this.red_button.UseVisualStyleBackColor = true;
            this.red_button.Visible = false;
            // 
            // refresh_button
            // 
            this.refresh_button.FlatAppearance.BorderSize = 0;
            this.refresh_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.refresh_button.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.refresh_button.Location = new System.Drawing.Point(128, 570);
            this.refresh_button.Margin = new System.Windows.Forms.Padding(4);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(56, 56);
            this.refresh_button.TabIndex = 11;
            this.refresh_button.Text = "刷新";
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.refresh_button_MouseClick);
            // 
            // logout_button
            // 
            this.logout_button.FlatAppearance.BorderSize = 0;
            this.logout_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logout_button.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.logout_button.Location = new System.Drawing.Point(42, 570);
            this.logout_button.Margin = new System.Windows.Forms.Padding(4);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(56, 56);
            this.logout_button.TabIndex = 10;
            this.logout_button.Text = "注销";
            this.logout_button.UseVisualStyleBackColor = true;
            // 
            // menu_label
            // 
            this.menu_label.AutoSize = true;
            this.menu_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.menu_label.Location = new System.Drawing.Point(15, 116);
            this.menu_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.menu_label.Name = "menu_label";
            this.menu_label.Size = new System.Drawing.Size(70, 25);
            this.menu_label.TabIndex = 8;
            this.menu_label.Text = "MENU";
            // 
            // menu_panel
            // 
            this.menu_panel.Controls.Add(this.class_button);
            this.menu_panel.Controls.Add(this.all_task_button);
            this.menu_panel.Controls.Add(this.foot_button);
            this.menu_panel.Controls.Add(this.collection_button);
            this.menu_panel.Controls.Add(this.slide_panel);
            this.menu_panel.Controls.Add(this.person_list_button);
            this.menu_panel.Location = new System.Drawing.Point(0, 162);
            this.menu_panel.Margin = new System.Windows.Forms.Padding(1);
            this.menu_panel.Name = "menu_panel";
            this.menu_panel.Size = new System.Drawing.Size(226, 257);
            this.menu_panel.TabIndex = 6;
            // 
            // class_button
            // 
            this.class_button.FlatAppearance.BorderSize = 0;
            this.class_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(71)))), ((int)(((byte)(82)))));
            this.class_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.class_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.class_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.class_button.Image = ((System.Drawing.Image)(resources.GetObject("class_button.Image")));
            this.class_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.class_button.Location = new System.Drawing.Point(8, 207);
            this.class_button.Margin = new System.Windows.Forms.Padding(4);
            this.class_button.Name = "class_button";
            this.class_button.Size = new System.Drawing.Size(219, 50);
            this.class_button.TabIndex = 11;
            this.class_button.Text = "课程";
            this.class_button.UseVisualStyleBackColor = true;
            this.class_button.MouseEnter += new System.EventHandler(this.class_button_MouseEnter);
            // 
            // all_task_button
            // 
            this.all_task_button.FlatAppearance.BorderSize = 0;
            this.all_task_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(71)))), ((int)(((byte)(82)))));
            this.all_task_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.all_task_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.all_task_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.all_task_button.Image = ((System.Drawing.Image)(resources.GetObject("all_task_button.Image")));
            this.all_task_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.all_task_button.Location = new System.Drawing.Point(8, 150);
            this.all_task_button.Margin = new System.Windows.Forms.Padding(4);
            this.all_task_button.Name = "all_task_button";
            this.all_task_button.Size = new System.Drawing.Size(219, 50);
            this.all_task_button.TabIndex = 10;
            this.all_task_button.Text = "所有任务";
            this.all_task_button.UseVisualStyleBackColor = true;
            this.all_task_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.all_task_button_MouseClick);
            this.all_task_button.MouseEnter += new System.EventHandler(this.all_task_button_MouseEnter);
            // 
            // foot_button
            // 
            this.foot_button.FlatAppearance.BorderSize = 0;
            this.foot_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(71)))), ((int)(((byte)(82)))));
            this.foot_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.foot_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foot_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.foot_button.Image = ((System.Drawing.Image)(resources.GetObject("foot_button.Image")));
            this.foot_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.foot_button.Location = new System.Drawing.Point(8, 100);
            this.foot_button.Margin = new System.Windows.Forms.Padding(4);
            this.foot_button.Name = "foot_button";
            this.foot_button.Size = new System.Drawing.Size(219, 50);
            this.foot_button.TabIndex = 9;
            this.foot_button.Text = "我的足迹";
            this.foot_button.UseVisualStyleBackColor = true;
            this.foot_button.MouseEnter += new System.EventHandler(this.foot_button_MouseEnter);
            // 
            // collection_button
            // 
            this.collection_button.FlatAppearance.BorderSize = 0;
            this.collection_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(71)))), ((int)(((byte)(82)))));
            this.collection_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.collection_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collection_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.collection_button.Image = ((System.Drawing.Image)(resources.GetObject("collection_button.Image")));
            this.collection_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.collection_button.Location = new System.Drawing.Point(8, 50);
            this.collection_button.Margin = new System.Windows.Forms.Padding(4);
            this.collection_button.Name = "collection_button";
            this.collection_button.Size = new System.Drawing.Size(219, 50);
            this.collection_button.TabIndex = 8;
            this.collection_button.Text = "我的收藏";
            this.collection_button.UseVisualStyleBackColor = true;
            this.collection_button.MouseEnter += new System.EventHandler(this.collection_button_MouseEnter);
            // 
            // slide_panel
            // 
            this.slide_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(231)))), ((int)(((byte)(229)))));
            this.slide_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.slide_panel.Location = new System.Drawing.Point(0, 0);
            this.slide_panel.Margin = new System.Windows.Forms.Padding(4);
            this.slide_panel.Name = "slide_panel";
            this.slide_panel.Size = new System.Drawing.Size(8, 50);
            this.slide_panel.TabIndex = 7;
            // 
            // person_list_button
            // 
            this.person_list_button.FlatAppearance.BorderSize = 0;
            this.person_list_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(71)))), ((int)(((byte)(82)))));
            this.person_list_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.person_list_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.person_list_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.person_list_button.Image = ((System.Drawing.Image)(resources.GetObject("person_list_button.Image")));
            this.person_list_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.person_list_button.Location = new System.Drawing.Point(8, 0);
            this.person_list_button.Margin = new System.Windows.Forms.Padding(4);
            this.person_list_button.Name = "person_list_button";
            this.person_list_button.Size = new System.Drawing.Size(219, 50);
            this.person_list_button.TabIndex = 0;
            this.person_list_button.Text = "个人列表";
            this.person_list_button.UseVisualStyleBackColor = true;
            this.person_list_button.MouseEnter += new System.EventHandler(this.File_Button_MouseEnter);
            // 
            // title_label
            // 
            this.title_label.BackColor = System.Drawing.Color.Transparent;
            this.title_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.title_label.Location = new System.Drawing.Point(-5, 9);
            this.title_label.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(224, 105);
            this.title_label.TabIndex = 5;
            this.title_label.Text = "    PKU \r\n         To Do \r\n                List";
            this.title_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // Exit_Button
            // 
            this.Exit_Button.BackColor = System.Drawing.Color.Silver;
            this.Exit_Button.Cursor = System.Windows.Forms.Cursors.Default;
            this.Exit_Button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Exit_Button.FlatAppearance.BorderSize = 0;
            this.Exit_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.Exit_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit_Button.ForeColor = System.Drawing.Color.Black;
            this.Exit_Button.Image = ((System.Drawing.Image)(resources.GetObject("Exit_Button.Image")));
            this.Exit_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Exit_Button.Location = new System.Drawing.Point(928, 12);
            this.Exit_Button.Margin = new System.Windows.Forms.Padding(1);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(80, 46);
            this.Exit_Button.TabIndex = 6;
            this.Exit_Button.Text = "Exit";
            this.Exit_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Exit_Button.UseVisualStyleBackColor = false;
            this.Exit_Button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Exit_Button_MouseClick);
            // 
            // file_slide
            // 
            this.file_slide.BackColor = System.Drawing.Color.Transparent;
            this.file_slide.Controls.Add(this.add_list_button);
            this.file_slide.Location = new System.Drawing.Point(226, 162);
            this.file_slide.Margin = new System.Windows.Forms.Padding(4);
            this.file_slide.Name = "file_slide";
            this.file_slide.Size = new System.Drawing.Size(206, 50);
            this.file_slide.TabIndex = 7;
            this.file_slide.Visible = false;
            this.file_slide.MouseLeave += new System.EventHandler(this.file_slide_MouseLeave);
            // 
            // add_list_button
            // 
            this.add_list_button.BackColor = System.Drawing.Color.CadetBlue;
            this.add_list_button.FlatAppearance.BorderSize = 0;
            this.add_list_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.add_list_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_list_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_list_button.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.add_list_button.Image = ((System.Drawing.Image)(resources.GetObject("add_list_button.Image")));
            this.add_list_button.Location = new System.Drawing.Point(-2, 0);
            this.add_list_button.Margin = new System.Windows.Forms.Padding(4);
            this.add_list_button.Name = "add_list_button";
            this.add_list_button.Size = new System.Drawing.Size(208, 50);
            this.add_list_button.TabIndex = 3;
            this.add_list_button.UseVisualStyleBackColor = false;
            this.add_list_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.add_list_button_MouseClick);
            // 
            // right_display_panel
            // 
            this.right_display_panel.AutoScroll = true;
            this.right_display_panel.BackColor = System.Drawing.Color.White;
            this.right_display_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.right_display_panel.Location = new System.Drawing.Point(648, 77);
            this.right_display_panel.Name = "right_display_panel";
            this.right_display_panel.Size = new System.Drawing.Size(360, 549);
            this.right_display_panel.TabIndex = 9;
            // 
            // search_label
            // 
            this.search_label.AutoSize = true;
            this.search_label.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.search_label.Location = new System.Drawing.Point(677, 23);
            this.search_label.Name = "search_label";
            this.search_label.Size = new System.Drawing.Size(79, 25);
            this.search_label.TabIndex = 10;
            this.search_label.Text = "Search:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.textBox1.Location = new System.Drawing.Point(770, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 32);
            this.textBox1.TabIndex = 11;
            // 
            // class_slide
            // 
            this.class_slide.BackColor = System.Drawing.Color.Transparent;
            this.class_slide.Controls.Add(this.add_class_button);
            this.class_slide.Location = new System.Drawing.Point(228, 369);
            this.class_slide.Margin = new System.Windows.Forms.Padding(0);
            this.class_slide.Name = "class_slide";
            this.class_slide.Size = new System.Drawing.Size(206, 50);
            this.class_slide.TabIndex = 8;
            this.class_slide.Visible = false;
            this.class_slide.MouseLeave += new System.EventHandler(this.class_slide_MouseLeave);
            // 
            // add_class_button
            // 
            this.add_class_button.BackColor = System.Drawing.Color.CadetBlue;
            this.add_class_button.FlatAppearance.BorderSize = 0;
            this.add_class_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.add_class_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_class_button.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_class_button.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.add_class_button.Location = new System.Drawing.Point(0, 0);
            this.add_class_button.Margin = new System.Windows.Forms.Padding(0);
            this.add_class_button.Name = "add_class_button";
            this.add_class_button.Size = new System.Drawing.Size(208, 50);
            this.add_class_button.TabIndex = 0;
            this.add_class_button.Text = "加入课程";
            this.add_class_button.UseVisualStyleBackColor = false;
            this.add_class_button.Click += new System.EventHandler(this.add_class_button_Click);
            // 
            // left_display_view
            // 
            this.left_display_view.ContextMenuStrip = this.contextMenuStrip1;
            this.left_display_view.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.left_display_view.HideSelection = false;
            this.left_display_view.Location = new System.Drawing.Point(266, 77);
            this.left_display_view.Name = "left_display_view";
            this.left_display_view.Size = new System.Drawing.Size(364, 549);
            this.left_display_view.TabIndex = 12;
            this.left_display_view.UseCompatibleStateImageBehavior = false;
            this.left_display_view.SelectedIndexChanged += new System.EventHandler(this.left_display_view_SelectedIndexChanged);
            this.left_display_view.MouseClick += new System.Windows.Forms.MouseEventHandler(this.left_display_view_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delete_menu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // delete_menu
            // 
            this.delete_menu.Name = "delete_menu";
            this.delete_menu.Size = new System.Drawing.Size(108, 24);
            this.delete_menu.Text = "删除";
            this.delete_menu.Click += new System.EventHandler(this.delete_menu_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // color_imageList
            // 
            this.color_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("color_imageList.ImageStream")));
            this.color_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.color_imageList.Images.SetKeyName(0, "purple_circle_20px.png");
            this.color_imageList.Images.SetKeyName(1, "red_circle_20px.png");
            this.color_imageList.Images.SetKeyName(2, "blue_circle_20px.png");
            this.color_imageList.Images.SetKeyName(3, "green_circle_20px.png");
            this.color_imageList.Images.SetKeyName(4, "grey_circle_20px.png");
            this.color_imageList.Images.SetKeyName(5, "yellow_circle_20px.png");
            // 
            // list_contextMenuStrip
            // 
            this.list_contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.list_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.list_delete_menu,
            this.add_task_menu});
            this.list_contextMenuStrip.Name = "list_contextMenuStrip";
            this.list_contextMenuStrip.Size = new System.Drawing.Size(152, 60);
            // 
            // list_delete_menu
            // 
            this.list_delete_menu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.list_delete_menu.Name = "list_delete_menu";
            this.list_delete_menu.Size = new System.Drawing.Size(151, 28);
            this.list_delete_menu.Text = "删除";
            this.list_delete_menu.Click += new System.EventHandler(this.list_delete_menu_Click);
            // 
            // add_task_menu
            // 
            this.add_task_menu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.add_task_menu.Name = "add_task_menu";
            this.add_task_menu.Size = new System.Drawing.Size(151, 28);
            this.add_task_menu.Text = "添加任务";
            this.add_task_menu.Click += new System.EventHandler(this.add_task_menu_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1036, 652);
            this.Controls.Add(this.class_slide);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.search_label);
            this.Controls.Add(this.right_display_panel);
            this.Controls.Add(this.file_slide);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.left_panel);
            this.Controls.Add(this.left_display_view);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseEnter += new System.EventHandler(this.file_slide_MouseLeave);
            this.left_panel.ResumeLayout(false);
            this.left_panel.PerformLayout();
            this.menu_panel.ResumeLayout(false);
            this.file_slide.ResumeLayout(false);
            this.class_slide.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.list_contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel left_panel;
        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.Button Exit_Button;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Panel menu_panel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        //private WindowsFormsControlLibrary1.aaa aaa1;
        private System.Windows.Forms.Panel file_slide;
        private System.Windows.Forms.Button person_list_button;
        private System.Windows.Forms.Panel slide_panel;
        private System.Windows.Forms.Button all_task_button;
        private System.Windows.Forms.Button foot_button;
        private System.Windows.Forms.Button collection_button;
        private System.Windows.Forms.Label menu_label;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.Button logout_button;
        private System.Windows.Forms.Button add_list_button;
        private System.Windows.Forms.Button class_button;
        private System.Windows.Forms.Panel right_display_panel;
        private System.Windows.Forms.Label search_label;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel class_slide;
        private System.Windows.Forms.Button add_class_button;
        private System.Windows.Forms.Button red_button;
        private System.Windows.Forms.Button green_button;
        private System.Windows.Forms.Button yellow_button;
        private System.Windows.Forms.Button grey_button;
        private System.Windows.Forms.Button blue_button;
        private System.Windows.Forms.Button purple_button;
        private System.Windows.Forms.ListView left_display_view;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList color_imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem delete_menu;
        private System.Windows.Forms.ContextMenuStrip list_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem list_delete_menu;
        private System.Windows.Forms.ToolStripMenuItem add_task_menu;
    }
}

