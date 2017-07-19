using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models.AbstractModel
{
    public abstract class Tb_OperationLog:CustomTable
    {
        public Tb_OperationLog(string logBizType) :
            base(logBizType)
        {
            OtherMessage = string.Empty;
        }
        public string OperationLevel { get; set; }
        public string OperationMessage { get; set; }
        public string OperationBusinessTopic { get; set; }
        public string OtherMessage { get; set; }
    }
}