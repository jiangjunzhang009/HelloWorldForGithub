using ReceviePcAutoUserCallback.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_OperationLogPcAutoRepository:BaseRepository<Tb_OperationLogPcAuto>
    {
        public Tb_OperationLogPcAutoRepository(string strConn)
            :base(strConn)
        { }

        public int AddAndReturnId(Tb_OperationLogPcAuto operationLog)
        {
            string sqlScript = "INSERT INTO tb_operationlogpcauto(OperationLevel, OperationMessage, OperationBusinessTopic, OtherMessage)"
                + "VALUES(@OperationLevel, @OperationMessage, @OperationBusinessTopic, @OtherMessage);SELECT LAST_INSERT_ID();";
            return base.AddAndReturnId(operationLog, sqlScript);
        }
    }
}
