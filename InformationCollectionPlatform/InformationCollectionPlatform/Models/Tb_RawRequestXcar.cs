using InformationCollectionPlatform.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models
{
    public class Tb_RawRequestXcar:Tb_RawRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logBizType">Xcar</param>
        public Tb_RawRequestXcar(string logBizType= "Xcar") :
            base(logBizType)
        { }
    }
}