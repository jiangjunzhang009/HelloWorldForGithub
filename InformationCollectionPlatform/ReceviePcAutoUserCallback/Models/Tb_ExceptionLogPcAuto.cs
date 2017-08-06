using ReceviePcAutoUserCallback.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReceviePcAutoUserCallback.Models
{
    public class Tb_ExceptionLogPcAuto : Tb_ExceptionLog
    {
        public Tb_ExceptionLogPcAuto()
        {
            LogBusinessTopic = string.Empty;
            OtherMessage = string.Empty;
        }
    }
}