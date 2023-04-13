using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace socket_serve
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //System.Windows.Forms.Timer timer { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    txtIP.Text = ipa.ToString();
            }
            txtPort.Text = "50000";
            //timer = new System.Windows.Forms.Timer();
            //timer.Enabled = false;
            //timer.Interval = 10000;
            //timer.Tick += (o, t) =>
            //{
            //    try
            //    {
            //        SetUserList();
            //    }
            //    catch (Exception ex)
            //    {
            //        ShowMsg(ex.Message);
            //    }
            //    finally
            //    {
            //        timer.Enabled = true;
            //    }
            //};
        }
        /*
         服务器端需要：
           1.申请一个socket
           2.绑定IP和端口
           3.开启监听，等待接收连接
        */
        private void btndemo_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(txtIP.Text);
            IPEndPoint point = new IPEndPoint(ip, int.Parse(txtPort.Text));//端口号在1-65535之间，最好在1024以后

            //使用IPv4地址，流式socket方式，tcp协议传递数据
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //创建好socket后，必须告诉socket绑定的ip地址和端口号
            //让socket监听point
            try
            {
                //socket监听那个端口
                socket.Bind(point);
                //同一个时间过来10个客户端，排队
                socket.Listen(10);
                ShowMsg("服务器开始监听");
                Thread thread = new Thread(AcceptInfo);
                thread.IsBackground = true;
                thread.Start(socket);
                //timer.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// 记录通信用的Socket
        /// </summary>
        Dictionary<string, Socket> dic = new Dictionary<string, Socket>();

        void AcceptInfo(object o)
        {
            Socket socket = o as Socket;
            while (true)
            {
                //通信用Socket
                try
                {
                    //创建通信用的socket
                    Socket tsocket = socket.Accept();
                    string point = tsocket.RemoteEndPoint.ToString();
                    ShowMsg(point + "连接成功！");
                    dic.Add(point, tsocket);
                    lisip.Items.Add(point);
                    SetUserList();

                    //接收消息
                    Thread th = new Thread(ReceiveMsg);
                    th.IsBackground = true;
                    th.Start(tsocket);
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message);
                    break;
                }
            }

        }
        /// <summary>
        /// 接收消息
        /// </summary>
        void ReceiveMsg(object o)
        {
            Socket socket = o as Socket;
            while (true)
            {
                
                //接收客户端发送过来的数据
                try
                {
                    //定义byte数组存放从客户端接收过来的数据
                    byte[] buffer = new byte[1024 * 1024];
                    //将接收过来的数据放到buffer中，并返回实际接受数据的长度
                    int n = socket.Receive(buffer);
                    //字节转字符串
                    string words = Encoding.UTF8.GetString(buffer, 0, n);

                    ShowMsg(socket.RemoteEndPoint.ToString() + ":" + words);

                    string userip = words.Split('|').ToList().First();
                    string text = words.Replace(words.Substring(0,userip.Length)+"|",string.Empty);
                    buffer = Encoding.UTF8.GetBytes(text);
                    dic[userip].Send(buffer);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "远程主机强迫关闭了一个现有的连接。")
                    {
                        lisip.Items.Remove(socket.RemoteEndPoint.ToString());
                        dic.Remove(socket.RemoteEndPoint.ToString());
                        SetUserList();
                        ShowMsg(socket.RemoteEndPoint.ToString() +":"+ex.Message);
                    }
                    else
                    {
                        ShowMsg(ex.Message);
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// 通知所有客户端在线人数
        /// </summary>
        public void SetUserList()
        {
            if (lisip.Items.Count == 0) return;
            List<string> lis = new List<string>();
            foreach (string item in lisip.Items)
            {
                lis.Add(item.ToString());
            }
            //在通知各个客户端发送在线信息信息
                foreach (string item in lis)
                {
                    try
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes("%在..//`''线$#人$%数：" + string.Join(";", lis));
                         dic[item].Send(buffer);
                    }
                    catch (Exception ex)
                    {
                        ShowMsg(ex.Message);
                    }
                }
           
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (lisip == null || lisip.SelectedIndex < 0)
                {
                    ShowMsg("请先选择需要发送的ip地址！");
                    return;
                }
                //timer.Enabled = false;
                ShowMsg("Mark_Serve:" + txtServe.Text);
                byte[] buffer = Encoding.UTF8.GetBytes(txtServe.Text);
                dic[lisip.Text].Send(buffer);
                txtServe.Text = string.Empty;
                //timer.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
