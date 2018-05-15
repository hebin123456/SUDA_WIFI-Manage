using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SUDA_WIFI
{
    public class MyWebClient : WebClient
    {
        private IPAddress ipAddress;

        public MyWebClient(IPAddress ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = (WebRequest)base.GetWebRequest(address);

            ((HttpWebRequest)request).ServicePoint.BindIPEndPointDelegate += (servicePoint, remoteEndPoint, retryCount) =>
            {
                return new IPEndPoint(ipAddress, 0);
            };

            return request;
        }
    }
}
