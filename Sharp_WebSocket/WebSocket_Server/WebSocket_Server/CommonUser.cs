using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocket_Server
{
    public class CommonUser : WebSocketBehavior
    {
        /// <summary>
        /// 事件委托
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        public delegate void MessageEventHandler(object obj, ApiMessageEventArgs args);

        public event MessageEventHandler Message;

        protected override void OnMessage(MessageEventArgs e)
        {
            if ("Mark".Equals(e.Data) || "Zx".Equals(e.Data))
            {
                Send("请求连接成功！用户：" + e.Data);
            }
            else
            {
                //Send(e.Data);
            }

            if (Message != null)
            {
                string msg = DateTime.Now + "本地端消息：" + e.Data + "\r\n";
                Message.Invoke(this, new ApiMessageEventArgs() { Result = msg });
            }
        }

        public void SendMsg(string msg)
        {
            Send(msg);
        }
    }

    public class ApiMessageEventArgs : EventArgs
    {
        public string Result { get; set; }
    }
}
