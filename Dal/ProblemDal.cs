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
            return new ProblemDal().Where(i => !i.DelFlag).ToList();
            //    .Select(i => new Problem
            //{
            //    ProblemDes = i.ProblemDes.RemoveNewLine(),
            //    ProblemName = i.ProblemName.RemoveNewLine(),
            //    ProblemType = i.ProblemType.RemoveNewLine(),
            //    ProblemSol = i.ProblemSol.RemoveNewLine(),
            //    ProblemVega = i.ProblemVega.RemoveNewLine(),
            //    Id = i.Id
            //}).ToList();
        }
    }
}
