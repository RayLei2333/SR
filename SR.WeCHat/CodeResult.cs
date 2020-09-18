using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat
{
    public class CodeResult : WeChatResult
    {
        public string UserId { get; set; }

        public string OpenId { get; set; }

        public string DeviceId { get; set; }
    }
}
