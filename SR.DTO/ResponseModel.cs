using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DTO
{
    public abstract class ResponseInfo
    {
        public ResponseInfo() { }

        public ResponseInfo(int code, string msg)
        {
            this.ErrCode = code;
            this.ErrMsg = msg;
        }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

    }
    public class ResponseModel : ResponseInfo
    {
        public ResponseModel() { }

        public ResponseModel(int code, string msg) : base(code, msg){ }

        public ResponseModel(object data)
        {
            this.Data = data;
        }

        public object Data { get; set; }
    }

    public class ResponseModel<T> : ResponseInfo where T:class
    {
        public ResponseModel() { }

        public ResponseModel(int code, string msg) : base(code, msg) { }

        public ResponseModel(T data)
        {
            this.Data = data;
        }

        public T Data { get; set; }
    }
}
