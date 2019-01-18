using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SUDA_WIFI_Windows
{
    class HttpHelper
    {
        private static readonly string OnlineStateUrl = "http://a.suda.edu.cn/index.php/index/init?";
        private static readonly string DialLoginUrl = "http://a.suda.edu.cn/index.php/index/login";
        private static readonly string DialLogoutUrl = "http://a.suda.edu.cn/index.php/index/logout";
        private static readonly string VerifyUrl = "http://10.9.0.14/index.php/service/public/verify/";
        private static readonly string ManageLoginUrl = "http://10.9.0.14/index.php/service/public/checkLogin";
        private static readonly string AccountInfoUrl = "http://10.9.0.14/index.php/service/AccountInfo/index/";
        private static readonly string ManageLogoutUrl = "http://10.9.0.14/index.php/service/AccountInfo/logout";

        public string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
        public string GetRequest(string url)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            CookieContainer cookies = new CookieContainer();
            try
            {
                Uri httpUrl = new Uri(url);
                req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.AllowAutoRedirect = true;
                req.CookieContainer = cookies;
                req.KeepAlive = true;

                string postData = "";  //这里按照前面FireBug中查到的POST字符串做相应修改。
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = postdatabyte.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                res = (HttpWebResponse)req.GetResponse();
                string strWebData = string.Empty;
                StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                strWebData = reader.ReadToEnd();
                return strWebData;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public bool OnlineState()
        {
            try
            {
                string result = GetRequest(OnlineStateUrl);
                if (result.Contains("\"status\":1"))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public RequestResponse OnlineTime()
        {
            try
            {
                string result = GetRequest(OnlineStateUrl);
                if (result.Contains("\"status\":1"))
                {
                    string pattern = "\"logout_timer\":\\d+";
                    Match match = (new Regex(pattern)).Match(result);
                    string s = match.ToString();
                    s = s.Substring(15, s.Length - 15);
                    int t = Int32.Parse(s);
                    int a = t / 3600;
                    int b = t / 60 - a * 60;
                    int c = t % 60;
                    return new RequestResponse(true, (a < 10 ? "0" : "") + a + ":" + (b < 10 ? "0" : "") + b + ":" + (c < 10 ? "0" : "") + c);
                }
                return new RequestResponse(false, "用户未登录");
            }
            catch
            {
                return new RequestResponse(false, "用户未登录");
            }
        }

        public RequestResponse DialLogin(string username, string password)
        {
            byte[] bytes = Encoding.Default.GetBytes(password);
            string newpassword = Convert.ToBase64String(bytes);
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            CookieContainer cookies = new CookieContainer();
            try
            {
                Uri httpUrl = new Uri(DialLoginUrl);
                req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.AllowAutoRedirect = true;
                req.CookieContainer = cookies;
                req.KeepAlive = true;

                string postData = string.Format("username={0}&password={1}&enablemacauth=0", username, newpassword);  //这里按照前面FireBug中查到的POST字符串做相应修改。
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = postdatabyte.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                res = (HttpWebResponse)req.GetResponse();
                string strWebData = string.Empty;
                StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                strWebData = reader.ReadToEnd();

                if (strWebData.Contains("\"status\":1"))
                {
                    return new RequestResponse(true, "");
                }
                else
                {
                    int a = strWebData.IndexOf("\"info\":\"");
                    int b = strWebData.IndexOf("\",\"status\"");
                    string substring = strWebData.Substring(a + 8, b - a - 8);
                    return new RequestResponse(false, Unicode2String(substring));
                }
            }
            catch (Exception ex)
            {
                return new RequestResponse(false, ex.Message);
            }
        }
        public RequestResponse DialLogout()
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            CookieContainer cookies = new CookieContainer();
            try
            {
                Uri httpUrl = new Uri(DialLogoutUrl);
                req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.AllowAutoRedirect = true;
                req.CookieContainer = cookies;
                req.KeepAlive = true;

                string postData = "";  //这里按照前面FireBug中查到的POST字符串做相应修改。
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = postdatabyte.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                res = (HttpWebResponse)req.GetResponse();
                string strWebData = string.Empty;
                StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                strWebData = reader.ReadToEnd();

                if (strWebData.Contains("\"status\":1"))
                {
                    return new RequestResponse(true, "");
                }
                else
                {
                    int a = strWebData.IndexOf("\"info\":\"");
                    int b = strWebData.IndexOf("\",\"status\"");
                    string substring = strWebData.Substring(a + 8, b - a - 8);
                    return new RequestResponse(false, Unicode2String(substring));
                }
            }
            catch (Exception ex)
            {
                return new RequestResponse(false, ex.Message);
            }
        }

        private CookieContainer cookies = null;
        private string strCookies = string.Empty;
        public Image GetValidateImage()
        {
            cookies = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(VerifyUrl);
            request.Accept = "*/*";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0";
            request.CookieContainer = new CookieContainer(); //暂存到新实例
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            MemoryStream ms = null;
            using (var stream = response.GetResponseStream())
            {
                Byte[] buffer = new Byte[response.ContentLength];
                int offset = 0, actuallyRead = 0;
                do
                {
                    actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                    offset += actuallyRead;
                }
                while (actuallyRead > 0);
                ms = new MemoryStream(buffer);
            }
            response.Close();

            cookies = request.CookieContainer; //保存cookies
            strCookies = request.CookieContainer.GetCookieHeader(request.RequestUri); //把cookies转换成字符串

            return (Image)new Bitmap(ms);
        }
        private string GetMD5(string sDataIn)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }

        public RequestResponse ManageLogin(string username, string password, string verify)
        {
            byte[] bytes = Encoding.Default.GetBytes(password);
            string newpassword = Convert.ToBase64String(bytes);
            string newverify = GetMD5(verify);

            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(ManageLoginUrl);
            request.Method = "POST";

            request.Accept = "*/*;";
            request.UserAgent = "Mozilla/5.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookies;
            request.KeepAlive = true;

            string postData = string.Format("username={0}&password={1}&verify={2}", username, newpassword, newverify);  //这里按照前面FireBug中查到的POST字符串做相应修改。
            byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = postdatabyte.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabyte, 0, postdatabyte.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string strWebData = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strWebData = reader.ReadToEnd();
            }
            if (strWebData.Contains("\"status\":1"))
            {
                return new RequestResponse(true, "");
            }
            else
            {
                int a = strWebData.IndexOf("\"info\":\"");
                int b = strWebData.IndexOf("\",\"status\"");
                string substring = strWebData.Substring(a + 8, b - a - 8);
                return new RequestResponse(false, Unicode2String(substring));
            }
        }

        public string GetInfo()
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(AccountInfoUrl);
            request.Method = "POST";

            request.Accept = "*/*;";
            request.UserAgent = "Mozilla/5.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookies;
            request.KeepAlive = true;

            string postData = "";  //这里按照前面FireBug中查到的POST字符串做相应修改。
            byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = postdatabyte.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabyte, 0, postdatabyte.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string strWebData = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strWebData = reader.ReadToEnd();
            }
            return strWebData;
        }

        public string ManageLogout(string IP)
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(ManageLogoutUrl);
            request.Method = "POST";

            request.Accept = "*/*;";
            request.UserAgent = "Mozilla/5.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookies;
            request.KeepAlive = true;

            string postData = string.Format("framedipaddress={0}&onlinetype=0", IP);  //这里按照前面FireBug中查到的POST字符串做相应修改。
            byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = postdatabyte.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabyte, 0, postdatabyte.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string strWebData = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strWebData = reader.ReadToEnd();
            }
            return strWebData;
        }
    }
}
