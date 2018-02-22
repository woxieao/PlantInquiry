using System.Collections.Generic;
using System.Linq;
using PlantInquiry.Core;
using PlantInquiry.Models.Db;
using XASoft.EfHelper.Models;

namespace PlantInquiry.Dal
{

    public partial class ProblemDal : DalBase<Problem, DataBase>
    {
        public ProblemDal() : base(new DataBase())
        {
        }
        public List<Problem> GetProblemList()
        {
            return this.Where(i => !i.DelFlag).ToList();
        }
    }
}
