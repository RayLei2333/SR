using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DTO
{
    public abstract class ResponseInfo
    {
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

    }
    public class ResponseModel : ResponseInfo
    {
        public object Data { get; set; }
    }

    public class ResponseModel<T> : ResponseInfo
    {
        public T Data { get; set; }
    }
}
