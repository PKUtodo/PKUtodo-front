using Newtonsoft.Json.Linq;
using System;

namespace DarkDemo
{
    public class JSONHelper
    {
        public static string CreateJson(string mode, string email)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "email", email }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, string email, string password)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "email", email },
                { "password", password }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key, int id)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key },
                { "id", id }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key, string list_name)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key },
                { "list_name", list_name }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key, DateTime date)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key },
                { "DateTime", date.ToString() }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, string email, int veri_code, string user_name, string password)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "email", email },
                { "veri_code", veri_code },
                { "user_name", user_name },
                { "password", password }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key, int list_id, bool state)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key },
                { "list_id", list_id },
                { "state", state }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key, int list_id, int target_user_id)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key },
                { "list_id", list_id },
                { "target_user_id", target_user_id }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key, int task_id, DateTime creat_date, DateTime due_date, double position_x, double position_y, string content)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key },
                { "task_id", task_id },
                { "creat_date", creat_date.ToString() },
                { "due_date", due_date.ToString() },
                { "position_x", position_x },
                { "position_y", position_y },
                { "content", content }
            };
            return obj.ToString(Newtonsoft.Json.Formatting.None, null);
        }
        public static string CreateJson(string mode, int user_id, int key, int list_id, string task_name, DateTime creat_date, DateTime due_date, double position_x, double position_y, string content)
        {
            JObject obj = new JObject
            {
                { "mode", mode },
                { "user_id", user_id },
                { "key", key },
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

