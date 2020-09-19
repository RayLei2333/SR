using SR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DTO.Resp
{
    public class ArticleDTO //: T_Article
    {
        public T_Article Article { get; set; }

        public List<T_Article_Detail> Detail { get; set; }
    }
}
