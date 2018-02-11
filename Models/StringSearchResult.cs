using System.Collections.Generic;
using System.Text;
using PlantInquiry.Models.Db;

namespace PlantInquiry.Models
{
    public enum CheckTypeEnum
    {
        ProblemDes = 1,
        ProblemName = 2,
        ProblemSol = 3,
        ProblemType = 4,
        ProblemVega = 5
    }

    public partial class StringSearchResult
    {

        public string Keyword { get; set; }
        public CheckTypeEnum CheckType { get; set; }
        public int StartIndex { get; set; }
    }
}
