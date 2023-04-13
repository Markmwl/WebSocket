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

namespace WebSocket_Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        public delegate void ActionT<in T>(T t);

        private WebSocket WS = null;

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cbxUser.SelectedIndex = 0;
        }

        public void ActionCom(string t)
        {
            txtCom.Text += t;
        }

        private void btnCon_Click(object sender, EventArgs e)
        {
            try
            {
                var ws = new WebSocket("ws://localhost:8045/" + cbxUser.Text);
                ws.Connect();

                ws.OnOpen += (s, es) =>
                {
                    new Thread(() =>
                    {
                        ActionT<string> a = new ActionT<string>(ActionCom);
                        Invoke(a, DateTime.Now + "连接成功！" + "\r\n");

                    }).Start();
                };

                ws.OnError += (s, es) => Console.WriteLine("错误：" + es.Message);

                ws.OnMessage += (s, es) =>
                {
                    new Thread(() =>
                    {
                        ActionT<string> a = new ActionT<string>(ActionCom);
                        Invoke(a, DateTime.Now + "服务端消息：" + es.Data + "\r\n");

                    }).Start();
                };

                ws.Send(cbxUser.Text);

                WS = ws;
            }
            catch (Exception ex)
            {

                txtCom.Text +="连接异常，原因：" + ex.Message + "\r\n";
            }
           
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (WS != null)
                {
                    WS.Send(txtMsg.Text);
                }
                else
                {
                    txtCom.Text += "请先打开连接！" + "\r\n";
                }
            }
            catch (Exception ex)
            {
                txtCom.Text += "连接异常！原因：" + ex.Message + "\r\n";
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (WS != null)
            {
                WS.Close();
                txtCom.Text += "连接已关闭！"+"\r\n";
                WS = null;
            }
        }
    }
}
