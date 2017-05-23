using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jin.Tools.ConsoleApplication
{
    public class CreateUserInfoDto : IValidate
    {
        /// <summary>
        /// 登陆邮箱
        /// </summary>
        [EmailAddress(ErrorMessage = "登录邮箱不合法")]
        public string LoginEmail
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string LoginPwd
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        public long CreatorId
        {
            get;
            set;
        }
    }
}
