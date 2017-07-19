using InformationCollectionPlatform.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models
{
    public class Tb_OperationLogXcar: Tb_OperationLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logBizType">Xcar</param>
        public Tb_OperationLogXcar(string logBizType = "Xcar") :
            base(logBizType)
        { }
    }
}