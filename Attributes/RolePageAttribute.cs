using System.Web;
using System.Web.Mvc;
using PlantInquiry.Models.Db;

namespace PlantInquiry.Attributes
{

    public class RolePageAttribute : AuthorizeAttribute
    {
        public string Url { get; set; }
        public RoleTypeEnum RoleType { get; set; }
        public RolePageAttribute(RoleTypeEnum roleType, string url)
        {
            Url = url;
            RoleType = roleType;
        }
 
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var flag = base.AuthorizeCore(httpContext);
            if (flag)
            {
                var userInfo = (User)httpContext.Session[Configs.UserInfoSessionKeyName];
                if (userInfo.RoleType == RoleType)
                {
                    httpContext.Response.Redirect(Url);
                }
            }
            return flag;
        }
    }
}
