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
        /// <summary>
        /// 替换R.xxx
        /// </summary>
        [TestMethod]
        public void replaceR()
        {
            //文件根目录
            string rootDirectory = @"E:\库\APICloud\yuntongxunModule\ECKitSdk";
            //处理文件的类型
            string fileExtension = ".java,";
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
        /// 替换R.xxx
        /// </summary>
        [TestMethod]
        public void replaceReference()
        {
            //文件根目录
            string rootDirectory = @"E:\库\APICloud\yuntongxunModule\ECKitSdk";
            //处理文件的类型
            string fileExtension = ".java,";
            //替换的目标
            string searchContent = @"import com.yuntongxun.eckitsdk.R;";
            FileHelper.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed_replaceReference);
            FileHelper.batchReplacement(new ReplacementOption()
            {
                directory = rootDirectory,
                filterExtensions = fileExtension,
                matchingKey = searchContent,
                sameLevel = false
            });

        }
        public string onMatchingSucceed_replaceReference(string value)
        {
            return "import com.uzmap.pkg.uzcore.UZResourcesIDFinder;";
        }
        ///// <summary>
        ///// 替换 xml文件中的 @drawable/ a @color/ 等等
        ///// </summary>
        //[TestMethod]
        //public void replaceXmlAit()
        //{
        //    //文件根目录
        //    string rootDirectory = @"E:\库\APICloud\yuntongxunModule\ECKitImDemo";
        //    //处理文件的类型
        //    string fileExtension = ".xml,";
        //    //替换的目标
        //    string searchContent = @"(?<!android\.)R\.\w+\.\w+(?=[ ,;))])";
        //    FileHelper.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed);
        //    FileHelper.batchReplacement(new ReplacementOption()
        //    {
        //        directory = rootDirectory,
        //        filterExtensions = fileExtension,
        //        matchingKey = searchContent,
        //        sameLevel = false
        //    });
        //}

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
                    result = @"UZResourcesIDFinder.getResIdID(""" + extract + @""")";
                    break;
                case "string":
                    result = @"UZResourcesIDFinder.getResStringID(""mo_rlyuntongxun_string_" + extract + @""")";
                    break;
                case "dimen":
                    result = @"UZResourcesIDFinder.getResDimenID(""mo_rlyuntongxun_dimen_" + extract + @""")";
                    break;
                case "color":
                    result = @"UZResourcesIDFinder.getResColorID(""mo_rlyuntongxun_color_" + extract + @""")";
                    break;
                case "drawable":
                    result = @"UZResourcesIDFinder.getResDrawableID(""mo_rlyuntongxun_drawable_" + extract + @""")";
                    break;
                case "layout":
                    result = @"UZResourcesIDFinder.getResLayoutID(""mo_rlyuntongxun_layout_" + extract + @""")";
                    break;
                case "anim":
                    result = @"UZResourcesIDFinder.getResAnimID(""mo_rlyuntongxun_anim_" + extract + @""")";
                    break;
                case "array":
                    result = @"UZResourcesIDFinder.getResArrayID(""mo_rlyuntongxun_array_" + extract + @""")";
                    break;
                case "styleable":
                    result = @"UZResourcesIDFinder.getResStyleableID(""mo_rlyuntongxun_styleable_" + extract + @""")";
                    break;
                case "style":
                    result = @"UZResourcesIDFinder.getResStyleID(""mo_rlyuntongxun_style_" + extract + @""")";
                    break;
                case "xml":
                    result = @"UZResourcesIDFinder.getResXmlID(""mo_rlyuntongxun_xml_" + extract + @""")";
                    break;
            }
            return result;
        }
    }
}
