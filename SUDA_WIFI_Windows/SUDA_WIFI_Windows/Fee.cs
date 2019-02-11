using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SUDA_WIFI_Windows
{
    public partial class Fee : Form
    {
        private static HttpHelper httpHelper = Program.Global.httpHelper;
        private static string Username;
        private static string Password;

        public Fee()
        {
            InitializeComponent();

            Username = Properties.Settings.Default.username;
            Password = CryPto.Decrypt(Properties.Settings.Default.password);

            btn_Refresh_Click(null, null);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            new Thread(delegate ()
            {
                User user = httpHelper.GetFee(Username, Password);
                this.Invoke(new Action(delegate ()
                {
                    if (user!=null)
                    {
                        tb_Name.Text = user.Name;
                        tb_Username.Text = user.Username;
                        tb_Account.Text = user.Account;
                    }
                    else
                    {
                        tb_Name.Text = "获取数据出错!";
                        tb_Username.Text = "";
                        tb_Account.Text = "";
                    }
                }), new object[0]);
            }).Start();
        }

        private void btn_Pay_Click(object sender, EventArgs e)
        {
            //从注册表中读取默认浏览器可执行文件路径  
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();

            s = s.TrimStart('"');
            s = s.Substring(0, s.IndexOf("\""));

            try
            {
                Process.Start(s, "http://wlfy.suda.edu.cn/index.aspx");
            }
            catch
            {
                MessageBox.Show("打开默认浏览器失败, 请自行打开 http://wlfy.suda.edu.cn/index.aspx");
            }
        }
    }
}
