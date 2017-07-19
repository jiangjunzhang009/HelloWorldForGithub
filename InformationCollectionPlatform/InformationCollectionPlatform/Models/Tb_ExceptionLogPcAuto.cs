using InformationCollectionPlatform.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models
{
    public class Tb_ExceptionLogPcAuto : Tb_ExceptionLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logBizType">"PcAuto"</param>
        public Tb_ExceptionLogPcAuto(string logBizType = "PcAuto") :
            base(logBizType)
        { }
    }
}