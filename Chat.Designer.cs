namespace WinFormsChat1
{
    partial class Chat
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mTxtInputMessage = new System.Windows.Forms.TextBox();
            this.mBtnSendMessage = new System.Windows.Forms.Button();
            this.mTxtChatWindow = new System.Windows.Forms.TextBox();
            this.mTxtMyIP = new System.Windows.Forms.TextBox();
            this.mBtnConnectToServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mBtnStartServer = new System.Windows.Forms.Button();
            this.mTxtServerPortNum = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mTxtServerIP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mTxtMyPortNum = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mTxtInputMessage
            // 
            this.mTxtInputMessage.Location = new System.Drawing.Point(12, 384);
            this.mTxtInputMessage.Multiline = true;
            this.mTxtInputMessage.Name = "mTxtInputMessage";
            this.mTxtInputMessage.Size = new System.Drawing.Size(259, 41);
            this.mTxtInputMessage.TabIndex = 0;

            // 
            // mBtnSendMessage
            // 
            this.mBtnSendMessage.Location = new System.Drawing.Point(277, 384);
            this.mBtnSendMessage.Name = "mBtnSendMessage";
            this.mBtnSendMessage.Size = new System.Drawing.Size(57, 43);
            this.mBtnSendMessage.TabIndex = 1;
            this.mBtnSendMessage.Text = "보내기";
            this.mBtnSendMessage.UseVisualStyleBackColor = true;
            // 
            // mTxtChatWindow
            // 
            this.mTxtChatWindow.Location = new System.Drawing.Point(12, 158);
            this.mTxtChatWindow.Multiline = true;
            this.mTxtChatWindow.Name = "mTxtChatWindow";
            this.mTxtChatWindow.Size = new System.Drawing.Size(322, 220);
            this.mTxtChatWindow.TabIndex = 2;
            // 
            // mTxtMyIP
            // 
            this.mTxtMyIP.Location = new System.Drawing.Point(94, 15);
            this.mTxtMyIP.Name = "mTxtMyIP";
            this.mTxtMyIP.Size = new System.Drawing.Size(137, 20);
            this.mTxtMyIP.TabIndex = 3;
            // 
            // mBtnConnectToServer
            // 
            this.mBtnConnectToServer.Location = new System.Drawing.Point(237, 14);
            this.mBtnConnectToServer.Name = "mBtnConnectToServer";
            this.mBtnConnectToServer.Size = new System.Drawing.Size(79, 47);
            this.mBtnConnectToServer.TabIndex = 4;
            this.mBtnConnectToServer.Text = "서버 접속";
            this.mBtnConnectToServer.UseVisualStyleBackColor = true;
            this.mBtnConnectToServer.Click += new System.EventHandler(this.mBtnConnectToServer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "내 아이피";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "서버 아이피";
            // 
            // mBtnStartServer
            // 
            this.mBtnStartServer.Location = new System.Drawing.Point(237, 14);
            this.mBtnStartServer.Name = "mBtnStartServer";
            this.mBtnStartServer.Size = new System.Drawing.Size(79, 47);
            this.mBtnStartServer.TabIndex = 8;
            this.mBtnStartServer.Text = "서버 시작";
            this.mBtnStartServer.UseVisualStyleBackColor = true;
            this.mBtnStartServer.Click += new System.EventHandler(this.mBtnStartServer_Click);
            // 
            // mTxtServerPortNum
            // 
            this.mTxtServerPortNum.Location = new System.Drawing.Point(94, 40);
            this.mTxtServerPortNum.Name = "mTxtServerPortNum";
            this.mTxtServerPortNum.Size = new System.Drawing.Size(137, 20);
            this.mTxtServerPortNum.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.mTxtServerIP);
            this.groupBox1.Controls.Add(this.mBtnConnectToServer);
            this.groupBox1.Controls.Add(this.mTxtServerPortNum);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 67);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "클라이언트용";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "서버 포트번호";
            // 
            // mTxtServerIP
            // 
            this.mTxtServerIP.Location = new System.Drawing.Point(94, 14);
            this.mTxtServerIP.Name = "mTxtServerIP";
            this.mTxtServerIP.Size = new System.Drawing.Size(137, 20);
            this.mTxtServerIP.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.mTxtMyPortNum);
            this.groupBox2.Controls.Add(this.mTxtMyIP);
            this.groupBox2.Controls.Add(this.mBtnStartServer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 67);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "서버용";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "내 포트번호";
            // 
            // mTxtMyPortNum
            // 
            this.mTxtMyPortNum.Location = new System.Drawing.Point(94, 41);
            this.mTxtMyPortNum.Name = "mTxtMyPortNum";
            this.mTxtMyPortNum.Size = new System.Drawing.Size(137, 20);
            this.mTxtMyPortNum.TabIndex = 7;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 439);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mTxtChatWindow);
            this.Controls.Add(this.mBtnSendMessage);
            this.Controls.Add(this.mTxtInputMessage);
            this.Name = "Chat";
            this.Text = "채팅 프로그램";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mTxtInputMessage;
        private System.Windows.Forms.Button mBtnSendMessage;
        private System.Windows.Forms.TextBox mTxtChatWindow;
        private System.Windows.Forms.TextBox mTxtMyIP;
        private System.Windows.Forms.Button mBtnConnectToServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button mBtnStartServer;
        private System.Windows.Forms.TextBox mTxtServerPortNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox mTxtServerIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox mTxtMyPortNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}