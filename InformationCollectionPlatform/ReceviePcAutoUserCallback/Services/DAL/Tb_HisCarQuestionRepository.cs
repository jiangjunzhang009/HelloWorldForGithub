﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisCarQuestionRepository:BaseRepository<Tb_HisCarQuestion>
    {
        public Tb_HisCarQuestionRepository(string strConn)
            :base(strConn)
        { }
    }
}
