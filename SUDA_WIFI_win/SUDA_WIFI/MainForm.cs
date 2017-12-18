/*
 * 由SharpDevelop创建。
 * 用户： 何彬
 * 日期: 2017/6/11
 * 时间: 13:19
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
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
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            // textBox1.Text = Settings.Default.username.Trim();
            // textBox2.Text = Settings.Default.password.Trim();
            tb_username.Text = Properties.Settings.Default.username;
            tb_password.Text = Properties.Settings.Default.username;

            string result = getRequest("http://a.suda.edu.cn/index.php/index/init?");
			if(result.Contains("\"status\":1")){
				btn_Login.Text = "下线";
				flag = 1;
			}
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private int flag = 0;
		
		private void Button_LoginClick(object sender, EventArgs e)
		{
			if(flag == 0){
				string result = getRequest("http://a.suda.edu.cn/index.php/index/login", tb_username.Text, tb_password.Text);
				if(result.Contains("\"status\":1")){
					MessageBox.Show("登录成功！");
					btn_Login.Text = "下线";
					flag = 1;

                    Properties.Settings.Default.username = tb_username.Text;
                    Properties.Settings.Default.password = tb_password.Text;
                    Properties.Settings.Default.Save();
				}
				else{
					int a = result.IndexOf("\"info\":\"");
					int b = result.IndexOf("\",\"status\"");
					string substring = result.Substring(a + 8, b - a - 8);
					MessageBox.Show("登录失败！错误原因：" + Unicode2String(substring));
				}
			}
			else if(flag == 1){
				string result = getRequest("http://a.suda.edu.cn/index.php/index/logout");
				if(result.Contains("\"status\":1")){
					MessageBox.Show("下线成功！");
					btn_Login.Text = "登录";
					flag = 0;
				}
				else{
					MessageBox.Show("下线失败！");
				}
			}
		}
		
		private string getRequest(string url){
			HttpWebRequest req=null;
			HttpWebResponse res = null;
			CookieContainer cookies = new CookieContainer();
			try{
				Uri httpUrl=new Uri(url);
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
			catch(Exception ex){
				return ex.ToString();
			}
		}
		
		private string getRequest(string url, string username, string password){
			byte[] bytes = Encoding.Default.GetBytes(password);
			string newpassword = Convert.ToBase64String(bytes);
			HttpWebRequest req=null;
			HttpWebResponse res = null;
			CookieContainer cookies = new CookieContainer();
			try{
				Uri httpUrl=new Uri(url);
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
				return strWebData;
			}
			catch(Exception ex){
				return ex.ToString();
			}
		}
		
		private string Unicode2String(string source)
		{
		    return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
		}
		
		private void Button_ManageClick(object sender, EventArgs e)
		{
			Manage manage = new Manage();
			manage.ShowDialog();
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
