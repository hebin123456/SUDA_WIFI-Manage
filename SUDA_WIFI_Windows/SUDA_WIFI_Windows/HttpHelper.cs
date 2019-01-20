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
    enum Method{
        POST = 0,
        GET = 1
    }
    class HttpHelper
    {
        private static readonly string OnlineStateUrl = "http://a.suda.edu.cn/index.php/index/init?";
        private static readonly string DialLoginUrl = "http://a.suda.edu.cn/index.php/index/login";
        private static readonly string DialLogoutUrl = "http://a.suda.edu.cn/index.php/index/logout";
        private static readonly string VerifyUrl = "http://10.9.0.14/index.php/service/public/verify/";
        private static readonly string ManageLoginUrl = "http://10.9.0.14/index.php/service/public/checkLogin";
        private static readonly string AccountInfoUrl = "http://10.9.0.14/index.php/service/AccountInfo/index/";
        private static readonly string ManageLogoutUrl = "http://10.9.0.14/index.php/service/AccountInfo/logout";

        private static CookieContainer cookies = new CookieContainer();
        private static string strCookies = string.Empty;
        
        private static HttpWebRequest httpWebRequest = null;
        private static HttpWebResponse httpWebResponse = null;
        private static Uri uri = null;

        private string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }

        private void InitHttpWeb(string url, Method method)
        {
            uri = new Uri(url);
            httpWebRequest = (HttpWebRequest)(WebRequest.Create(uri));
            if (method == Method.POST) httpWebRequest.Method = "POST";
            else httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.CookieContainer = cookies;
            cookies = httpWebRequest.CookieContainer;
            httpWebRequest.KeepAlive = true;
        }

        private HttpWebResponse GetResponse(string postData="",Method method = Method.POST)
        {
            try
            {
                if(method == Method.POST)
                {
                    byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
                    httpWebRequest.ContentLength = postdatabyte.Length;
                    Stream stream = httpWebRequest.GetRequestStream();
                    stream.Write(postdatabyte, 0, postdatabyte.Length);
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    stream.Close();
                    return httpWebResponse;
                }
                else
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    return httpWebResponse;
                }
            }
            catch
            {
                return null;
            }
        }

        public string GetRequest(string url)
        {
            try
            {
                InitHttpWeb(url, Method.POST);
                httpWebResponse = GetResponse();
                string result = string.Empty;
                StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();
                sr.Close();
                return result;
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
            try
            {
                InitHttpWeb(DialLoginUrl, Method.POST);
                string postData = string.Format("username={0}&password={1}&enablemacauth=0", username, newpassword);  //这里按照前面FireBug中查到的POST字符串做相应修改
                httpWebResponse = GetResponse(postData);
                string result = string.Empty;
                StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();
                sr.Close();
                if (result.Contains("\"status\":1"))
                {
                    return new RequestResponse(true, "");
                }
                else
                {
                    int a = result.IndexOf("\"info\":\"");
                    int b = result.IndexOf("\",\"status\"");
                    string substring = result.Substring(a + 8, b - a - 8);
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
            try
            {
                InitHttpWeb(DialLogoutUrl, Method.POST);
                string postData = "";  //这里按照前面FireBug中查到的POST字符串做相应修改。
                httpWebResponse = GetResponse(postData);
                string result = string.Empty;
                StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();
                sr.Close();
                if (result.Contains("\"status\":1"))
                {
                    return new RequestResponse(true, "");
                }
                else
                {
                    int a = result.IndexOf("\"info\":\"");
                    int b = result.IndexOf("\",\"status\"");
                    string substring = result.Substring(a + 8, b - a - 8);
                    return new RequestResponse(false, Unicode2String(substring));
                }
            }
            catch (Exception ex)
            {
                return new RequestResponse(false, ex.Message);
            }
        }

        public Image GetValidateImage()
        {
            try
            {
                InitHttpWeb(VerifyUrl, Method.GET);
                httpWebResponse = GetResponse("", Method.GET);
                MemoryStream ms = null;
                Stream stream = httpWebResponse.GetResponseStream();
                Byte[] buffer = new Byte[httpWebResponse.ContentLength];
                int offset = 0, actuallyRead = 0;
                do
                {
                    actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                    offset += actuallyRead;
                }
                while (actuallyRead > 0);
                ms = new MemoryStream(buffer);

                cookies = httpWebRequest.CookieContainer; //保存cookies
                strCookies = httpWebRequest.CookieContainer.GetCookieHeader(httpWebRequest.RequestUri); //把cookies转换成字符串

                return (Image)new Bitmap(ms);
            }
            catch
            {
                return null;
            }
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
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(password);
                string newpassword = Convert.ToBase64String(bytes);
                string newverify = GetMD5(verify);
                InitHttpWeb(ManageLoginUrl, Method.POST);
                string postData = string.Format("username={0}&password={1}&verify={2}", username, newpassword, newverify);  //这里按照前面FireBug中查到的POST字符串做相应修改。
                httpWebResponse = GetResponse(postData);
                string result = string.Empty;
                StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();
                sr.Close();
                if (result.Contains("\"status\":1"))
                {
                    return new RequestResponse(true, "");
                }
                else
                {
                    int a = result.IndexOf("\"info\":\"");
                    int b = result.IndexOf("\",\"status\"");
                    string substring = result.Substring(a + 8, b - a - 8);
                    return new RequestResponse(false, Unicode2String(substring));
                }
            }
            catch(Exception ex)
            {
                return new RequestResponse(false, ex.ToString());
            }
        }

        public string GetInfo()
        {
            try
            {
                InitHttpWeb(AccountInfoUrl, Method.POST);
                httpWebResponse = GetResponse();
                string result = string.Empty;
                StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();
                sr.Close();
                return result;
            }
            catch
            {
                return "";
            }
        }

        public RequestResponse ManageLogout(string IP)
        {
            try
            {
                InitHttpWeb(ManageLogoutUrl, Method.POST);

                string postData = string.Format("framedipaddress={0}&onlinetype=0", IP);  //这里按照前面FireBug中查到的POST字符串做相应修改。
                httpWebResponse = GetResponse(postData);
                string result = string.Empty;
                StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();
                sr.Close(); if (result.Contains("\"status\":1"))
                {
                    return new RequestResponse(true, "");
                }
                else
                {
                    int a = result.IndexOf("\"info\":\"");
                    int b = result.IndexOf("\",\"status\"");
                    string substring = result.Substring(a + 8, b - a - 8);
                    return new RequestResponse(false, Unicode2String(substring));
                }
            }
            catch(Exception ex)
            {
                return new RequestResponse(false, ex.ToString());
            }
        }

        public User GetFee(string username, string password)
        {
            try
            {
                string html = GetRequest("http://wlfy.suda.edu.cn/index.aspx");

                string pattern = "<input.*?__VIEWSTATE.*?>";
                Regex regex = new Regex(pattern);
                string ViewState = regex.Match(html).ToString();
                int a = ViewState.IndexOf("value=\"");
                int b = ViewState.LastIndexOf("\"");
                ViewState = ViewState.Substring(a + 7, b - a - 7);

                pattern = "<input.*?__EVENTVALIDATION.*?>";
                regex = new Regex(pattern);
                string EventValidation = regex.Match(html).ToString();
                a = EventValidation.IndexOf("value=\"");
                b = EventValidation.LastIndexOf("\"");
                EventValidation = EventValidation.Substring(a + 7, b - a - 7);

                InitHttpWeb("http://wlfy.suda.edu.cn/index.aspx", Method.POST);
                string postData = string.Format("__VIEWSTATE={0}&__EVENTVALIDATION={1}&TextBox1={2}&TextBox2={3}&Button1=%E7%99%BB%E5%BD%95", ConvertUrl(ViewState), ConvertUrl(EventValidation), ConvertUrl(username), ConvertUrl(password));
                httpWebResponse = GetResponse(postData);

                string result = string.Empty;
                StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                result = sr.ReadToEnd();
                sr.Close();

                pattern = "<span id=\"姓名\">.*?</span>";
                regex = new Regex(pattern);
                string Name = regex.Match(result).ToString();
                a = Name.IndexOf("姓名");
                b = Name.LastIndexOf("<");
                Name = Name.Substring(a + 4, b - a - 4);

                pattern = "<span id=\"帐户\">.*?</span>";
                regex = new Regex(pattern);
                string Username = regex.Match(result).ToString();
                a = Username.IndexOf("帐户");
                b = Username.LastIndexOf("<");
                Username = Username.Substring(a + 4, b - a - 4);

                pattern = "<span id=\"余额\">.*?</span>";
                regex = new Regex(pattern);
                string Account = regex.Match(result).ToString();
                a = Account.IndexOf("余额");
                b = Account.LastIndexOf("<");
                Account = Account.Substring(a + 4, b - a - 4);
                
                return new User(Name, Username, Account);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string ConvertUrl(string data)
        {
            return data.Replace("/", "%2F").Replace("=", "%3D").Replace("+", "%2B");
        }
    }
}
