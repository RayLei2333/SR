using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ORM.ModelConfiguration;

namespace SR.Model
{
    public class T_Article
    {
        [Key(KeyType.GUID)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string HeaderImg { get; set; }

        public string Contents { get; set; }

        public bool GroupMsg { get; set; }

        public string Author { get; set; }

        public bool IsSend { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
