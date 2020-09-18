using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat
{
    public class TokenResult : WeChatResult
    {
        public string access_token { get; set; }

        private int _expires_in;

        public int expires_in
        {
            get { return _expires_in; }
            set
            {
                _expires_in = value;
                this.ExpiresTime = DateTime.Now.AddSeconds(value);
            }
        }

        public DateTime ExpiresTime { get; set; }


    }
}
