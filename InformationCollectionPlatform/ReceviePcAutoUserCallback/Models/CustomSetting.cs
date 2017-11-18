using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Models
{
    public class CustomSetting
    {
        public string SqlServerConnection { get; set; }
        public string MySqlConnection { get; set; }
        public string MySqlDevMediaDissectorConnection { get; set; }
        public string MySqlProMediaDissectorConnection { get; set; }
    }
}
