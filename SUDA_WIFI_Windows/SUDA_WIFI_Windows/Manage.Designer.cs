namespace SUDA_WIFI_Windows
{
    partial class Manage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manage));
            this.Login_List = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_Login = new System.Windows.Forms.Button();
            this.pictureBox_Code = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_validate = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Code)).BeginInit();
            this.SuspendLayout();
            // 
            // Login_List
            // 
            this.Login_List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.Login_List.FullRowSelect = true;
            this.Login_List.Location = new System.Drawing.Point(12, 119);
            this.Login_List.MultiSelect = false;
            this.Login_List.Name = "Login_List";
            this.Login_List.Size = new System.Drawing.Size(543, 97);
            this.Login_List.TabIndex = 18;
            this.Login_List.UseCompatibleStateImageBehavior = false;
            this.Login_List.View = System.Windows.Forms.View.Details;
            this.Login_List.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Login_List_MouseDoubleClick);
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
            // button_Login
            // 
            this.button_Login.Location = new System.Drawing.Point(92, 90);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(75, 23);
            this.button_Login.TabIndex = 17;
            this.button_Login.Text = "登录";
            this.button_Login.UseVisualStyleBackColor = true;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // pictureBox_Code
            // 
            this.pictureBox_Code.Location = new System.Drawing.Point(198, 63);
            this.pictureBox_Code.Name = "pictureBox_Code";
            this.pictureBox_Code.Size = new System.Drawing.Size(49, 21);
            this.pictureBox_Code.TabIndex = 16;
            this.pictureBox_Code.TabStop = false;
            this.pictureBox_Code.Click += new System.EventHandler(this.pictureBox_Code_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 23);
            this.label3.TabIndex = 15;
            this.label3.Text = "验证码：";
            // 
            // tb_validate
            // 
            this.tb_validate.Location = new System.Drawing.Point(92, 63);
            this.tb_validate.Name = "tb_validate";
            this.tb_validate.Size = new System.Drawing.Size(100, 21);
            this.tb_validate.TabIndex = 14;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(92, 34);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(100, 21);
            this.tb_password.TabIndex = 13;
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(92, 7);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(100, 21);
            this.tb_username.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "密码：";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "帐号：";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 23);
            this.label4.TabIndex = 19;
            this.label4.Text = "双击下线↓";
            // 
            // Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 227);
            this.Controls.Add(this.Login_List);
            this.Controls.Add(this.button_Login);
            this.Controls.Add(this.pictureBox_Code);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_validate);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Manage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理帐号";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Code)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView Login_List;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.PictureBox pictureBox_Code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_validate;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}