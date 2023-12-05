using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using CodingCommon.Extentions;


namespace CodingCommon.Tests
{
    [TestClass()]
    public class StringExtensionTests
    {
        [TestMethod()]
        public void ToLowerUnderlineCaseTest()
        {
            string[] inputStrings = { "bestCodeNeed", "best_code_need", "BestCodeNeed", "best code need", "Best Code Need", "best-code-need", "Best-Code-Need", "BEST-CODE-NEED" };

            foreach (string input in inputStrings)
            {
                string output = input.ToNamingLowerKebabCase();
                Debug.WriteLine(output);
            }
        }
    }
}