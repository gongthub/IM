using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.Model
{
    public class Enums
    {
        
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            [Description("正常")]
            Normal = 0,
            [Description("错误")]
            Error = 1
        }
    }
}
