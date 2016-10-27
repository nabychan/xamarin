using System;
using System.Collections.Generic;
using System.Text;
using Core.Http;
using Newtonsoft.Json;

namespace Core.Api
{
    public interface IApi
    {
        T Request<T>(HttpMethod method, Dictionary<string, string> headers=null, Dictionary<string, string> queries=null, Dictionary<string, string> bodies=null, string url=null);
    }
    public class Api : IApi
    {
        public string Url { get; set; }
        public T Request<T>(HttpMethod method, Dictionary<string, string> headers=null, Dictionary<string, string> queries=null, Dictionary<string, string> bodies=null, string url=null)
        {
            if ((String.IsNullOrWhiteSpace(Url) && String.IsNullOrWhiteSpace(url)) || method == HttpMethod.NONE)
            {
                throw new Exception($"Url为空或未指定Http Method:{JsonConvert.SerializeObject(new { Url, method })}");
            }

            var uri = url;
            if (String.IsNullOrWhiteSpace(uri))
                uri = Url;
            if (!uri.StartsWith("http://"))
                uri = "http://api.p.tgnet.com" + uri;
            uri += "?";
            foreach (var item in queries ?? new Dictionary<string, string>())
            {
                uri += System.Net.WebUtility.UrlEncode(item.Key) + "=" + System.Net.WebUtility.UrlEncode(item.Value)+"&";
            }
            var req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
            req.Method = method.ToString();
            foreach (var item in headers ?? new Dictionary<string, string>())
            {
                req.Headers[item.Key] = item.Value;
            }
            var body = "";
            foreach (var item in bodies??new Dictionary<string, string>())
            {
                body += System.Net.WebUtility.UrlEncode(item.Key) + "&" + System.Net.WebUtility.UrlEncode(item.Value);
            }
            if (!String.IsNullOrWhiteSpace(body))
            {
                var bytes = Encoding.UTF8.GetBytes(body);
                req.GetRequestStream().Write(bytes, 0, bytes.Length);
                req.ContentLength = bytes.Length;
            }

            var response = req.GetResponse();
            var result = "";
            using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            response.Close();
            req.Abort();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
