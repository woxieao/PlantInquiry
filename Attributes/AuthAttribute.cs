using System.Web;
using System.Web.Mvc;
using XASoft.CommonModel.ExceptionModel;

namespace PlantInquiry.Attributes
{

    public class AuthAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session[Configs.UserInfoSessionKeyName] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                throw new AuthException("请登录后重试");
            }
            else
            {
                filterContext.HttpContext.Response.Redirect(Configs.LoginPage);
            }
        }
    }
}
