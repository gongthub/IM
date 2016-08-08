using IM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IM.BLL
{
    public class Server
    {

        /// <summary>
        /// 服务器IP地址
        /// </summary>;
        private string ServerIP;

        /// <summary>
        /// 监听端口
        /// </summary>
        private int ServerPort;
        private TcpListener myListener;

        /// <summary>
        /// 是否正常退出所有接收线程
        /// </summary>
        bool isNormalExit = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Server()
        {
            ServerIP = ConfigurationManager.AppSettings["ServerHost"].ToString();
            ServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["ServerHostPort"].ToString());
        }
         
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Start()
        {
            myListener = new TcpListener(IPAddress.Parse(ServerIP), ServerPort);
            myListener.Start();
            string log = string.Format("开始在{0}:{1}监听客户连接", ServerIP, ServerPort);
            Common.LogHelp.logHelp.WriteLogRedis(log, Model.Enums.LogType.Normal);
            //创建一个线程监客户端连接请求
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }

        /// <summary>
        /// 接收客户端连接
        /// </summary>
        private void ListenClientConnect()
        {
            TcpClient newClient = null;
            while (true)
            {
                try
                {
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    //当单击‘停止监听’或者退出此窗体时 AcceptTcpClient() 会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接收一个客户端连接，就创建一个对应的线程循环接收该客户端发来的信息；
                Model.DTOUser dtouser = new Model.DTOUser(newClient);
                if (dtouser.user == null)
                {
                    dtouser.user = new Model.User();
                }
                Thread threadReceive = new Thread(ReceiveData);
                threadReceive.Start(dtouser);
                DTOUser.Add(dtouser);
                string log = string.Format("[{0}]进入", newClient.Client.RemoteEndPoint);
                Common.LogHelp.logHelp.WriteLogRedis(log, Model.Enums.LogType.Normal);
                log = string.Format("当前连接用户数：{0}", newClient.Client.RemoteEndPoint);
                Common.LogHelp.logHelp.WriteLogRedis(log, Model.Enums.LogType.Normal);
            }

        }


        /// <summary>
        /// 处理接收的客户端信息
        /// </summary>
        /// <param name="userState">客户端信息</param>
        private void ReceiveData(object userState)
        {
            Model.DTOUser dtouser = (Model.DTOUser)userState;
            if (dtouser.user == null)
            {
                dtouser.user = new Model.User(); 
            }
            TcpClient client = dtouser.client;
            while (isNormalExit == false)
            {
                string receiveString = null;
                try
                {
                    //从网络流中读出字符串，此方法会自动判断字符串长度前缀
                    receiveString = dtouser.br.ReadString();
                }
                catch (Exception)
                {
                    if (isNormalExit == false)
                    {
                        string log = string.Format("与[{0}]失去联系，已终止接收该用户信息", client.Client.RemoteEndPoint);
                        Common.LogHelp.logHelp.WriteLogRedis(log, Model.Enums.LogType.Normal);
                        RemoveUser(dtouser);
                    }
                    break;
                }
                Common.LogHelp.logHelp.WriteLogRedis(string.Format("来自[{0}]：{1}", dtouser.client.Client.RemoteEndPoint, receiveString), Model.Enums.LogType.Normal);
                string[] splitString = receiveString.Split(',');
                switch (splitString[0])
                {
                    case "Login":
                        dtouser.user.UserName = splitString[1];
                        SendToAllClient(dtouser, receiveString);
                        break;
                    case "Logout":
                        SendToAllClient(dtouser, receiveString);
                        RemoveUser(dtouser);
                        return;
                    case "Talk":
                        string talkString = receiveString.Substring(splitString[0].Length + splitString[1].Length + 2);
                        Common.LogHelp.logHelp.WriteLogRedis(string.Format("{0}对{1}说：{2}", dtouser.user.UserName, splitString[1], talkString), Model.Enums.LogType.Normal);
                        SendToClient(dtouser, "talk," + dtouser.user.UserName + "," + talkString);
                        List<Model.DTOUser> userList = BLL.DTOUser.GetModels();
                        foreach (Model.DTOUser target in userList)
                        {
                            if (target.user.UserName == splitString[1] && dtouser.user.UserName != splitString[1])
                            {
                                SendToClient(target, "talk," + dtouser.user.UserName + "," + talkString);
                                break;
                            }
                        }
                        break;
                    default:
                        Common.LogHelp.logHelp.WriteLogRedis("什么意思啊：" + receiveString, Model.Enums.LogType.Normal);

                        break;
                }
            }
        }

        /// <summary>
        /// 发送消息给所有客户
        /// </summary>
        /// <param name="user">指定发给哪个用户</param>
        /// <param name="message">信息内容</param>
        private void SendToAllClient(Model.DTOUser dtouser, string message)
        {
            List<Model.DTOUser> userList = BLL.DTOUser.GetModels();
            if (userList != null && userList.Count>0)
            {
                string command = message.Split(',')[0].ToLower();
                if (command == "login")
                {
                    //获取所有客户端在线信息到当前登录用户
                    for (int i = 0; i < userList.Count; i++)
                    {
                        SendToClient(dtouser, "login," + userList[i].user.UserName);
                    }
                    //把自己上线，发送给所有客户端
                    for (int i = 0; i < userList.Count; i++)
                    {
                        if (dtouser.user.UserName != userList[i].user.UserName)
                        {
                            SendToClient(userList[i], "login," + dtouser.user.UserName);
                        }
                    }
                }
                else if (command == "logout")
                {
                    for (int i = 0; i < userList.Count; i++)
                    {
                        if (userList[i].user.UserName != dtouser.user.UserName)
                        {
                            SendToClient(userList[i], message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 发送 message 给 user
        /// </summary>
        /// <param name="user">指定发给哪个用户</param>
        /// <param name="message">信息内容</param>
        private void SendToClient(Model.DTOUser dtouser, string message)
        {
            try
            {
                if (dtouser.user != null)
                {
                    //将字符串写入网络流，此方法会自动附加字符串长度前缀
                    dtouser.bw.Write(message);
                    dtouser.bw.Flush();
                    string log = string.Format("向[{0}]发送：{1}", dtouser.user.UserName, message);
                    Common.LogHelp.logHelp.WriteLogRedis(log, Model.Enums.LogType.Normal);
                }
                else
                { 
                    Common.LogHelp.logHelp.WriteLogRedis("发送失败", Model.Enums.LogType.Normal);
                }
            }
            catch
            {
                string log = string.Format("向[{0}]发送信息失败}", dtouser.user.UserName);
                Common.LogHelp.logHelp.WriteLogRedis(log, Model.Enums.LogType.Normal);
            }
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="user">指定要移除的用户</param>
        private void RemoveUser(Model.DTOUser dtouser)
        {
            if (dtouser.user != null)
            {
                BLL.User.Remove(dtouser.user.UserName);
                dtouser.Close();
            }
            string log = string.Format("当前连接用户数：{0}", BLL.DTOUser.GetCount());
            Common.LogHelp.logHelp.WriteLogRedis(log, Model.Enums.LogType.Normal);
        }

        private delegate void AddItemToListBoxDelegate(string str);

        /// <summary>
        /// 停止监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Stop()
        {
            Common.LogHelp.logHelp.WriteLogRedis("开始停止服务，并依次使用户退出！", Model.Enums.LogType.Normal);
            isNormalExit = true;
            int count = BLL.DTOUser.GetCount();
            List<Model.DTOUser> userList = BLL.DTOUser.GetModels();

            if (userList != null && userList.Count > 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    RemoveUser(userList[i]);
                }
            }
            //通过停止监听让 myListener.AcceptTcpClient() 产生异常退出监听线程
            myListener.Stop();
        } 
    }
}
