using Jin.Tools.Bussiness.Entity;
using Jin.Tools.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jin.Tools.Web.Test.Controllers
{
    public class UserManageController : Controller
    {
        // GET: UserManage
        public ActionResult Index()
        {
         
            UserInfoService service_user = new UserInfoService();

            //UserInfoDto user = new UserInfoDto() { Name = "tyzshare", Pwd = "tyzshare" };
            //service_user.CreateUserInfo(user);

            List<UserInfoDto> list_user = new List<UserInfoDto>();
            UserInfoDto user1 = new UserInfoDto() { Name = "tyzshare1", Pwd = "tyzshare" };
            UserInfoDto user2 = new UserInfoDto() { Name = "tyzshare2", Pwd = "tyzshare" };
            list_user.Add(user1);
            list_user.Add(user2);
            service_user.CreateUserInfo(list_user);

            return View();
        }
    }
}