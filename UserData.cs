
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO
{
    public class UserData
    {
        public static int user_id;
        public static string password;
        public static List<int> list_ids;
        public static List<int> task_ids;
        public static string veri_code;
    }

    public class MessageType
    {
        static public string set_up = "set_up";
        static public string verify = "verify";
        static public string login = "login";
        static public string refresh = "refresh";
        static public string add_task = "add_task";
        static public string del_task = "del_task";
        static public string del_list = "del_list";
        static public string finish = "finish";
        static public string find_list = "find_list";
        static public string join = "join";
        static public string handle = "handle";
        static public string assignment = "assignment";
        static public string find_member = "find_member";
        static public string transfer = "transfer";
    }
}
