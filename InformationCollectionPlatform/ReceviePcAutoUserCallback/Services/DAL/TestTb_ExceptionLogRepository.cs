using ReceviePcAutoUserCallback.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class TestTb_ExceptionLogRepository:BaseRepository<TestTb_ExceptionLog>
    {
        public TestTb_ExceptionLogRepository(string strConn)
            :base(strConn)
        { }

        public void Add(TestTb_ExceptionLog exceptionLog)
        {
            string sqlScript = "INSERT INTO Tb_ExceptionLog(LogLevel, LogTopic, LogMessage, OtherMessage)"
                + "VALUES(@LogLevel, @LogTopic, @LogMessage, @OtherMessage)";
            base.Add(exceptionLog, sqlScript);
        }
        public IEnumerable<TestTb_ExceptionLog> GetAll()
        {
            string sqlScript = "SELECT * FROM  Tb_ExceptionLog;";
            return base.GetAll(sqlScript);
        }
    }
}
