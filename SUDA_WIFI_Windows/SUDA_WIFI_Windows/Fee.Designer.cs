namespace SUDA_WIFI_Windows
{
    partial class Fee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fee));
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Pay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.tb_Username = new System.Windows.Forms.TextBox();
            this.tb_Account = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(14, 104);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 0;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Pay
            // 
            this.btn_Pay.Location = new System.Drawing.Point(139, 104);
            this.btn_Pay.Name = "btn_Pay";
            this.btn_Pay.Size = new System.Drawing.Size(75, 23);
            this.btn_Pay.TabIndex = 1;
            this.btn_Pay.Text = "充值";
            this.btn_Pay.UseVisualStyleBackColor = true;
            this.btn_Pay.Click += new System.EventHandler(this.btn_Pay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "姓名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "帐户：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "余额：";
            // 
            // tb_Name
            // 
            this.tb_Name.Location = new System.Drawing.Point(59, 12);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.ReadOnly = true;
            this.tb_Name.Size = new System.Drawing.Size(155, 21);
            this.tb_Name.TabIndex = 5;
            // 
            // tb_Username
            // 
            this.tb_Username.Location = new System.Drawing.Point(59, 39);
            this.tb_Username.Name = "tb_Username";
            this.tb_Username.ReadOnly = true;
            this.tb_Username.Size = new System.Drawing.Size(155, 21);
            this.tb_Username.TabIndex = 6;
            // 
            // tb_Account
            // 
            this.tb_Account.Location = new System.Drawing.Point(59, 66);
            this.tb_Account.Name = "tb_Account";
            this.tb_Account.ReadOnly = true;
            this.tb_Account.Size = new System.Drawing.Size(155, 21);
            this.tb_Account.TabIndex = 7;
            // 
            // Fee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 138);
            this.Controls.Add(this.tb_Account);
            this.Controls.Add(this.tb_Username);
            this.Controls.Add(this.tb_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Pay);
            this.Controls.Add(this.btn_Refresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "费用查询";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Pay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Name;
        private System.Windows.Forms.TextBox tb_Username;
        private System.Windows.Forms.TextBox tb_Account;
    }
}