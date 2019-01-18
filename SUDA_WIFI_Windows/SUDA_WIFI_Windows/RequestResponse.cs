using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUDA_WIFI_Windows
{
    class RequestResponse
    {
        public bool Result { set; get; }
        public string ResponseString { set; get; }
        public RequestResponse(bool Result = false, string ResponseString = "")
        {
            this.Result = Result;
            this.ResponseString = ResponseString;
        }

        public override string ToString()
        {
            return Result + "|" + ResponseString;
        }
    }
}
