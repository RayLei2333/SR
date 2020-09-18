using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ORM.ModelConfiguration;

namespace SR.Model
{
    public class T_Sys_User
    {
        [Key(KeyType.GUID)]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string Role { get; set; }

        public string CreateTime { get; set; }

    }
}
