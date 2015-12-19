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
            //文件根目录
            string rootDirectory = @"E:\Git\BulkModifier\UnitTest\testTxt";
            //处理文件的类型
            string fileExtension = ".txt,";
            //替换的目标
            string searchContent = @"(?<!android\.)R\.\w+\.\w+(?=[ ,;))])";
            FileHelper.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed);
            FileHelper.batchReplacement(new ReplacementOption()
            {
                directory = rootDirectory,
                filterExtensions = fileExtension,
                matchingKey = searchContent,
                sameLevel = false
            });
        }

        /// <summary>
        /// 处理匹配成功字符串
        /// </summary>
        /// <param name="value"></param>
        public string onMatchingSucceed(string value)
        {
            MatchCollection mc;
            //提取保留的内容
            var extract = "";
            var type = "";
            Regex reg = new Regex(@"(?<=\.)\w+");
            if (reg.IsMatch(value))
            {
                mc = reg.Matches(value);
                extract = mc[1].Value;
                type = mc[0].Value;
            }
            string result = "";
            switch (type)
            {
                case "id":
                    result = "my.getId(\"" + extract + "\")";
                    break;
                case "string":
                    result = "my.string(\"" + extract + "\")";
                    break;
            }
            return result;
        }
    }
}
