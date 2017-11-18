using ReceviePcAutoUserCallback.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.DAL
{
    public class CommonLogRepository:BaseRepository<Tb_ExceptionLog>
    {
        #region Constructor
        public CommonLogRepository(string strConn)
            :base(strConn)
        { }
        #endregion
    }
}
