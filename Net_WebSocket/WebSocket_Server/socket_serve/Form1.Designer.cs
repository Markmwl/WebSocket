namespace socket_serve
{
    partial class Form1
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
            this.btndemo = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtServe = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lisip = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btndemo
            // 
            this.btndemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btndemo.Location = new System.Drawing.Point(430, 26);
            this.btndemo.Name = "btndemo";
            this.btndemo.Size = new System.Drawing.Size(151, 24);
            this.btndemo.TabIndex = 0;
            this.btndemo.Text = "开启侦听";
            this.btndemo.UseVisualStyleBackColor = true;
            this.btndemo.Click += new System.EventHandler(this.btndemo_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(188, 77);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(559, 146);
            this.txtMsg.TabIndex = 1;
            // 
            // txtServe
            // 
            this.txtServe.Location = new System.Drawing.Point(188, 243);
            this.txtServe.Multiline = true;
            this.txtServe.Name = "txtServe";
            this.txtServe.Size = new System.Drawing.Size(559, 111);
            this.txtServe.TabIndex = 1;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(188, 376);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(559, 52);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "发送";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(188, 26);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 21);
            this.txtIP.TabIndex = 3;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(309, 26);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 3;
            // 
            // lisip
            // 
            this.lisip.FormattingEnabled = true;
            this.lisip.ItemHeight = 12;
            this.lisip.Location = new System.Drawing.Point(13, 26);
            this.lisip.Name = "lisip";
            this.lisip.Size = new System.Drawing.Size(152, 400);
            this.lisip.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 450);
            this.Controls.Add(this.lisip);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtServe);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btndemo);
            this.Name = "Form1";
            this.Text = "Serve";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btndemo;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtServe;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ListBox lisip;
    }
}

