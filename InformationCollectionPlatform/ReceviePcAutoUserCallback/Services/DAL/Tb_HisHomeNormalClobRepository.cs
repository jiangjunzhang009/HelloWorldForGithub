using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisHomeNormalClobRepository:BaseRepository<Tb_HisHomeNormalClob>
    {
        public Tb_HisHomeNormalClobRepository(string strConn)
            :base(strConn)
        { }
    }
}
