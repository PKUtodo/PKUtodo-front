using System;
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
    public partial class TransferForm : Form
    {
        public event EventHandler AfterMsgChange;//事件句柄
        public List<UserData> users=new List<UserData>();//users是所有选课的人
        public int myid;//管理员的ID
        public TransferForm()
        {
            InitializeComponent();
            show_users();//在view中展示所有用户
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //点击发送给上层页面
            int index;
            ListView.SelectedIndexCollection indexes = this.view.SelectedIndices;//选中的index
            if ((indexes.Count > 0)&&(indexes[0]>=0))
            {
                index = indexes[0];
                if(users[index].user_id!=myid)
                {
                    string str = index.ToString();
                    AfterMsgChange(this, new TextBoxMsgChangeEventArg() { Text = str });
                    this.Close();
                }
            }
        }
        public void show_users()
        {
            //在view中展示所有用户
            this.view.View = View.List;
            this.view.BeginUpdate();

            for (int i = 0; i < users.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = users[i].name;//对应文字
                this.view.Items.Add(lvi);
            }

            this.view.EndUpdate();
        }
    }
}
