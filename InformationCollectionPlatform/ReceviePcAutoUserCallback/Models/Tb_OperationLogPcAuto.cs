using ReceviePcAutoUserCallback.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReceviePcAutoUserCallback.Models
{
    public class Tb_OperationLogPcAuto : Tb_OperationLog
    {
        public Tb_OperationLogPcAuto()
        {
            OperationBusinessTopic = string.Empty;
            OtherMessage = string.Empty;
        }
    }
}