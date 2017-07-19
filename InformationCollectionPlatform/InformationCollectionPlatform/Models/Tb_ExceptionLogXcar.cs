using InformationCollectionPlatform.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models
{
    public class Tb_ExceptionLogXcar : Tb_ExceptionLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logBizType">Xcar</param>
        public Tb_ExceptionLogXcar(string logBizType = "Xcar") :
            base(logBizType)
        { }
    }
}