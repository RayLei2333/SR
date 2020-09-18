using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat
{
    public class DepartmentResult : WeChatResult
    {
        public List<DepartmentData> department { get; set; }
    }

    public class DepartmentData
    {
        /// <summary>
        /// 创建的部门id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string name_en { get; set; }

        /// <summary>
        /// 父部门id
        /// </summary>
        public int parentid { get; set; }

        /// <summary>
        /// 在父部门中的次序值
        /// </summary>
        public int order { get; set; }
    }

}
