﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisHomeOfficalClubRepository : BaseRepository<Tb_HisHomeOfficalClub>
    {
        public Tb_HisHomeOfficalClubRepository(string strConn)
            :base(strConn)
        { }
    }
}
