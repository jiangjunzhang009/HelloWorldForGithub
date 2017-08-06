using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisCommentPostRepository:BaseRepository<Tb_HisCommentPost>
    {
        public Tb_HisCommentPostRepository(string strConn)
            :base(strConn)
        { }
    }
}
