using ReceviePcAutoUserCallback.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.DAL
{
    public class TbUnknowTypeDataRepository:BaseRepository<TbUnknowTypeData>
    {
        #region Constructor
        public TbUnknowTypeDataRepository(string strConn)
            :base(strConn)
        { }
        #endregion
        #region Methods
        public void AddRecord(string tableName, TbUnknowTypeData record)
        {
            string sqlScript = $"INSERT INTO {tableName}(OperationId,Id,TaskId,TaskGroupId,CompatibleTaskId,RuntimeRefUrl,RuntimeHttpMocker,RuntimeFirstPageUrl,PcAutoUserId,IsMultiPage,PageNo,CrawledDataUrl,CrawledDataContent,CrawledDataTime,OtherMessage)" +
                $"VALUES(@OperationId,@Id,@TaskId,@TaskGroupId,@CompatibleTaskId,@RuntimeRefUrl,@RuntimeHttpMocker,@RuntimeFirstPageUrl,@PcAutoUserId,@IsMultiPage,@PageNo,@CrawledDataUrl,@CrawledDataContent,@CrawledDataTime,@OtherMessage)";
            base.Add(record, sqlScript);
        }
        #endregion
    }
}
