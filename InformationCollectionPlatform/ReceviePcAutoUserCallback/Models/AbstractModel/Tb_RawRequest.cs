using System;
using System.Collections.Generic;
using System.Linq;

namespace ReceviePcAutoUserCallback.Models.AbstractModel
{
    public abstract class Tb_RawRequest:CustomTable
    {
        public string RequestUrl { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string OtherMessage { get; set; }
    }
}