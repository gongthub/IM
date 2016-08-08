namespace SyncChatClient
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lst_OnlineUser = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txt_SendText = new System.Windows.Forms.TextBox();
            this.btn_LoadOnlineUser = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(80, 25);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(277, 21);
            this.txt_UserName.TabIndex = 1;
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(363, 23);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "登陆";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Message);
            this.groupBox1.Location = new System.Drawing.Point(35, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 244);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "对话信息";
            // 
            // txt_Message
            // 
            this.txt_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Message.Location = new System.Drawing.Point(3, 17);
            this.txt_Message.Multiline = true;
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(400, 224);
            this.txt_Message.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lst_OnlineUser);
            this.groupBox2.Location = new System.Drawing.Point(456, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 301);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "当前在线";
            // 
            // lst_OnlineUser
            // 
            this.lst_OnlineUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_OnlineUser.FormattingEnabled = true;
            this.lst_OnlineUser.ItemHeight = 12;
            this.lst_OnlineUser.Location = new System.Drawing.Point(3, 17);
            this.lst_OnlineUser.Name = "lst_OnlineUser";
            this.lst_OnlineUser.Size = new System.Drawing.Size(163, 280);
            this.lst_OnlineUser.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_Send);
            this.groupBox3.Controls.Add(this.txt_SendText);
            this.groupBox3.Location = new System.Drawing.Point(35, 302);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 51);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送信息";
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(312, 18);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 1;
            this.btn_Send.Text = "发送";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txt_SendText
            // 
            this.txt_SendText.Location = new System.Drawing.Point(6, 18);
            this.txt_SendText.Name = "txt_SendText";
            this.txt_SendText.Size = new System.Drawing.Size(282, 21);
            this.txt_SendText.TabIndex = 0;
            this.txt_SendText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_SendText_KeyPress);
            // 
            // btn_LoadOnlineUser
            // 
            this.btn_LoadOnlineUser.Location = new System.Drawing.Point(503, 23);
            this.btn_LoadOnlineUser.Name = "btn_LoadOnlineUser";
            this.btn_LoadOnlineUser.Size = new System.Drawing.Size(75, 23);
            this.btn_LoadOnlineUser.TabIndex = 6;
            this.btn_LoadOnlineUser.Text = "刷新";
            this.btn_LoadOnlineUser.UseVisualStyleBackColor = true;
            this.btn_LoadOnlineUser.Click += new System.EventHandler(this.btn_LoadOnlineUser_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 358);
            this.Controls.Add(this.btn_LoadOnlineUser);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.txt_UserName);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lst_OnlineUser;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txt_SendText;
        private System.Windows.Forms.Button btn_LoadOnlineUser;
    }
}

