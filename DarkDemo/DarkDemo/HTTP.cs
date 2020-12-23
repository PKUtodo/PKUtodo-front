using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DarkDemo
{
    class HTTP
    {
        #region POST请求
        /// <summary>
        /// POST请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求Url地址</param>
        /// <param name="postParameters">post提交参数</param>
        /// <returns></returns>
        public static T HttpPostStr<T>(string url, string js)
        {
            try
            {
                string retString = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //注意：输入特定格式的时候头文件上下文需说明，如JSON字符串声明 
                //为："application/json;"
                request.ContentType = "application/x-www-form-urlencoded;charset:utf-8";
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
                return JsonConvert.DeserializeObject<T>(retString);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
        #endregion
    }
}
