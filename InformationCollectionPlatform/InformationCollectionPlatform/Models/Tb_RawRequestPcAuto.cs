using InformationCollectionPlatform.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models
{
    public class Tb_RawRequestPcAuto: Tb_RawRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logBizType">PcAuto</param>
        public Tb_RawRequestPcAuto(string logBizType = "PcAuto") :
            base(logBizType)
        { }
    }
}