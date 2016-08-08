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
using System.IO;
using System.Threading;

namespace SyncChatClient
{
    public partial class MainForm : Form
    {
        private string ServerIP; //IP
        private int port;   //端口
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        public MainForm()
        {
            InitializeComponent();
            Random r = new Random((int)DateTime.Now.Ticks);
            txt_UserName.Text = "user" + r.Next(100, 999);
            lst_OnlineUser.HorizontalScrollbar = true;
            SetServerIPAndPort();
        }

        /// <summary>
        /// 根据当前程序目录的文本文件‘ServerIPAndPort.txt’内容来设定IP和端口
        /// 格式：127.0.0.1:8885
        /// </summary>
        private void SetServerIPAndPort()
        {
            try
            {
                FileStream fs = new FileStream("ServerIPAndPort.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string IPAndPort = sr.ReadLine();
                ServerIP = IPAndPort.Split(':')[0]; //设定IP
                port = int.Parse(IPAndPort.Split(':')[1]); //设定端口
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置IP与端口失败，错误原因：" + ex.Message);
                Application.Exit();
            }
        }

        /// <summary>
        /// 【登陆】按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {
            btn_Login.Enabled = false;
            try
            {
                //此处为方便演示，实际使用时要将Dns.GetHostName()改为服务器域名
                //IPAddress ipAd = IPAddress.Parse("182.150.193.7");
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ServerIP), port);
                AddTalkMessage("连接成功");
            }
            catch(Exception ex)
            {
                AddTalkMessage("连接失败，原因：" + ex.Message);
                btn_Login.Enabled = true;
                return;
            }
            //获取网络流
            NetworkStream networkStream = client.GetStream();
            //将网络流作为二进制读写对象
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            SendMessage("Login," + txt_UserName.Text);
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }

        /// <summary>
        /// 处理服务器信息
        /// </summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去连接");
                    }
                    break;
                }
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "login":   //格式： login,用户名
                        AddOnline(splitString[1]);
                        break;
                    case "logout":  //格式： logout,用户名
                        RemoveUserName(splitString[1]);
                        break;
                    case "talk":    //格式： talk,用户名,对话信息
                        AddTalkMessage(splitString[1] + "：\r\n");
                        AddTalkMessage(receiveString.Substring(splitString[0].Length + splitString[1].Length+2));
                        break;
                    default:
                        AddTalkMessage("什么意思啊：" + receiveString);
                        break;
                }
            }
            Application.Exit();
        }

        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddTalkMessage("发送失败");
            }
        }

        private delegate void AddTalkMessageDelegate(string message);
        /// <summary>
        /// 在聊天对话框（txt_Message）中追加聊天信息
        /// </summary>
        /// <param name="message"></param>
        private void AddTalkMessage(string message)
        {
            if (txt_Message.InvokeRequired)
            {
                AddTalkMessageDelegate d = new AddTalkMessageDelegate(AddTalkMessage);
                txt_Message.Invoke(d, new object[] { message });
            }
            else
            {
                txt_Message.AppendText(message + Environment.NewLine);
                txt_Message.ScrollToCaret();
            }
        }

        private delegate void AddOnlineDelegate(string message);
        /// <summary>
        /// 在在线框（lst_Online)中添加其他客户端信息
        /// </summary>
        /// <param name="userName"></param>
        private void AddOnline(string userName)
        {
            if (lst_OnlineUser.InvokeRequired)
            {
                AddOnlineDelegate d = new AddOnlineDelegate(AddOnline);
                lst_OnlineUser.Invoke(d, new object[] { userName });
            }
            else
            {
                lst_OnlineUser.Items.Add(userName);
                lst_OnlineUser.SelectedIndex = lst_OnlineUser.Items.Count - 1;
                lst_OnlineUser.ClearSelected();
            }
        }

        private delegate void RemoveUserNameDelegate(string userName);
        /// <summary>
        /// 在在线框(lst_Online)中移除不在线的客户端信息
        /// </summary>
        /// <param name="userName"></param>
        private void RemoveUserName(string userName)
        {
            if (lst_OnlineUser.InvokeRequired)
            {
                RemoveUserNameDelegate d = new RemoveUserNameDelegate(RemoveUserName);
                lst_OnlineUser.Invoke(d, userName);
            }
            else
            {
                lst_OnlineUser.Items.Remove(userName);
                lst_OnlineUser.SelectedIndex = lst_OnlineUser.Items.Count - 1;
                lst_OnlineUser.ClearSelected();
            }
        }

        /// <summary>
        /// 【发送】按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            if (lst_OnlineUser.SelectedIndex != -1)
            {
                SendMessage("Talk," + lst_OnlineUser.SelectedItem + "," + txt_SendText.Text + "\r\n");
                txt_SendText.Clear();
            }
            else
            {
                MessageBox.Show("请先在【当前在线】中选择一个对话着");
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未与服务器连接前 client 为 null
            if (client != null)
            {
                try
                {
                    SendMessage("Logout," + txt_UserName.Text);
                    isExit = true;
                    br.Close();
                    bw.Close();
                    client.Close();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 在发送信息的文本框中按下【Enter】键触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_SendText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                //触发【发送】按钮的单击事件
                btn_Send.PerformClick();
            }
        }

        private void btn_LoadOnlineUser_Click(object sender, EventArgs e)
        {

        }


    }
}
