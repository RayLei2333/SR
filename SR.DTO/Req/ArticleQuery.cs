using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DTO.Req
{
    public class ArticleQuery
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        //值 mobile 或 pc
        public string ReqSource { get; set; }

        public string Keyword { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Area { get; set; }
    }
}
