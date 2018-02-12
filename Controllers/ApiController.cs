using System;
using System.Collections.Generic;
using System.Linq;
using PlantInquiry.Attributes;
using PlantInquiry.Core;
using PlantInquiry.Dal;
using PlantInquiry.Models;
using PlantInquiry.Models.Db;
using XASoft.BaseMvc;
using XASoft.Extensions;

namespace PlantInquiry.Controllers
{
    public class ApiController : BaseApiController
    {
        private static readonly List<Problem> ProblemList = MethodCacheExtensions.GetCache(new ProblemDal().GetProblemList);
        public OkResult Login(User user)
        {
            Session[Configs.UserInfoSessionKeyName] = 1;
            return Ok();
        }
        public OkResult GetProblemList(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            Func<string, List<ProblemSearchResult>> func = str =>
            {
                return ProblemList.Select(problem => Logic.IsContain(keyword, problem)).ToList();
            };
            var result = MethodCacheExtensions.GetCache(func, keyword).Where(i => i.IsMatch() || string.IsNullOrEmpty(keyword)).OrderByDescending(i => i.PriorityLevel).ThenByDescending(i => i.StringSearchList.Count);
            return Ok(new
            {
                List = result.Skip(pageSize * (pageIndex - 1)).Take(pageSize),
                TotalCount = result.Count(),
                TotalPage = Math.Ceiling((result.Count() / (double)pageSize))
            });
        }
    }
}