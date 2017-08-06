using ReceviePcAutoUserCallback.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_ExceptionLogPcAutoRepository:BaseRepository<Tb_ExceptionLogPcAuto>
    {
        public Tb_ExceptionLogPcAutoRepository(string strConn)
            :base(strConn)
        { }

        public void Add(Tb_ExceptionLogPcAuto exceptionLog)
        {
            string sqlScript = "INSERT INTO tb_exceptionlogpcauto(LogLevel, LogMessage, LogBusinessTopic, OtherMessage)"
                + "VALUES(@LogLevel, @LogMessage, @LogBusinessTopic, @OtherMessage)";
            base.Add(exceptionLog, sqlScript);
        }

        public int AddAndReturnId(Tb_ExceptionLogPcAuto exceptionLog)
        {
            string sqlScript = "INSERT INTO tb_exceptionlogpcauto(LogLevel, LogMessage, LogBusinessTopic, OtherMessage)"
                + "VALUES(@LogLevel, @LogMessage, @LogBusinessTopic, @OtherMessage);SELECT LAST_INSERT_ID();";
            return base.AddAndReturnId(exceptionLog, sqlScript);
        }
    }
}
