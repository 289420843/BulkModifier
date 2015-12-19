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
            string rootDirectory = @"E:\Git\BulkModifier\UnitTest";
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
                sameLevel = true
            });
        }
        
        /// <summary>
        /// 处理匹配成功字符串
        /// </summary>
        /// <param name="value"></param>
        public void onMatchingSucceed(string value)
        {
            string aa = value;
        }
    }
}
