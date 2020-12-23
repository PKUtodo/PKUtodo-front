using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DarkDemo
{
    public class JSONHelper
    {
        string CreatJson(string email)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(string email, string password)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key, int id)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key, string list_name)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key, DateTime date)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(string email, int veri_code, string user_name, string password)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key, int list_id, bool state)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key, int list_id, int target_user_id)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key, int task_id, DateTime creat_date, DateTime due_date, double position_x, double position_y, string content)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
        string CreatJson(int user_id, int key, int list_id, string task_name, DateTime creat_date, DateTime due_date, double position_x, double position_y, string content)
        {
            return (new JObject()).ToString(Newtonsoft.Json.Formatting.None, null);
        }
    }
}

