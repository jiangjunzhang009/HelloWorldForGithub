using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models.AbstractModel
{
    public abstract class Tb_RawRequest:CustomTable
    {
        public Tb_RawRequest(string logBizType) :
            base(logBizType)
        {
            OtherMessage = string.Empty;
        }
        public string RequestUrl { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string OtherMessage { get; set; }
    }
}