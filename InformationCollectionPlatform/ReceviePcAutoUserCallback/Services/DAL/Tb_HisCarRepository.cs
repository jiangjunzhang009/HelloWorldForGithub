using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisCarRepository:BaseRepository<Tb_HisCar>
    {
        public Tb_HisCarRepository(string strConn)
            :base(strConn)
        { }
    }
}
