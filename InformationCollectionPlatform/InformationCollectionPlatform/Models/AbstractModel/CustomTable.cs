using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Models.AbstractModel
{
    public abstract class CustomTable
    {
        public string LogBizType { get; set; }
        public CustomTable(string logBizType)
        {
            LogBizType = LogBizType;
        }
        public int Id { get; set; }
        public DateTime CreateDatetime { get; set; }

    }
}