using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求Url地址</param>
        /// <param name="postParameters">post提交参数</param>
        /// <returns></returns>
        public static JObject HttpPost(string js, string url = "http://127.0.0.1:1000")
        {
            try
            {
                string retString = "";
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset:utf-8";
                // 设置超时时间
                request.Timeout = 3000;
                request.KeepAlive = false;
                //POST参数
                //编码要跟服务器编码统一
                byte[] bt = Encoding.UTF8.GetBytes(js);
                string responseData = String.Empty;
                request.ContentLength = bt.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bt, 0, bt.Length);
                    reqStream.Close();
                }
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
    }
}