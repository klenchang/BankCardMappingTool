using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapBankCard
{
    public class HttpRequestUtility
    {
        public HttpRequestUtility()
        {
            // default timeout after 10 seconds
            Timeout = 10;
            // default charset
            CharSet = Encoding.UTF8.HeaderName;
        }
        public string Url { get; set; }
        public Dictionary<string, string> FormHeaders { get; set; }
        public HttpContent FormContent { get; set; }
        public HttpMethod FormMethod { get; set; }
        public IWebProxy Proxy { get; set; }
        /// <summary>
        /// unit: seconds
        /// </summary>
        public int Timeout { get; set; }
        public string ContentType { get; set; }
        public HttpResponseMessage Submit()
        {
            using (var client = new HttpClient(new HttpClientHandler { Proxy = Proxy }) { Timeout = TimeSpan.FromSeconds(Timeout) })
            {
                if (FormHeaders != null && FormHeaders.Count > 0)
                    foreach (var header in FormHeaders)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);

                SetFormContentType();

                switch (FormMethod.Method.ToLower())
                {
                    case "get":
                        return client.GetAsync(Url).Result;
                    case "post":
                        return client.PostAsync(Url, FormContent).Result;
                    default:
                        throw new ArgumentOutOfRangeException("FormMethod");
                }
            }
        }
        public string GetRequestLogString()
        {
            var content = FormContent != null ? FormContent.ReadAsStringAsync().Result : "null";
            if (BodyLogFilter != null) content = BodyLogFilter(content);

            var header = FormHeaders != null ? GeneralUtility.ConvertToKeyValueString(FormHeaders) : "null";
            if (HeaderLogFilter != null) header = HeaderLogFilter(header);

            SetFormContentType();
            var contentType = FormContent?.Headers.ContentType.ToString() ?? "null";

            return $"\r\nUrl:{Url}\r\nTimeout:{Timeout} sec\r\nSubmit Method:{FormMethod.Method}\r\nHeader:{header}\r\nContentType:{contentType}\r\nContent:{content}";
        }
        public Func<string, string> BodyLogFilter { get; set; }
        public Func<string, string> HeaderLogFilter { get; set; }
        public string CharSet { get; set; }
        public async Task<string> ReadResponseContentAsStringAsync(HttpResponseMessage response)
        {
            // Check response charset. If charset is invalid, set charset to default charset.
            try
            {
                Encoding.GetEncoding(response.Content.Headers.ContentType.CharSet);
            }
            catch
            {
                response.Content.Headers.ContentType.CharSet = CharSet;
            }

            return await response.Content.ReadAsStringAsync();
        }
        private void SetFormContentType()
        {
            if (FormContent != null)
            {
                if (ContentType != null)
                    FormContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ContentType);

                if (CharSet != null)
                    FormContent.Headers.ContentType.CharSet = CharSet;
            }
        }
    }
}