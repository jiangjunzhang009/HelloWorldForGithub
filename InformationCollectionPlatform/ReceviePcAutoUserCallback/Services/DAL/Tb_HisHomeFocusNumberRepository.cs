using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisHomeFocusNumberRepository:BaseRepository<Tb_HisHomeFocusNumber>
    {
        public Tb_HisHomeFocusNumberRepository(string strConn)
            :base(strConn)
        { }
    }
}
