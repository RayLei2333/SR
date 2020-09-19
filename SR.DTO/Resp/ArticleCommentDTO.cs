using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DTO.Resp
{
    public class ArticleCommentDTO
    {
        public string ArticleId { get; set; }

        public string Content { get; set; }

        public IEnumerable<ArticleCommentMedia> Media { get; set; } 
    }

    public class ArticleCommentMedia
    {
        public int Seq { get; set; }

        public string Media { get; set; }
    }
}
