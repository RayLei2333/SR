﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ORM.ModelConfiguration;

namespace SR.Model
{
    public class T_Article_Sign
    {
        [Key(KeyType.GUID)]
        public string Id { get; set; }

        public string ArticleId { get; set; }

        public string DetailId { get; set; }

        public string UserId { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreatTime { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}
