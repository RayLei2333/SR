﻿using SR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ORM;

namespace SR.Respository
{
    /// <summary>
    /// 数据库底层服务
    /// </summary>
    public class BaseRespository : DbContext
    {
        public BaseRespository() : base(new SqlConnection(AppConfig.DB))
        {
        }
    }
}
