namespace SUDA_WIFI_Windows
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.checkbox_autostart = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_State = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar_Interval = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Manage = new System.Windows.Forms.Button();
            this.btn_Login = new System.Windows.Forms.Button();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btn_Fee = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Interval)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Stop
            // 
            this.btn_Stop.Enabled = false;
            this.btn_Stop.Location = new System.Drawing.Point(125, 92);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 12;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(8, 92);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 11;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // checkbox_autostart
            // 
            this.checkbox_autostart.Location = new System.Drawing.Point(103, 39);
            this.checkbox_autostart.Name = "checkbox_autostart";
            this.checkbox_autostart.Size = new System.Drawing.Size(38, 24);
            this.checkbox_autostart.TabIndex = 10;
            this.checkbox_autostart.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "开机自启：";
            // 
            // lb_State
            // 
            this.lb_State.Location = new System.Drawing.Point(101, 21);
            this.lb_State.Name = "lb_State";
            this.lb_State.Size = new System.Drawing.Size(99, 23);
            this.lb_State.TabIndex = 2;
            this.lb_State.Text = "断开";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "当前状态：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.trackBar_Interval);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_Stop);
            this.groupBox1.Controls.Add(this.btn_Start);
            this.groupBox1.Controls.Add(this.checkbox_autostart);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lb_State);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 124);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "高级功能——自动拨号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "1s";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // trackBar_Interval
            // 
            this.trackBar_Interval.AutoSize = false;
            this.trackBar_Interval.Location = new System.Drawing.Point(60, 64);
            this.trackBar_Interval.Maximum = 300;
            this.trackBar_Interval.Minimum = 1;
            this.trackBar_Interval.Name = "trackBar_Interval";
            this.trackBar_Interval.Size = new System.Drawing.Size(99, 20);
            this.trackBar_Interval.TabIndex = 19;
            this.trackBar_Interval.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Interval.Value = 1;
            this.trackBar_Interval.ValueChanged += new System.EventHandler(this.trackBar_Interval_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "拨号间隔：";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "苏大自动拨号";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // btn_Manage
            // 
            this.btn_Manage.Location = new System.Drawing.Point(161, 70);
            this.btn_Manage.Name = "btn_Manage";
            this.btn_Manage.Size = new System.Drawing.Size(59, 23);
            this.btn_Manage.TabIndex = 15;
            this.btn_Manage.Text = "管理";
            this.btn_Manage.UseVisualStyleBackColor = true;
            this.btn_Manage.Click += new System.EventHandler(this.btn_Manage_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(14, 70);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(59, 23);
            this.btn_Login.TabIndex = 14;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(65, 40);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(155, 21);
            this.tb_password.TabIndex = 13;
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(65, 12);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(155, 21);
            this.tb_username.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "密码：";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "帐号：";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btn_Fee
            // 
            this.btn_Fee.Location = new System.Drawing.Point(87, 70);
            this.btn_Fee.Name = "btn_Fee";
            this.btn_Fee.Size = new System.Drawing.Size(59, 23);
            this.btn_Fee.TabIndex = 17;
            this.btn_Fee.Text = "费用";
            this.btn_Fee.UseVisualStyleBackColor = true;
            this.btn_Fee.Click += new System.EventHandler(this.btn_Fee_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 248);
            this.Controls.Add(this.btn_Fee);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Manage);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUDA_WIFI";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Interval)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.CheckBox checkbox_autostart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_State;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btn_Manage;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBar_Interval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Button btn_Fee;
    }
}

