using ReceviePcAutoUserCallback.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Models
{
    /// <summary>
    /// 测试日志表
    /// </summary>
    public class TestTb_ExceptionLog:CustomTable
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 描述消息
        /// </summary>
        public string LogMessage { get; set; }
        /// <summary>
        /// 业务主题
        /// </summary>
        public string LogTopic { get; set; }
        /// <summary>
        /// 附件信息
        /// </summary>
        public byte[] OtherMessage { get; set; }
    }
}
