using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace socket_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            txtip.Text = "116.62.18.134";
            txtport.Text = "50000";
        }

        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void btnCon_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(txtip.Text);
            IPEndPoint point = new IPEndPoint(ip, int.Parse(txtport.Text));//端口号在1-65535之间，最好在1024以后

            //使用IPv4地址，流式socket方式，tcp协议传递数据

            //创建好socket后，必须告诉socket绑定的ip地址和端口号
            //让socket监听point
            try
            {
                //连接到服务器
                client.Connect(point);
                ///ShowMsg("连接成功！");
                ShowMsg("您已上线！");
                ShowMsg("服务器：" + client.RemoteEndPoint.ToString());
                ShowMsg("客户端：" + client.LocalEndPoint.ToString());
                //连接成功后就可以接受服务器发送的信息了
                Thread thread = new Thread(ReceiveMsg);
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }
        //接收服务器发送的信息
        void ReceiveMsg()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int n = client.Receive(buffer);
                    string s = Encoding.UTF8.GetString(buffer, 0, n);
                    if (s.Contains("%在..//`''线$#人$%数："))
                    {
                        var lis = s.Replace("%在..//`''线$#人$%数：", string.Empty).Split(';').ToList();
                        lbxuser.Items.Clear();
                        if (lis.Count() > 0)
                        {
                            string port = client.LocalEndPoint.ToString().Substring(client.LocalEndPoint.ToString().Length - 5, 5);
                            foreach (string item in lis)
                            {
                                if (!item.Contains(port))
                                {
                                    lbxuser.Items.Add(item);
                                }
                            }
                           
                            lbxuser.Items.Remove(client.LocalEndPoint.ToString()); 
                        }
                        usernumber.Text = lis.Count() > 0 ? (lis.Count() -1).ToString() : "0";
                    }
                    else
                    {
                        //ShowMsg(client.RemoteEndPoint.ToString() + ":" + s);
                        ShowMsg(client.RemoteEndPoint.ToString() +" "+System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        ShowMsg("   " + s);
                    }
                   
                }
                catch (Exception ex)
                {
                    if (ex.Message == "你的主机中的软件中止了一个已建立的连接。")
                    {
                        ShowMsg("您已下线！");
                    }
                    else
                    {
                        ShowMsg(ex.Message);
                    }
                    break;
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                try
                {
                    if (lbxuser.Items.Count == 0 || lbxuser.SelectedIndex < 0)
                    {
                        MessageBox.Show("请先选择发送人！");
                        return;
                    }
                    ShowMsg("Mark_Client:" + txtClient.Text);
                    byte[] buffer = Encoding.UTF8.GetBytes(lbxuser.SelectedItem.ToString() +"|"+ txtClient.Text);
                    client.Send(buffer);
                    //SocketAsyncEventArgs eventArgs = new SocketAsyncEventArgs();
                    //eventArgs.SetBuffer(buffer,100,100000);
                    //client.SendAsync(eventArgs);
                    txtClient.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message);
                }
            }
        }
        void ShowMsg(string msg)
        {
            try
            {
                txtMsg.AppendText(msg + "\r\n");
            }
            catch
            {

            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                //client.Shutdown(SocketShutdown.Both);
                client.Close();
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtClient.Text = string.Empty;
        }
    }
}
