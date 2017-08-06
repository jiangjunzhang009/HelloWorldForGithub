using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Models.DbModel
{
    public class ReceiveResultTemplateModel
    {
        public ReceiveResultTemplateModel()
        {
            Id = 0;
        }
        public int OperationId { get; set; }
        [Key]
        public int Id { get; set; }
        public string TaskId { get; set; }
        public string TaskGroupId { get; set; }
        public string CompatibleTaskId { get; set; }
        public string RuntimeRefUrl { get; set; }
        public string RuntimeHttpMocker { get; set; }
        public string RuntimeFirstPageUrl { get; set; }
        public string PcAutoUserId { get; set; }
        public int IsMultiPage { get; set; }
        public string CrawledDataUrl { get; set; }
        public string CrawledDataContent { get; set; }
        public string CrawledDataTime { get; set; }
        public DateTime CreateDatetime { get; set; }
        public string OtherMessage { get; set; }
    }
}
