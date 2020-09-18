using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ORM.ModelConfiguration;

namespace SR.Model
{
    public class T_Article_Comment
    {
        [Key(KeyType.GUID)]
        public string Id { get; set; }

        public string ArticleId { get; set; }

        public string UserId { get; set; }

        public string Contents { get; set; }

        public string Media { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
