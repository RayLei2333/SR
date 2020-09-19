using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ORM.ModelConfiguration;

namespace SR.Model
{
    public class T_WeChat_User
    {
        [Key(KeyType.GUID)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string OpenUserId { get; set; }

        public string Name { get; set; }

        public string HeaderImg { get; set; }

        public string Position { get; set; }

        public string Mobile { get; set; }

        public int Gender { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }

        public string Alias { get; set; }

        public string EnglishName { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
