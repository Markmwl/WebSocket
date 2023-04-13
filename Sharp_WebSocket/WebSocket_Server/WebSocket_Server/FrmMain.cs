using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocket_Server
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        WebSocketServer WS = null;
        Func<CommonUser> func = null;
        CommonUser cu = null;

        public delegate void ActionT<in T>(T t);

        public void ActionDesc(string t)
        {
            txtDesc.Text += t;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cbxUser.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (WS != null)
                {
                    txtDesc.Text += "侦听已开启！" + "\r\n";
                    return;
                }
                WS = new WebSocketServer("ws://localhost:8045/");
                func = (() => {
                    if (cu == null)
                    {
                        cu = new CommonUser();
                        cu.Message += (o, es) =>
                        {
                            new Thread(() =>
                            {
                                ActionT<string> a = new ActionT<string>(ActionDesc);
                                Invoke(a, es.Result);

                            }).Start();
                        };
                    }
                    return cu;
                });
                WS.AddWebSocketService("/Mark", func);

                WS.AddWebSocketService<CommonUser>("/Zx");

                WS.Start();

                txtDesc.Text += "开启侦听成功！" + "\r\n";
            }
            catch (Exception ex)
            {
                txtDesc.Text += "侦听开启异常！原因：" + ex.Message + "\r\n";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (WS != null && func != null)
            {
                func.Invoke().SendMsg(txtInput.Text);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (WS != null)
            {
                WS.Stop();
                txtDesc.Text += "侦听已关闭！" + "\r\n";
                WS = null;
            }
        }
    }
}
