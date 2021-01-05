using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TODO
{
    class HTTP
    {
        public static string URL = "http://127.0.0.1:1000";
        #region POST请求
        /// <summary>
        /// POST请求
        /// </summary>
        /// <typeparam name="JObject"></typeparam>
        /// <param name="url">请求Url地址</param>
        /// <param name="postParameters">post提交参数</param>
        /// <returns></returns>
        public static JObject HttpPost(string js, string url = "http://10.128.169.239:5000")
        {
            try
            {
                //HTTP请求
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
                request.Method = "POST";
                request.ContentType = "text/json;charset:utf-8"; //
                // 设置超时时间
                request.Timeout = 3000;
                request.KeepAlive = true;

                //POST参数
                //编码要跟服务器编码统一
                byte[] bt = Encoding.UTF8.GetBytes(js);
                request.ContentLength = bt.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bt, 0, bt.Length);
                    reqStream.Close();
                }
                string retString = "";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        retString = streamReader.ReadToEnd().ToString();
                    }
                }
                return JObject.Parse(retString);
            }
            catch (Exception e)
            {
                return default(JObject);
            }
        }
        #endregion

        static public async System.Threading.Tasks.Task testpostAsync(string js, string url= "http://10.128.169.239:5000")
        {
            var data = new StringContent(js, Encoding.UTF8, "application/json");

            
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }
    }
}