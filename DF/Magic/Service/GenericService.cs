using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace Magic.Service
{
    public class GenericService
    {
        #region variables
        private HttpWebRequest _request;
        private string _url;
        private string _xml;

        public GenericService()
        {
        }

        public GenericService(string url)
        {
            _url = url;
        }

        public GenericService(string url, string xml)
        {
            _url = url;
            _xml = xml;
        }

        private void CreateRequest()
        {
            if (string.IsNullOrWhiteSpace(_url))
                return;

            _request = (HttpWebRequest)WebRequest.Create(_url);
            _request.Headers.Add("SOAP:Action");
            _request.ContentType = "text/xml;charset=\"utf-8\"";
            _request.Accept = "text/xml";
            _request.Method = "Post";

        }

        public string CallService()
        {
            string res = string.Empty;
            try
            {
                CreateRequest();
                if (_request == null)
                    return "Cannot create SOAP Request.  Please check your parameters.";                

                using (System.IO.Stream stream = _request.GetRequestStream())
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(_xml);
                    doc.Save(stream);
                }

                using (WebResponse response = _request.GetResponse())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        res = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.ToString();
            }

            return res;
        }

        #endregion
    }
}
