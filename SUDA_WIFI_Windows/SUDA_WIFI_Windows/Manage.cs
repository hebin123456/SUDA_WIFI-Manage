using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SUDA_WIFI_Windows
{
    public partial class Manage : Form
    {
        private static bool IfLoadLib = false;
        private static HttpHelper httpHelper = Program.Global.httpHelper;

        public Manage()
        {
            InitializeComponent();

            tb_username.Text = Properties.Settings.Default.username;
            tb_password.Text = Properties.Settings.Default.password;

            new Thread(delegate ()
            {
                LoadLib();
                Image image = httpHelper.GetValidateImage();
                this.Invoke(new Action(delegate ()
                {
                    pictureBox_Code.Image = image;
                    if (IfLoadLib) tb_validate.Text = Identification();
                    else tb_validate.Text = "加载识别库失败!";
                }), new object[0]);
            }).Start();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            Login_List.Items.Clear();
            new Thread(delegate ()
            {
                RequestResponse requestResponse = httpHelper.ManageLogin(tb_username.Text, tb_password.Text, tb_validate.Text);
                this.Invoke(new Action(delegate ()
                {
                    if (requestResponse.Result)
                    {
                        Properties.Settings.Default.username = tb_username.Text;
                        Properties.Settings.Default.password = tb_password.Text;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("登录成功!");

                        string html = httpHelper.GetInfo();
                        UpdateList(html);
                    }
                    else
                    {
                        MessageBox.Show("登录失败! 错误原因: " + requestResponse.ResponseString);
                    }
                }), new object[0]);
            }).Start();
        }

        private void UpdateList(string html)
        {
            if (html == "") return;
            Regex reg = new Regex(@"<table id[\s\S]*?</table>");
            string table = reg.Match(html).ToString();

            reg = new Regex(@"<tr[\s\S]*?</tr>");

            int i = 0;
            foreach (Match m in reg.Matches(table))
            {
                if (i != 0)
                {
                    Regex regtd = new Regex(@"<td[\s\S]*?</td>");
                    string tr = m.ToString();
                    string[] list = new string[20];
                    int j = 0;
                    foreach (Match m2 in regtd.Matches(tr))
                    {
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
                    Login_List.Items.Add(lt);
                }
                i++;
            }
        }

        private void pictureBox_Code_Click(object sender, EventArgs e)
        {
            new Thread(delegate ()
            {
                Image image = httpHelper.GetValidateImage();
                this.Invoke(new Action(delegate ()
                {
                    pictureBox_Code.Image = image;
                    if (IfLoadLib) tb_validate.Text = Identification();
                    else tb_validate.Text = "加载识别库失败!";
                }), new object[0]);
            }).Start();
        }

        // 识别验证码
        private string Identification()
        {
            byte[] Buffer = ImageToBytes(pictureBox_Code.Image);
            StringBuilder Result = new StringBuilder('\0', 256);
            if (GetImageFromBuffer(Buffer, Buffer.Length, Result))
                return Result.ToString();
            else
                return "请检查是否存在SUDA_WIFI.dat文件";
        }

        // 识别读取验证码byte序列
        private byte[] ImageToBytes(Image image)
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

        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromFile(string FilePath, string Password);

        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromBuffer(byte[] FileBuffer, int ImgBufLen, StringBuilder Vcode);

        [DllImport("WmCode.dll")]
        public static extern bool SetWmOption(int OptionIndex, int OptionValue);

        // 加载验证码识别库
        private void LoadLib()
        {
            try
            {
                if (LoadWmFromFile("SUDA_WIFI.dat", "123456"))
                {
                    SetWmOption(6, 90);
                }
                IfLoadLib = true;
            }
            catch
            {
                IfLoadLib = false;
            }
        }

        private void Login_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = Login_List.HitTest(e.X, e.Y);
            new Thread(delegate ()
            {
                if (info.Item != null)
                {
                    var item = info.Item as ListViewItem;
                    string IP = item.SubItems[2].Text;
                    RequestResponse requestResponse = httpHelper.ManageLogout(IP);

                    this.Invoke(new Action(delegate ()
                    {
                        if (requestResponse.Result)
                        {
                            MessageBox.Show("下线成功!");
                            Login_List.Items.Remove(item);
                        }
                        else
                        {
                            MessageBox.Show("下线失败! 错误原因: " + requestResponse.ResponseString);
                        }
                    }), new object[0]);
                }
            }).Start();
        }
    }
}
