using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models.AbstractModel
{
    public abstract class Tb_ExceptionLog : CustomTable
    {
        public Tb_ExceptionLog(string logBizType):
            base(logBizType)
        {
            OtherMessage = string.Empty;
        }
        public string LogLevel { get; set; }
        public string LogMessage { get; set; }
        public string LogBusinessTopic { get; set; }
        public string OtherMessage { get; set; }
    }
}