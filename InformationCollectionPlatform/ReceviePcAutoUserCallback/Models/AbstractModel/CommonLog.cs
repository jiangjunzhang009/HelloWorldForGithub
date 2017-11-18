using System;
using System.Collections.Generic;
using System.Linq;

namespace ReceviePcAutoUserCallback.Models.AbstractModel
{
    public abstract class CommonLog : CustomTable
    {
        public string LogLevel { get; set; }
        public string LogTopic { get; set; }
        public string LogMessage { get; set; }
        public byte[] OtherMessage { get; set; }
    }
}