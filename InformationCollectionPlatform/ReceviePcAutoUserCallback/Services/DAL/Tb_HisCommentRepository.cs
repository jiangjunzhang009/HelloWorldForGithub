using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisCommentRepository:BaseRepository<Tb_HisComment>
    {
        public Tb_HisCommentRepository(string strConn)
            :base(strConn)
        { }
    }
}
