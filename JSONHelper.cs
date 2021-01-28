using Newtonsoft.Json.Linq;
using System;

namespace TODO
{
    public class JSONHelper
    {
        //发送验证码
        public static string CreateJson(string type, string email)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email }
            };
            return obj.ToString();
        }
        //
        public static string CreateJson(string type, int user_id, string password)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password }
            };
            return obj.ToString();
        }
        //登录
        public static string CreateJson(string type, string email, string password)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "password", password }
            };
            return obj.ToString();
        }
        //刷新
        public static string CreateJson(string type, string email, int user_id, string password)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password }
            };
            return obj.ToString();
        }
        //加入班级
        public static string CreateJson(string type, string email, int user_id, string password, int class_id)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "list_id", class_id }
            };
            return obj.ToString();
        }
        //删除列表
        public static string CreateJsonDelList(string type, string email,int user_id, string password, int id)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "list_id", id }
            };
            return obj.ToString();
        }
        //删除Task
        public static string CreateJsonDelTask(string type, string email,int user_id, string password, int id)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "task_id", id }
            };
            return obj.ToString();
        }
        //创建list
        public static string CreateJson(string type, string email, int user_id, string password, string list_name)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "list_name", list_name }
            };
            return obj.ToString();
        }

        public static string CreateJson(string type, string email,int user_id, string password, DateTime date)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "DateTime", date.ToString() }
            };
            return obj.ToString();
        }

        public static string CreateJsonSetup(string type, string email, int veri_code, string user_name, string password)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "verify_code", veri_code },
                { "name", user_name },
                { "password", password }
            };
            return obj.ToString();
        }
        public static string CreateJson(string type, string email,int user_id, string password, int list_id, bool state)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "list_id", list_id },
                { "state", state }
            };
            return obj.ToString();
        }
        public static string CreateJson(string type, string email,int user_id, string password, int list_id, int target_user_id)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "list_id", list_id },
                { "target_user_id", target_user_id }
            };
            return obj.ToString();
        }
        public static string CreateJson(string type, string email,int user_id, string password, int task_id, DateTime create_date, DateTime due_date, string content, double position_x=0, double position_y=0)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "task_id", task_id },
                { "create_date", create_date.ToString("yyyyMMdd HH:mm:ss") },
                { "due_date", due_date.ToString("yyyyMMdd HH:mm:ss") },
                { "position_x", position_x },
                { "position_y", position_y },
                { "content", content }
            };
            return obj.ToString();
        }
        public static string CreateJson(string type, string email,int user_id, string password, int list_id, string task_name, DateTime create_date, DateTime due_date, string content, double position_x=0, double position_y=0)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "user_id", user_id },
                { "password", password },
                { "list_id", list_id },
                { "task_name", task_name },
                { "create_date", create_date.ToString("yyyyMMdd HH:mm:ss") },
                { "due_date", due_date.ToString("yyyyMMdd HH:mm:ss")},
                { "position_x", position_x },
                { "position_y", position_y },
                { "content", content }
            };
            return obj.ToString();
        }
    }
}