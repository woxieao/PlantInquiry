using System;
using System.Collections.Generic;
using System.Linq;
using PlantInquiry.Models.Db;

namespace PlantInquiry.Models
{
    public enum CheckTypeEnum
    {
        ProblemType = 1,
        ProblemVega = 2,
        ProblemSol = 3,
        ProblemDes = 4,
        ProblemName = 5,
    }
    public class StringSpan
    {
        public int StartIndex { get; set; }
        public int EndPoint { get; set; }

    }
    public class ProblemSearchResult
    {
        private readonly decimal _keywordLength;
        public ProblemSearchResult(Problem problem, int keywordLength, List<StringSearchResult> stringSearchList, int level)
        {
            Problem = problem;
            StringSearchList = stringSearchList;
            PriorityLevel = level;
            _keywordLength = keywordLength;
        }

        public HashSet<char> GetStringSpan(CheckTypeEnum checkType)
        {
            var keywordList = StringSearchList.Where(i => i.CheckType == checkType).Select(i => i.Keyword).ToList();
            var hashSet = new HashSet<char>();
            foreach (var keyword in keywordList)
            {
                foreach (var ch in keyword)
                {

                    hashSet.Add(ch);
                }
            }
            return hashSet;
        }
        public HashSet<char> ProblemTypeSpans => GetStringSpan(CheckTypeEnum.ProblemType);
        public HashSet<char> ProblemVegaSpans => GetStringSpan(CheckTypeEnum.ProblemVega);
        public HashSet<char> ProblemSolSpans => GetStringSpan(CheckTypeEnum.ProblemSol);
        public HashSet<char> ProblemDesSpans => GetStringSpan(CheckTypeEnum.ProblemDes);
        public HashSet<char> ProblemNameSpans => GetStringSpan(CheckTypeEnum.ProblemName);
        public int PriorityLevel { get; set; }
        public string PriorityPercent => (PriorityLevel / _keywordLength).ToString("p");
        public bool IsMatch()
        {
            return StringSearchList != null && StringSearchList.Any();
        }
        public Problem Problem { get; set; }
        internal List<StringSearchResult> StringSearchList { get; set; }
    }

    public partial class StringSearchResult
    {
        public string Keyword { get; set; }
        public CheckTypeEnum CheckType { get; set; }
        public string CheckTypeStr => CheckType.ToString();
        public int StartIndex { get; set; }
    }
}
