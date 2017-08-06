using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisHomeLoveCarsRepository:BaseRepository<Tb_HisHomeLoveCars>
    {
        public Tb_HisHomeLoveCarsRepository(string strConn)
            :base(strConn)
        { }
    }
}
