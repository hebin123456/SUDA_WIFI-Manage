/*
 * 由SharpDevelop创建。
 * 用户： 何彬
 * 日期: 2017/6/13
 * 时间: 20:42
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SUDA_WIFI
{
	/// <summary>
	/// Description of Manage.
	/// </summary>
	public partial class Manage : Form
	{
		public Manage()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			textBox1.Text = Settings.Default.username.Trim();
			textBox2.Text = Settings.Default.password.Trim();
			
			GetValidateImage();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		CookieContainer cookies = null;
        string strCookies = string.Empty;
		
		/// <summary>
        /// 获取验证码和Cookie
        /// </summary>
        private void GetValidateImage()
        {
            cookies = new CookieContainer();
            string url = "http://10.9.0.14/index.php/service/public/verify/";  //验证码页面
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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

            Bitmap bi = new Bitmap((Image)new Bitmap(ms));
            pictureBox1.Image = bi;
        }
		
        private void PictureBox1Click(object sender, EventArgs e)
		{
        	GetValidateImage();
		}
		
        private void Button_LoginClick(object sender, EventArgs e)
		{
        	listView1.Items.Clear();
        	string result = Login(textBox1.Text, textBox2.Text, textBox3.Text);
        	if(result.Contains("\"status\":1")){
				MessageBox.Show("登录成功！");
				string html = GetInfo();
				
				Regex reg = new Regex(@"<table id[\s\S]*?</table>");
				string table = reg.Match(html).ToString();
				
				reg = new Regex(@"<tr[\s\S]*?</tr>");
				
				int i = 0;
				foreach(Match m in reg.Matches(table)){
					if(i != 0){
						Regex regtd = new Regex(@"<td[\s\S]*?</td>");
						string tr = m.ToString();
						string[] list = new string[20];
						int j = 0;
						foreach(Match m2 in regtd.Matches(tr)){
							string td = m2.ToString();
							td = td.Replace("<td>", "").Replace("</td>", "").Replace("<td style=\"width:15%\">", "");
							list[j] = td;
							j++;
						}
						ListViewItem lt = new ListViewItem();
						lt.Text = list[0];
						lt.SubItems.Add(list[1]);
						lt.SubItems.Add(list[2]);
						lt.SubItems.Add(list[3]);
						listView1.Items.Add(lt);
					}
					i++;
				}
			}
			else{
				int a = result.IndexOf("\"info\":\"");
				int b = result.IndexOf("\",\"status\"");
				string substring = result.Substring(a + 8, b - a - 8);
				MessageBox.Show("登录失败！错误原因：" + Unicode2String(substring));
			}
		}
        
        private string Login(string username, string password, string verify)
        {
        	byte[] bytes = Encoding.Default.GetBytes(password);
			string newpassword = Convert.ToBase64String(bytes);
			string newverify = GetMD5(verify);
			
        	HttpWebRequest request = null;
            string url = "http://10.9.0.14/index.php/service/public/checkLogin";   //登录页面
            request = (HttpWebRequest)WebRequest.Create(url);
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
            return strWebData;
        }
        
        private string GetInfo()
        {
        	HttpWebRequest request = null;
            string url = "http://10.9.0.14/index.php/service/AccountInfo/index/";   //登录页面
            request = (HttpWebRequest)WebRequest.Create(url);
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
        
        private string Loginout(string IP)
        {	
        	HttpWebRequest request = null;
            string url = "http://10.9.0.14/index.php/service/AccountInfo/logout";   //登录页面
            request = (HttpWebRequest)WebRequest.Create(url);
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
        
        private string Unicode2String(string source)
		{
		    return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
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
		
        private void ListView1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
			if(info.Item != null){
				var item = info.Item as ListViewItem;
				string IP = item.SubItems[2].Text;
				string result = Loginout(IP);
				if(result.Contains("\"status\":1")){
					MessageBox.Show("下线成功！");
					listView1.Items.Remove(item);
				}
				else{
					MessageBox.Show("下线失败！");
				}
			}
		}
	}
}
