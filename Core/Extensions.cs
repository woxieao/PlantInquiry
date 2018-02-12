using System;
using System.Collections.Generic;
using System.Text;
using PlantInquiry.Models;
using PlantInquiry.Models.Db;

namespace PlantInquiry.Core
{
    public static class Extensions
    {
        public static string RemoveNewLine(this string str)
        {
            return str?.Replace("\r\n", string.Empty);
        }
    }
}