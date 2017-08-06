using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisCarAnswerRepository: BaseRepository<Tb_HisCarAnswer>
    {
        public Tb_HisCarAnswerRepository(string strConn)
            :base(strConn)
        { }
    }
}
