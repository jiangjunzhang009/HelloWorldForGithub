using InformationCollectionPlatform.Models;
using InformationCollectionPlatform.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationCollectionPlatform.Services
{
    public class ReceiveMessageService
    {
        DALService dalService = new DALService();

        public int SaveTb_ExceptionLog(Tb_ExceptionLog exceptionLog)
        {
            return dalService.SaveTb_ExceptionLog(exceptionLog);
        }

        public int SaveTb_OperationLog(Tb_OperationLog operationLog)
        {
            return dalService.SaveTb_OperationLog(operationLog);
        }

        public int SaveTb_RawRequest(Tb_RawRequest rawRequest)
        {
            return dalService.SaveTb_RawRequest(rawRequest);
        }
    }
}