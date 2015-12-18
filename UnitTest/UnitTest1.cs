using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string lineStr = "<book> (R.string.a) = android.R.string.b , R.string.c;R.id.d;";
            string keyStr = "getResDrawableID(*)";
            var priceRegex = new Regex(@"(R\.string)");
            if (priceRegex.IsMatch(lineStr))
            {
                Match m = priceRegex.Match(lineStr);
                for (var i = 0; i < m.Groups.Count;i++ )
                {
                    string a = m.Groups[i].Value;
                    Console.WriteLine(a);
                }
            }

            Console.ReadKey();

            //文件根目录
            string rootDirectory = @"E:\库\APICloud\APICloudSDK";
            //处理文件的类型
            string fileExtension = ".xml,.java";
            //替换的目标
            string searchContent = "UZResourcesIDFinder.getResDrawableID(";
            string extractContent = "";
            string replace = "UZResourcesIDFinder";
            IList<string> excFiles = FileHelper.filterExtension(rootDirectory, fileExtension);
            foreach (string filePath in excFiles)
            {
                FileHelper.matching(filePath, searchContent);
            }
            int fileCount = excFiles.Count;
        }
    }
}
