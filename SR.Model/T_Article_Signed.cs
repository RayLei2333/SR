using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Model
{
    public class T_Article_Signed
    {
        public string Id { get; set; }

        public string ArticleId { get; set; }

        public string DetailId { get; set; }

        public string UserId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}
