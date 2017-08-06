using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class Tb_HisHomeUserSettingRepository : BaseRepository<Tb_HisHomeUserSetting>
    {
        public Tb_HisHomeUserSettingRepository(string strConn)
            :base(strConn)
        { }
    }
}
