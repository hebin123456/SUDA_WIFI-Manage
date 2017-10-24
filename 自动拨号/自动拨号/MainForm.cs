/*
 * 由SharpDevelop创建。
 * 用户： 以夕阳落款
 * 日期: 2017/6/29
 * 时间: 20:38
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
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace 自动拨号
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
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			if(File.Exists(@"config.ini")){
				try{
					string[] str = File.ReadAllLines(@"config.ini");
					textBox1.Text = str[0];
					textBox2.Text = Decrypt(str[1]);
				}
				catch{
					
				}
			}
			
			string path = Application.ExecutablePath;
			RegistryKey rk = Registry.LocalMachine;
			RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			string a = (string)rk2.GetValue("SUDA_AUTO");
			
			if(a != null) checkBox1.CheckState = CheckState.Checked;
			
			if(Online()){
				label2.Text = "连接";
			}
			else{
				label2.Text = "断开";
			}
		}
		
		private void MainFormSizeChanged(object sender, EventArgs e)
		{
			if(this.WindowState == FormWindowState.Minimized)  //判断是否最小化
			{
				this.ShowInTaskbar = false;  //不显示在系统任务栏
				notifyIcon1.Visible = true;  //托盘图标可见
			}
		}
		
		private void NotifyIcon1DoubleClick(object sender, EventArgs e)
		{
			if(this.WindowState == FormWindowState.Minimized)
			{
				this.ShowInTaskbar = true;  //显示在系统任务栏
				this.WindowState = FormWindowState.Normal;  //还原窗体
				notifyIcon1.Visible = false;  //托盘图标隐藏
			}
		}
		
		Thread th;
		
		private void Button1Click(object sender, EventArgs e)
		{
			textBox1.Enabled = false;
			textBox2.Enabled = false;
			button1.Enabled = false;
			button2.Enabled = true;
			
			string path = @"config.ini";
			// 用覆盖的方式写入  
			string contents = textBox1.Text + "\r\n" + Encrypt(textBox2.Text);
			File.WriteAllText(path, contents);  
			
			th = new Thread(new ParameterizedThreadStart(MethodThread));
            th.Start();
		}
		
		private void Button2Click(object sender, EventArgs e)
		{
			textBox1.Enabled = true;
			textBox2.Enabled = true;
			button1.Enabled = true;
			button2.Enabled = false;
			
			th.Abort();
		}  
		
		private delegate void SetOtherPage(string username, string password);//创建一个代理

        // 顺序播放
        public void MethodThread(Object list)
        {
        	while(true){
                Method("", "");
                Thread.Sleep(10000);
            }
        }

        //子线程操作主线程
        private void Method(string username, string password)
        {
            if (!InvokeRequired)
            {
            	if(Online()){
					label2.Text = "连接";
				}
				else{
					label2.Text = "断开";
					string result = getRequest("http://a.suda.edu.cn/index.php/index/login", textBox1.Text, textBox2.Text);
					if(result.Contains("\"status\":1")){
						string path = @"config.ini";
						// 用覆盖的方式写入  
						string contents = textBox1.Text + "\r\n" + Encrypt(textBox2.Text);
						File.WriteAllText(path, contents);  
					}
				}
            }
            else
            {
                SetOtherPage a1 = new SetOtherPage(Method);
                Invoke(a1, new object[] { username, password });//执行唤醒操作
            }
        }
		
		private bool Online(){
			try{
				string result = getRequest("http://a.suda.edu.cn/index.php/index/init?");
				if(result.Contains("\"status\":1")){
					return true;
				}
				return false;
			}
			catch{
				return false;
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
		
		static string encryptKey = "Oyea";    //定义密钥
		#region 加密字符串  
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
  
        #region 解密字符串   
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
        
		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;//取消窗体的关闭事件
			this.WindowState = FormWindowState.Minimized;//使当前窗体最小化
			notifyIcon1.Visible = true;//使最下化的图标可见
		}
		
		private void 退出ToolStripMenuItemClick(object sender, EventArgs e)
		{
			try{
				th.Abort();
			}
			catch{}
			Application.Exit();
		}
		
		private void CheckBox1Click(object sender, EventArgs e)
		{
			if (checkBox1.Checked) //设置开机自启动
            {
                MessageBox.Show ("已设置开机自启动","提示");  
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("SUDA_AUTO", path);
                rk2.Close();
                rk.Close();
            }
            else //取消开机自启动  
            {
                MessageBox.Show ("已取消开机自启动","提示");  
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("SUDA_AUTO", false);
                rk2.Close();
                rk.Close();
            }
		}
	}
}
