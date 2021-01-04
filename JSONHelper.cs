using Newtonsoft.Json.Linq;
using System;

namespace TODO
{
    public class JSONHelper
    {
        public static string CreateJson(string type, string email)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, int user_id, int password)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, string email, string password)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "password", password }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJsonDelList(string type, int user_id, int password, int id)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "list_id", id }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJsonDelTask(string type, int user_id, int password, int id)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "task_id", id }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, int user_id, int password, string list_name)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "list_name", list_name }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, int user_id, int password, DateTime date)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "DateTime", date.ToString() }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, string email, int veri_code, string user_name, string password)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "email", email },
                { "veri_code", veri_code },
                { "user_name", user_name },
                { "password", password }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, int user_id, int password, int list_id, bool state)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "list_id", list_id },
                { "state", state }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, int user_id, int password, int list_id, int target_user_id)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "list_id", list_id },
                { "target_user_id", target_user_id }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, int user_id, int password, int task_id, DateTime creat_date, DateTime due_date, double position_x, double position_y, string content)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "task_id", task_id },
                { "creat_date", creat_date.ToString() },
                { "due_date", due_date.ToString() },
                { "position_x", position_x },
                { "position_y", position_y },
                { "content", content }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string type, int user_id, int password, int list_id, string task_name, DateTime creat_date, DateTime due_date, double position_x, double position_y, string content)
        {
            JObject obj = new JObject
            {
                { "type", type },
                { "user_id", user_id },
                { "password", password },
                { "task_id", list_id },
                { "task_name", task_name },
                { "creat_date", creat_date.ToString() },
                { "due_date", due_date.ToString() },
                { "position_x", position_x },
                { "position_y", position_y },
                { "content", content }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
    }
}

