using IM.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IM.WinServer
{
    public partial class Main : Form
    {
        //注册log改变事件
        private BLL.LogListen logListen = new BLL.LogListen();

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            logListen.AddLogsChangeHandler(OnChange_Logs);
            string logs = LogHelp.logHelp.GetLogsRedis(20);
            txtShow.Text = logs;
        }

        #region Logs发生改变事件 -void OnChange_Logs(object sender, EventArgs e)
        /// <summary>
        /// Logs发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChange_Logs(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            string logs = Common.LogHelp.logHelp.GetLogsRedis(20);
            txtShow.Text = logs;
            BLL.LogListen.NewTime = DateTime.Now;
        }
        #endregion

       /// <summary>
       /// 开始事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            BLL.Server server = new BLL.Server();
            server.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// 结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            BLL.Server server = new BLL.Server();
            server.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
