using System;
using System.Collections.Generic;
using System.Linq;

namespace ReceviePcAutoUserCallback.Models.AbstractModel
{
    public abstract class Tb_ExceptionLog : CustomTable
    {
        public string LogLevel { get; set; }
        public string LogMessage { get; set; }
        public string LogBusinessTopic { get; set; }
        public string OtherMessage { get; set; }
    }
}