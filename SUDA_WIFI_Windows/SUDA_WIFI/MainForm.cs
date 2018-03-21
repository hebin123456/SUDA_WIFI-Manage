/*
 * 由SharpDevelop创建。
 * 用户： 何彬
 * 日期: 2017/6/11
 * 时间: 13:19
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            tb_password.Text = Properties.Settings.Default.password;
            checkbox_autostart.Checked = Properties.Settings.Default.autostart;

            string result = getRequest("http://a.suda.edu.cn/index.php/index/init?");
			if(result.Contains("\"status\":1")){
				btn_Login.Text = "下线";
				flag = 1;
			}

            if (Properties.Settings.Default.autostart)
            {
                btn_Start_Click(null, null);
            }
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
        

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                ifMinimized();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ifMinimized();
            this.WindowState = FormWindowState.Normal;
        }

        private void ifMinimized()
        {
            this.ShowInTaskbar = !this.ShowInTaskbar;
            notifyIcon1.Visible = !notifyIcon1.Visible;
        }
        
        Thread th;

        private void btn_Start_Click(object sender, EventArgs e)
        {
            changeAutoState();
            
            th = new Thread(new ParameterizedThreadStart(MethodThread));
            th.Start();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            changeAutoState();

            th.Abort();
        }

        private delegate void SetOtherPage(string username, string password);//创建一个代理

        // 顺序播放
        public void MethodThread(Object list)
        {
            while (true)
            {
                Method("", "");
                Thread.Sleep(1000);
            }
        }

        //子线程操作主线程
        private void Method(string username, string password)
        {
            if (!InvokeRequired)
            {
                string str = Online();
                if (str != "0")
                {
                    lb_State.Text = "已连接 " + str;
                }
                else
                {
                    lb_State.Text = "断开";
                    string result = getRequest("http://a.suda.edu.cn/index.php/index/login", tb_username.Text, tb_password.Text);
                    if (result.Contains("\"status\":1"))
                    {
                        Properties.Settings.Default.username = tb_username.Text;
                        Properties.Settings.Default.password = tb_password.Text;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            else
            {
                SetOtherPage a1 = new SetOtherPage(Method);
                Invoke(a1, new object[] { username, password });//执行唤醒操作
            }
        }

        private string Online()
        {
            try
            {
                string result = getRequest("http://a.suda.edu.cn/index.php/index/init?");
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
                    return (a < 10? "0":"") + a + ":" + (b < 10 ? "0" : "") + b + ":" + (c < 10 ? "0" : "") + c;
                }
                return "0";
            }
            catch
            {
                return "0";
            }
        }
        
        private void changeAutoState()
        {
            tb_username.Enabled = !tb_username.Enabled;
            tb_password.Enabled = !tb_password.Enabled;
            btn_Login.Enabled = !btn_Login.Enabled;
            btn_Manage.Enabled = !btn_Manage.Enabled;
            btn_Start.Enabled = !btn_Start.Enabled;
            btn_Stop.Enabled = !btn_Stop.Enabled;
        }

        private void checkbox_autostart_Click(object sender, EventArgs e)
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool isLimited = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!isLimited)
            {
                // 取消要记得取消状态改变
                checkbox_autostart.Checked = !checkbox_autostart.Checked;

                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("设置开机自启需要管理员权限, 确定吗?", "退出系统", messButton);
                if (dialogResult == DialogResult.OK)
                {
                    RunElevated(Application.ExecutablePath);
                    Environment.Exit(0);
                }
            }
            else
            {
                if (checkbox_autostart.Checked) //设置开机自启动
                {
                    MessageBox.Show("已设置开机自启动", "提示");
                    string path = Application.ExecutablePath;
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.SetValue("SUDA_AUTO", path);
                    rk2.Close();
                    rk.Close();
                    Properties.Settings.Default.autostart = true;
                    Properties.Settings.Default.Save();
                }
                else //取消开机自启动  
                {
                    MessageBox.Show("已取消开机自启动", "提示");
                    string path = Application.ExecutablePath;
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.DeleteValue("SUDA_AUTO", false);
                    rk2.Close();
                    rk.Close();
                    Properties.Settings.Default.autostart = false;
                    Properties.Settings.Default.Save();
                }
            }
        }

        // 启动UAC
        private void RunElevated(string fileName)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.FileName = fileName;
            try
            {
                Process.Start(processInfo);
            }
            catch { }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                th.Abort();
            }
            catch { }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
