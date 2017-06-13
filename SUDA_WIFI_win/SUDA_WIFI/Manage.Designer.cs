/*
 * 由SharpDevelop创建。
 * 用户： 何彬
 * 日期: 2017/6/13
 * 时间: 20:42
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace SUDA_WIFI
{
	partial class Manage
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button_Login;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label label4;
		
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button_Login = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "帐号：";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "密码：";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(92, 6);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 21);
			this.textBox1.TabIndex = 2;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(92, 33);
			this.textBox2.Name = "textBox2";
			this.textBox2.PasswordChar = '*';
			this.textBox2.Size = new System.Drawing.Size(100, 21);
			this.textBox2.TabIndex = 3;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(92, 62);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(100, 21);
			this.textBox3.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "验证码：";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(198, 62);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(49, 21);
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.PictureBox1Click);
			// 
			// button_Login
			// 
			this.button_Login.Location = new System.Drawing.Point(92, 89);
			this.button_Login.Name = "button_Login";
			this.button_Login.Size = new System.Drawing.Size(75, 23);
			this.button_Login.TabIndex = 7;
			this.button_Login.Text = "登录";
			this.button_Login.UseVisualStyleBackColor = true;
			this.button_Login.Click += new System.EventHandler(this.Button_LoginClick);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2,
			this.columnHeader3,
			this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(12, 118);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(543, 97);
			this.listView1.TabIndex = 8;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView1MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "上线时间";
			this.columnHeader1.Width = 161;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "上线位置";
			this.columnHeader2.Width = 137;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "IP地址";
			this.columnHeader3.Width = 109;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "MAC地址";
			this.columnHeader4.Width = 125;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 94);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "双击下线↓";
			// 
			// Manage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(567, 226);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button_Login);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Manage";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "管理帐号";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
