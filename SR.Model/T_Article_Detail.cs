﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.ORM.ModelConfiguration;

namespace SR.Model
{
    public class T_Article_Detail
    {
        [Key(KeyType.GUID)]
        public string Id { get; set; }

        public string ArticleId { get; set; }

        public string Place { get; set; }

        public string Longitude { get; set; }

        public string Region { get; set; }

        public int Number { get; set; }

        public bool IsLBS { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}
