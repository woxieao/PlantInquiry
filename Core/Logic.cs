using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantInquiry.Core
{
    public class Logic
    {
        public static List<string> IsContain(string keyword, string str2Check)
        {
            var str = new StringBuilder();
            foreach (var c in keyword)
            {
                if (c >= 19968 && c <= 40869)
                {
                    str.Append(c);
                }
            }
            var strResult = str.ToString();
            var wordList = new HashSet<string>();
            Action<string, int> cutStr = (s2Add, cutLen) =>
            {
                for (var i = 0; i < s2Add.Length - cutLen + 1; i += cutLen)
                {
                    var word = new StringBuilder();
                    for (var x = i; x < i + cutLen; x++)
                    {
                        word.Append(s2Add[x]);
                    }
                    wordList.Add(word.ToString());
                }
            };
            cutStr (strResult, 1);
            cutStr (strResult, 2);
            cutStr(strResult, 3);
            if (strResult.Length > 1)
            {
                cutStr(strResult.Substring(1), 2);
                cutStr(strResult.Substring(1), 3);
            }
            if (strResult.Length > 2)
            {
                cutStr(strResult.Substring(2), 3);
            }
            return wordList.OrderByDescending(i => i.Length).ToList();
        }
    }

}