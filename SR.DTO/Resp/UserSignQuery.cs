using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DTO.Resp
{
    public class UserSignQuery
    {
        public string ArticleId { get; set; }

        public string DetailId { get; set; }

        public int Count { get; set; }
    }
}
