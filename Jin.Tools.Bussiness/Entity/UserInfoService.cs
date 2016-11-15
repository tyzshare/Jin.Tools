using Jin.Tools.Dto;
using Jin.Tools.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jin.Tools.Bussiness.Entity
{
    public class UserInfoService
    {
        public void CreateUserInfo(UserInfoDto user)
        {
            var ss = AutoMapperHelper.MapTo<UserInfo>(user);
        }
        public void CreateUserInfo(List<UserInfoDto> list_user)
        {
            var ss = AutoMapperHelper.MapToList<UserInfo>(list_user);
        }
    }
}
