using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DarkDemo
{
    class WorkerThreadhandler
    {
        public TcpListener myTcplistener;
        static string webRoot = "D:\\MyCMD\\";
        static string defaultPage = "index.html,home.html";
        public void HandleThread()
        {
            try
            {
                Socket socket = myTcplistener.AcceptSocket();

                byte[] recv_Buffer = new byte[1024 * 640];
                int recv_Count = socket.Receive(recv_Buffer);  //接收浏览器的请求数据
                string recv_request = Encoding.UTF8.GetString(recv_Buffer, 0, recv_Count);

                //Resolve(recv_request, web_client);  //解析、路由、处理

                byte[] cont = pageHandle(RouteHandle(recv_request));
                sendPageContent(cont, socket);


            }
            catch (Exception ex)
            {
                writeLog("Error!");
            }

        }
        static void sendPageContent(byte[] pageContent, Socket response)
        {

            string statusline = "HTTP/1.1 200 OK\r\n";   //状态行
            byte[] statusline_to_bytes = Encoding.UTF8.GetBytes(statusline);

            byte[] content_to_bytes = pageContent;

            string header = string.Format("Content-Type:text/html;charset=UTF-8\r\nContent-Length:{0}\r\n", content_to_bytes.Length);
            byte[] header_to_bytes = Encoding.UTF8.GetBytes(header);  //应答头


            response.Send(statusline_to_bytes);  //发送状态行
            response.Send(header_to_bytes);  //发送应答头
            response.Send(new byte[] { (byte)'\r', (byte)'\n' });  //发送空行
            response.Send(content_to_bytes);  //发送正文（html）

            response.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string RouteHandle(string request)
        {
            string retRoute = "";
            string[] strs = request.Split(new string[] { "\r\n" }, StringSplitOptions.None);  //以“换行”作为切分标志
            if (strs.Length > 0)  //解析出请求路径、post传递的参数(get方式传递参数直接从url中解析)
            {
                string[] items = strs[0].Split(' ');  //items[1]表示请求url中的路径部分（不含主机部分）
                string pageName = items[1];
                string post_data = strs[strs.Length - 1]; //最后一项
                //Dictionary<string, string> dict = ParameterHandle(strs);

                retRoute = pageName + (post_data.Length > 0 ? "?" + post_data : "");
            }

            return retRoute;

        }


        /// <summary>
        /// 按照HTTP协议格式,解析浏览器发送的请求字符串
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        static Dictionary<string, string> ParameterHandle(string[] strs)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            if (strs.Length > 0)  //解析出请求路径、post传递的参数(get方式传递参数直接从url中解析)
            {
                if (strs.Contains(""))  //包含空行  说明存在post数据
                {
                    string post_data = strs[strs.Length - 1]; //最后一项
                    if (post_data != "")
                    {
                        string[] post_datas = post_data.Split('&');
                        foreach (string s in post_datas)
                        {
                            param.Add(s.Split('=')[0], s.Split('=')[1]);
                        }
                    }
                }
            }
            return param;
        }

        static byte[] pageHandle(string pagePath)
        {
            byte[] pageContent = null;

            pagePath = pagePath.TrimEnd('/', '\\');
            if (pagePath.Length == 0)
            {
                foreach (string page in defaultPage.Split(','))
                {
                    if (System.IO.File.Exists(webRoot + page))
                    {
                        pagePath = page;
                        break;
                    }
                }
            }
            if (System.IO.File.Exists(webRoot + pagePath))
                pageContent = System.IO.File.ReadAllBytes(webRoot + pagePath);
            if (pageContent == null)
            {

                string content = notExistPage();
                pageContent = Encoding.UTF8.GetBytes(content);

            }
            return pageContent;
        }


        static void writeLog(string msg)
        {
            Console.WriteLine("  " + msg);
        }


        static string notExistPage()
        {
            string cont = @"<!DOCTYPE HTML>
<html>

    <head>
        <link rel='stylesheet' type='text/css' href='NewErrorPageTemplate.css' >

        <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
        <title>This page can&rsquo;t be displayed</title>

        <script src='errorPageStrings.js' language='javascript' type='text/javascript'>
        </script>
        <script src='httpErrorPagesScripts.js' language='javascript' type='text/javascript'>
        </script>
    </head>

    <body onLoad='javascript:getInfo();'>
        <div id='contentContainer' class='mainContent'>
            <div id='mainTitle' class='title'>This page can&rsquo;t be displayed</div>
            <div class='taskSection' id='taskSection'>
                <ul id='cantDisplayTasks' class='tasks'>
                    <li id='task1-1'>Make sure the web address <span id='webpage' class='webpageURL'></span>is correct.</li>
                    <li id='task1-2'>Look for the page with your search engine.</li>
                    <li id='task1-3'>Refresh the page in a few minutes.</li>
                </ul>
                <ul id='notConnectedTasks' class='tasks' style='display:none'>
                    <li id='task2-1'>Check that all network cables are plugged in.</li>
                    <li id='task2-2'>Verify that airplane mode is turned off.</li>
                    <li id='task2-3'>Make sure your wireless switch is turned on.</li>
                    <li id='task2-4'>See if you can connect to mobile broadband.</li>
                    <li id='task2-5'>Restart your router.</li>
                </ul>
            </div>
            <div><button id='diagnose' class='diagnoseButton' onclick='javascript:diagnoseConnectionAndRefresh(); return false;'>Fix connection problems</button></div>
        </div>
    </body>
</html>";

            return cont;
        }

    };
}
