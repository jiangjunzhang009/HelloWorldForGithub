using InformationCollectionPlatform.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models
{
    public class Tb_OperationLogPcAuto : Tb_OperationLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logBizType">PcAuto</param>
        public Tb_OperationLogPcAuto(string logBizType = "PcAuto") :
            base(logBizType)
        { }
    }
}