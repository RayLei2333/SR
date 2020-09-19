using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DTO.Resp
{
    public class UserSignArticleQuery
    {
        public string Id { get; set; }

        public string HeaderImg { get; set; }

        public string Contents { get; set; }

        public DateTime BeginTime { get; set; }
    }
}
