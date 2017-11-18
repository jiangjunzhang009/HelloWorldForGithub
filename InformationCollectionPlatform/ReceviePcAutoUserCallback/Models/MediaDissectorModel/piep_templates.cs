using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Models.MediaDissectorModel
{
    public class piep_templates
    {
        public int id { get; set; }
        public string domain { get; set; }
        public string url_regex_rule { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string appkey { get; set; }
        public string template { get; set; }
        public int create_by { get; set; }
        public DateTime create_at { get; set; }
        public int modify_by { get; set; }
        public DateTime modify_at { get; set; }
        public bool enable { get; set; }
        public int status { get; set; }
    }
}
