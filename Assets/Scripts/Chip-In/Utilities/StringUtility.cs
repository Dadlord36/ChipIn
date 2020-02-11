﻿using System.Text.RegularExpressions;
using UnityEngine.Assertions;

namespace Utilities
{
    public static class StringUtility
    {
        public static int GetIntPartOfString(string givenString)
        {
            Assert.IsFalse(string.IsNullOrEmpty(givenString));

            var resultString = Regex.Match(givenString, @"\d+").Value;
            return int.Parse(resultString);
        }
    }
}