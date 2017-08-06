using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisHomeRepository:BaseRepository<Tb_HisHomeRepository>
    {
        public Tb_HisHomeRepository(string strConn)
            :base(strConn)
        { }
    }
}
