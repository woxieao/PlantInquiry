using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PlantInquiry.Models;
using PlantInquiry.Models.Db;

namespace PlantInquiry.Core
{
    public class Logic
    {
        public static List<int> AllIndexesOf(string str, string value)
        {
            var indexes = new List<int>();
            if (string.IsNullOrEmpty(value))
            {
                return indexes;
            }
            for (var index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index, StringComparison.Ordinal);
                if (index == -1)
                {
                    return indexes;
                }
                else
                {
                    indexes.Add(index);
                }
            }
        }
        public static ProblemSearchResult IsContain(string keyword, Problem problem)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var str = new StringBuilder();
                foreach (var c in keyword)
                {
                    if (c >= 19968 && c <= 40869)
                    {
                        str.Append(c);
                    }
                }
                keyword = Regex.Replace(keyword, "[^\u4e00-\u9fa5]", string.Empty);

                Func<string, int, HashSet<string>> cutStr = (s2Add, cutLen) =>
                 {
                     var wordList = new HashSet<string>();
                     for (var i = 0; i < s2Add.Length - cutLen + 1; i += cutLen)
                     {
                         var word = new StringBuilder();
                         for (var x = i; x < i + cutLen; x++)
                         {
                             word.Append(s2Add[x]);
                         }
                         wordList.Add(word.ToString());
                     }
                     return wordList;
                 };
                Func<HashSet<string>, CheckTypeEnum, List<StringSearchResult>> searchFunc = (wordList, checkType) =>
                     {
                         var tempResultList = new List<StringSearchResult>();
                         var str2Check = string.Empty;
                         switch (checkType)
                         {
                             case CheckTypeEnum.ProblemType:
                                 str2Check = problem.ProblemType;
                                 break;
                             case CheckTypeEnum.ProblemVega:
                                 str2Check = problem.ProblemVega;
                                 break;
                             case CheckTypeEnum.ProblemSol:
                                 str2Check = problem.ProblemSol;
                                 break;
                             case CheckTypeEnum.ProblemDes:
                                 str2Check = problem.ProblemDes;
                                 break;
                             case CheckTypeEnum.ProblemName:
                                 str2Check = problem.ProblemName;
                                 break;
                             default:
                                 break;
                         }
                         str2Check = Regex.Replace(str2Check, "[^\u4e00-\u9fa5]", string.Empty);
                         foreach (var word in wordList)
                         {
                             var indexs = AllIndexesOf(str2Check, word);
                             foreach (var index in indexs)
                             {
                                 tempResultList.Add(new StringSearchResult
                                 {
                                     CheckType = checkType,
                                     Keyword = word,
                                     StartIndex = index
                                 });
                             }
                         }
                         return tempResultList;
                     };
                Func<int, ProblemSearchResult> matchFunc = level =>
                {
                    var tempWordList = new HashSet<string>();
                    for (var i = level; i > 0; i--)
                    {
                        if (keyword.Length > i - 1)
                        {
                            foreach (var word in cutStr(keyword.Substring(i - 1), level))
                            {
                                tempWordList.Add(word);
                            }
                        }
                    }
                    var resultList = new List<StringSearchResult>();
                    resultList.AddRange(searchFunc(tempWordList, CheckTypeEnum.ProblemType));
                    resultList.AddRange(searchFunc(tempWordList, CheckTypeEnum.ProblemDes));
                    resultList.AddRange(searchFunc(tempWordList, CheckTypeEnum.ProblemName));
                    resultList.AddRange(searchFunc(tempWordList, CheckTypeEnum.ProblemSol));
                    resultList.AddRange(searchFunc(tempWordList, CheckTypeEnum.ProblemVega));
                    return new ProblemSearchResult(problem, keyword.Length, resultList, level);
                };

                var startLevel = keyword.Length > Configs.MatchWordMaxLength ? Configs.MatchWordMaxLength : keyword.Length;
                do
                {
                    var result = matchFunc(startLevel);
                    if (result.IsMatch())
                    {
                        return result;
                    }
                }
                while (--startLevel > 0);
            }
            return new ProblemSearchResult(problem, -1, new List<StringSearchResult>(), 0);
        }
    }
}