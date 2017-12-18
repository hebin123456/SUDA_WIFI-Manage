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
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
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

            // textBox1.Text = Settings.Default.username.Trim();
            // textBox2.Text = Settings.Default.password.Trim();
            tb_username.Text = Properties.Settings.Default.username;
            tb_password.Text = Properties.Settings.Default.username;

            LoadLib();
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

            pictureBox1.Image = (Image)new Bitmap(ms);

            // 识别验证码
            byte[] Buffer = ImageToBytes(pictureBox1.Image);
            StringBuilder Result = new StringBuilder('\0', 256);
            if (GetImageFromBuffer(Buffer, Buffer.Length, Result))
                tb_validate.Text = Result.ToString();
            else
                tb_validate.Text = "识别失败";
        }

        // 识别读取验证码byte序列
        public byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private void PictureBox1Click(object sender, EventArgs e)
		{
        	GetValidateImage();
		}
		
        private void Button_LoginClick(object sender, EventArgs e)
		{
        	listView1.Items.Clear();
        	string result = Login(tb_username.Text, tb_password.Text, tb_validate.Text);
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

        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromFile(string FilePath, string Password);

        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromBuffer(byte[] FileBuffer, int ImgBufLen, StringBuilder Vcode);

        [DllImport("WmCode.dll")]
        public static extern bool SetWmOption(int OptionIndex, int OptionValue);

        // 加载验证码识别库
        private void LoadLib()
        {
            if (LoadWmFromFile("SUDA_WIFI.dat", "123456"))
            {
                SetWmOption(6, 90);
            }
        }

        static string encryptKey = "Oyea";    //定义密钥
        #region 加密字符串(已废弃)
        /// <summary> /// 加密字符串   
        /// </summary>  
        /// <param name="str">要加密的字符串</param>  
        /// <returns>加密后的字符串</returns>  
        static string Encrypt(string str)  
        {    
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象   
    
            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    
   
            byte[] data = Encoding.Unicode.GetBytes(str);//定义字节数组，用来存储要加密的字符串  
     
            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      
  
            //使用内存流实例化加密流对象   
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);     
  
            CStream.Write(data, 0, data.Length);  //向加密流中写入数据      
  
            CStream.FlushFinalBlock();              //释放加密流      
  
            return Convert.ToBase64String(MStream.ToArray());//返回加密后的字符串  
        }
        #endregion

        #region 解密字符串(已废弃)
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        static string Decrypt(string str)  
        {      
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象    
     
            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    
    
            byte[] data = Convert.FromBase64String(str);//定义字节数组，用来存储要解密的字符串  
      
            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      
   
            //使用内存流实例化解密流对象       
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);   
   
            CStream.Write(data, 0, data.Length);      //向解密流中写入数据     
  
            CStream.FlushFinalBlock();               //释放解密流      
  
            return Encoding.Unicode.GetString(MStream.ToArray());       //返回解密后的字符串  
        }  
        #endregion 
	}
}
