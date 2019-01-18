using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SUDA_WIFI_Windows
{
    public partial class MainForm : Form
    {
        private static HttpHelper httpHelper = Program.Global.httpHelper;

        public MainForm()
        {
            InitializeComponent();
            
            tb_username.Text = Properties.Settings.Default.username;
            tb_password.Text = Properties.Settings.Default.password;
            checkbox_autostart.Checked = Properties.Settings.Default.autostart;

            checkbox_autostart.CheckedChanged += Checkbox_autostart_CheckedChanged;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if(btn_Login.Text == "登录")
            {
                new Thread(delegate ()
                {
                    RequestResponse requestResponse = httpHelper.DialLogin(tb_username.Text, tb_password.Text);
                    this.Invoke(new Action(delegate ()
                    {
                        if (requestResponse.Result)
                        {
                            Properties.Settings.Default.username = tb_username.Text;
                            Properties.Settings.Default.password = tb_password.Text;
                            Properties.Settings.Default.Save();
                            MessageBox.Show("登录成功!");
                        }
                        else
                        {
                            MessageBox.Show("登录失败! 错误原因: " + requestResponse.ResponseString);
                        }
                        TestOnline();
                    }), new object[0]);
                }).Start();
            }
            else if(btn_Login.Text == "下线")
            {
                new Thread(delegate ()
                {
                    RequestResponse requestResponse = httpHelper.DialLogout();
                    this.Invoke(new Action(delegate ()
                    {
                        if (requestResponse.Result)
                        {
                            MessageBox.Show("下线成功!");
                        }
                        else
                        {
                            MessageBox.Show("下线失败! 错误原因: " + requestResponse.ResponseString);
                        }
                        TestOnline();
                    }), new object[0]);
                }).Start();
            }
        }

        private void TestOnline()
        {
            new Thread(delegate ()
            {
                bool onlineState = httpHelper.OnlineState();
                this.Invoke(new Action(delegate ()
                {
                    if (onlineState)
                    {
                        this.btn_Login.Text = "下线";
                    }
                    else
                    {
                        this.btn_Login.Text = "登录";
                    }
                }), new object[0]);
            }).Start();
        }

        private void btn_Manage_Click(object sender, EventArgs e)
        {
            Manage manage = new Manage();
            manage.ShowDialog();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            ChangeAutoState();
            timer.Start();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            ChangeAutoState();
            timer.Stop();
        }

        private void ChangeAutoState()
        {
            tb_username.Enabled = !tb_username.Enabled;
            tb_password.Enabled = !tb_password.Enabled;
            btn_Login.Enabled = !btn_Login.Enabled;
            btn_Manage.Enabled = !btn_Manage.Enabled;
            btn_Start.Enabled = !btn_Start.Enabled;
            btn_Stop.Enabled = !btn_Stop.Enabled;
        }


        private void Checkbox_autostart_CheckedChanged(object sender, EventArgs e)
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool isLimited = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!isLimited)
            {
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

        //启动UAC
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

        private void trackBar_Interval_ValueChanged(object sender, EventArgs e)
        {
            timer.Interval = trackBar_Interval.Value * 1000;
            label6.Text = trackBar_Interval.Value + "s";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            new Thread(delegate ()
            {
                RequestResponse requestResponse = httpHelper.OnlineTime();
                this.Invoke(new Action(delegate ()
                {
                    if (!requestResponse.Result)
                    {
                        httpHelper.DialLogin(tb_username.Text, tb_password.Text);
                    }
                    lb_State.Text = requestResponse.ResponseString;
                }), new object[0]);
            }).Start();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //如果点击最小化按钮时，窗体隐藏，显示图标
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.notifyIcon.Visible = true;
                AddWindowExStyle(this.Handle, WS_EX_TOOLWINDOW);
            }
        }

        [DllImport("user32.dll")]
        public static extern
        Int32 GetWindowLong(IntPtr hwnd, Int32 index);
        [DllImport("user32.dll")]
        public static extern
        Int32 SetWindowLong(IntPtr hwnd, Int32 index, Int32 newValue);
        public const int GWL_EXSTYLE = (-20);
        //防止Alt+Tab
        public static int WS_EX_TOOLWINDOW = 0x00000080;
        public static void AddWindowExStyle(IntPtr hwnd, Int32 val)
        {
            int oldValue = GetWindowLong(hwnd, GWL_EXSTYLE);
            if (oldValue == 0)
            {
                throw new Win32Exception();
            }
            if (0 == SetWindowLong(hwnd, GWL_EXSTYLE, oldValue | val))
            {
                throw new Win32Exception();
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //RemoveWindowExStyle(this.Handle, WS_EX_TOOLWINDOW);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;

                this.Visible = true;//弹出MainForm
                this.WindowState = FormWindowState.Normal;//窗体恢复正常
                this.notifyIcon.Visible = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip.Show();
            }
        }
    }
}
