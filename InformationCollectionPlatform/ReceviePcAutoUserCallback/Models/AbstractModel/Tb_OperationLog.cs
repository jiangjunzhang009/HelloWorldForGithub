using System;
using System.Collections.Generic;
using System.Linq;

namespace ReceviePcAutoUserCallback.Models.AbstractModel
{
    public abstract class Tb_OperationLog:CustomTable
    {
        public string OperationLevel { get; set; }
        public string OperationMessage { get; set; }
        public string OperationBusinessTopic { get; set; }
        public string OtherMessage { get; set; }
    }
}