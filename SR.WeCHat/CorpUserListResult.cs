using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat
{
    public class CorpUserListResult : WeChatResult
    {
        public List<CorpUserResult> userlist { get; set; }
    }

    public class CorpUserResult : WeChatResult
    {
        public string userid { get; set; }

        public string name { get; set; }

        public string position { get; set; }

        public string mobile { get; set; }

        public string avatar { get; set; }

        public int gender { get; set; }

        public string email { get; set; }

        public string alias { get; set; }

        public string english_name { get; set; }

        public string open_userid { get; set; }

        public int status { get; set; }
    }
}
