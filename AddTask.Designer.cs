namespace TODO
{
    partial class AddTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label1.Location = new System.Drawing.Point(114, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label2.Location = new System.Drawing.Point(114, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "截止时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label3.Location = new System.Drawing.Point(114, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "任务描述";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.textBox1.Location = new System.Drawing.Point(119, 99);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(348, 32);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.textBox2.Location = new System.Drawing.Point(119, 192);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(348, 32);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.textBox3.Location = new System.Drawing.Point(119, 293);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(348, 203);
            this.textBox3.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button1.Location = new System.Drawing.Point(119, 527);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 44);
            this.button1.TabIndex = 6;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 613);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddTask";
            this.Text = "AddTask";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
    }
}