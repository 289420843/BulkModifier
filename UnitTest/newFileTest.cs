using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UnitTest
{
    [TestClass]
    public class newFileTest
    {
        /// <summary>
        /// 替换R.xxx
        /// </summary>
        [TestMethod]
        public void replaceR()
        {
            //文件根目录
            string rootDirectory = @"E:\Code\AndroidStudio\YTXFull_Demo\app\src\main\java";
            //处理文件的类型
            string fileExtension = ".java,";
            //替换的目标
            string searchContent = @"(?<!android\.)R\.\w+\.\w+(?=[ ,;))])";
            FileHelper fh1 = new FileHelper();
            fh1.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed);
            fh1.batchReplacement(new ReplacementOption()
            {
                directory = rootDirectory,
                filterExtensions = fileExtension,
                matchingKey = searchContent,
                sameLevel = false
            });
        }
        List<string> RList = new List<string>() { "dimen", "string", "color", "drawable", "layout", "anim", "array", "styleable", "style", "plurals" };
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
            string beforeStr = value.Substring(0, value.LastIndexOf(".") + 1);
            string result = null;
            if (RList.Contains(type))
            {
                result = beforeStr + "mo_rlytx_" + extract;
            }
            return result;
        }


        /// <summary>
        /// 替换引用资源
        /// </summary>
        [TestMethod]
        public void replaceReferenceResource()
        {
            //文件根目录
            string rootDirectory = @"E:\Code\AndroidStudio\YTXFull_Demo\app\src\main\res";
            //处理文件的类型
            string fileExtension = ".xml,";
            //替换的目标
            string searchContent = @"(?<=["">])@\w+/(?!mo_rlytx_)\w+";
            FileHelper fh1 = new FileHelper();
            fh1.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed_replaceReferenceResource);
            fh1.batchReplacement(new ReplacementOption()
            {
                directory = rootDirectory,
                filterExtensions = fileExtension,
                matchingKey = searchContent,
                sameLevel = false
            });
            int count = types.Count;
        }

        public List<string> types = new List<string>() { "dimen", "color", "drawable", "layout", "anim", "style", "string", "array", "interpolator" };
        public string onMatchingSucceed_replaceReferenceResource(string value)
        {
            //提取保留的内容
            var extract = value.Substring(value.IndexOf("/") + 1);
            var type = value.Substring(1, value.IndexOf("/") - 1);
            string result = null;
            if (types.Contains(type))
            {
                result = string.Format(@"@{0}/mo_rlytx_{1}", type, extract);
            }

            return result;
        }
        /// <summary>
        /// 替换xml 资源
        /// </summary>
        [TestMethod]
        public void replacexmlresources()
        {
            //文件根目录
            string rootDirectory = @"E:\库\APICloud\yuntongxunModule\";
            //处理文件的类型
            string fileExtension = ".xml,";
            //替换的目标
            string searchContent = @"<\S+ name=""(?!mo_rlyuntongxun_)";
            FileHelper fh1 = new FileHelper();
            fh1.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed_replacexmlresources);
            fh1.batchReplacement(new ReplacementOption()
            {
                directory = rootDirectory,
                filterExtensions = fileExtension,
                matchingKey = searchContent,
                sameLevel = false
            });
        }

        List<string> labelNames = new List<string>() { "string", "dimen", "color", "string-array", "declare-styleable", "style", "plurals" };
        public string onMatchingSucceed_replacexmlresources(string value)
        {
            string result = null;
            //提取保留的内容
            var extract = value.Substring(value.IndexOf(" ") + 1);
            var type = value.Substring(1, value.IndexOf(" ") - 1);
            if (labelNames.Contains(type))
            {
                result = string.Format(@"<{0} name=""mo_rlyuntongxun_{1}_", type, type);
                if (!types.Contains(type))
                {
                    types.Add(type);
                }

            }
            return result;
        }



        /// <summary>
        /// 替换xml 资源
        /// </summary>
        [TestMethod]
        public void replacelayoutclass()
        {
            //文件根目录
            string rootDirectory = @"E:\Code\AndroidStudio\rlytxim\app\src\main\res\layout";
            //处理文件的类型
            string fileExtension = ".xml,";
            //替换的目标
            string searchContent = @"(<\.)|(</\.)";
            FileHelper fh1 = new FileHelper();
            fh1.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed_replacelayoutclass);
            fh1.batchReplacement(new ReplacementOption()
            {
                directory = rootDirectory,
                filterExtensions = fileExtension,
                matchingKey = searchContent,
                sameLevel = false
            });
        }
        public string onMatchingSucceed_replacelayoutclass(string value)
        {
            string result = null;
            if (value.Length == 2)
            {
                result = "<com.cloudhealth.shenglin.rlytxim.";
            }
            else
            {
                result = "</com.cloudhealth.shenglin.rlytxim.";
            }
            return result;
        }
    }
}
