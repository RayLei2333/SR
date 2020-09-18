using SR.WeChat.NetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat
{
    public class DepartmentService
    {
        public DepartmentResult GetList(string accessToken, string id = null)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={accessToken}";
            if (!string.IsNullOrEmpty(id))
                url += $"&id={id}";

            DepartmentResult result = WXApi.Request<DepartmentResult>(url, null, Method.GET);
            return result;
        }

        public CorpUserListResult GetUserList(string accessToken, string departmentId, string fetchChild)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={accessToken}&department_id={departmentId}&fetch_child={fetchChild}";

            CorpUserListResult result = WXApi.Request<CorpUserListResult>(url, null, Method.GET);
            return result;
        }


    }
}
