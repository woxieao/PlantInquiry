using System.Linq;
using PlantInquiry.Attributes;
using PlantInquiry.Core;
using PlantInquiry.Dal;
using PlantInquiry.Models.Db;
using XASoft.BaseMvc;
using XASoft.Extensions;

namespace PlantInquiry.Controllers
{
    public class ApiController : BaseApiController
    {
        public OkResult Login(User user)
        {
            Session[Configs.UserInfoSessionKeyName] = 1;
            return Ok();
        }
        [Auth]
        public OkResult GetProblemList(string keyword = "")
        {
            var problemList = MethodCacheExtensions.GetCache(new ProblemDal().GetProblemList);
            
            return Ok(problemList);
        }

        public OkResult Test()
        {
         
            return Ok(Logic.IsContain("wasas一二三四五六七八九十sa", ""));
        }
    }
}