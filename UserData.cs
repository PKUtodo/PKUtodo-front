
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO
{
    /// <summary>
    /// 用户类：保存了用户个人信息和权限设置
    /// </summary>
    /// <returns></returns>
    public class UserData
    {
        public int user_id;
        public string password;
        public string veri_code;
        public string email;

        public List<StudentList> lists = new List<StudentList>();//所有的表单
        public List<StudentClass> classes = new List<StudentClass>();//所有个人的课程
        public List<Task> tasks = new List<Task>();//所有的私人任务和选课的公有任务（拷贝到本地）

        //表示是哪些课程的管理员，这个值等于课程号，列表为空表示不是管理员
        public List<int> administrator_list = new List<int>();

        //判断是否为class_id这门课的管理员
        public bool isAdministrator(int class_id)
        {
            if (administrator_list.Count == 0)
            {
                return false;
            }
            if (administrator_list.Contains(class_id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //成为class_id这门课的管理员，一门课可以有多个管理员
        public void becomeAdministrator(int class_id)
        {
            if (!administrator_list.Contains(class_id))
            {
                administrator_list.Add(class_id);
            }
        }
        //转让管理员身份给另一个人
        public bool tranferAdministrator(UserData other, int class_id)
        {
            if ((!administrator_list.Contains(class_id)) || (administrator_list.Count == 0))
            {
                return false;//转让失败
            }
            int index = 0;
            for (int i = 0; i < administrator_list.Count; i++)
            {
                if (administrator_list[i] == class_id)
                {
                    index = i; break;
                }
            }
            administrator_list.RemoveAt(index);
            if (!other.administrator_list.Contains(class_id))
            {
                other.administrator_list.Add(class_id);
            }
            return true;
        }
    }

    public class MessageType
    {
        static public string set_up = "set_up";
        static public string verify = "verify";
        static public string login = "login";
        static public string refresh = "refresh";

        static public string finish = "finish";
        static public string find_list = "find_list";
        static public string handle = "handle";
        static public string assignment = "assignment";
        static public string find_member = "find_member";
        static public string transfer = "transfer";
        //添加课程和选课
        static public string join_class = "join";
        static public string quit_class = "quit_class";
        //增删清单
        static public string add_list = "add_list";
        static public string del_list = "del_list";
        //增删任务
        static public string add_task = "add_task";
        static public string del_task = "del_task";
        //管理员功能

    }
}
