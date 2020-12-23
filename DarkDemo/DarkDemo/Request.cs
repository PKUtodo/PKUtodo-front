using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DarkDemo
{
    public enum RequestMode : int
    {
        EMAILCHECK = 0,
        SIGNIN = 1,
        SIGNUP = 2,
        ADDLIST = 3,
        ADDTASK = 4
    }

    class Request
    {
        JObject HTTPRequest(int mode, string js)
        {
            return new JObject();
        }
    }
}
