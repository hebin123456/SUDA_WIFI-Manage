/*
 * 由SharpDevelop创建。
 * 用户： 以夕阳落款
 * 日期: 2017/6/29
 * 时间: 20:38
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace 自动拨号
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button2;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "自动拨号";
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "当前连接状态：";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(107, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "断开";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(26, 112);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "开始";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(60, 35);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(155, 21);
			this.textBox1.TabIndex = 3;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(60, 72);
			this.textBox2.Name = "textBox2";
			this.textBox2.PasswordChar = '*';
			this.textBox2.Size = new System.Drawing.Size(155, 21);
			this.textBox2.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "账号：";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 23);
			this.label4.TabIndex = 6;
			this.label4.Text = "密码：";
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(122, 112);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "停止";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(227, 147);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "自动拨号";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.SizeChanged += new System.EventHandler(this.MainFormSizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
