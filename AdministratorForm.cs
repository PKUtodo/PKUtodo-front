﻿using System;
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
    public partial class AdministratorForm : Form
    {
        public UserData user;//当前用户

        public AdministratorForm(int index)//index表示在对应用户管理的课程中的顺序
        {
            InitializeComponent();
        }
    }
}
