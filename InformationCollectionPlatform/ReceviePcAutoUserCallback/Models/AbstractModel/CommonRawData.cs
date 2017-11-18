using System;
using System.Collections.Generic;
using System.Linq;

namespace ReceviePcAutoUserCallback.Models.AbstractModel
{
    public abstract class CommonRawData : CustomTable
    {
        public int OperationId { get; set; }
        public string TaskId { get; set; }
        public string TaskGroupId { get; set; }
        public string CompatibleTaskId { get; set; }
        public string RuntimeRefUrl { get; set; }
        public string RuntimeHttpMocker { get; set; }
        public string RuntimeFirstPageUrl { get; set; }
        public string PcAutoUserId { get; set; }
        public int IsMultiPage { get; set; }
        public int PageNo { get; set; }
        public string CrawledDataUrl { get; set; }
        public byte[] CrawledDataContent { get; set; }
        public string CrawledDataTime { get; set; }
        public byte[] OtherMessage { get; set; }
    }
}