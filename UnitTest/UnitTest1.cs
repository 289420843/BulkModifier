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
            string rootDirectory = @"E:\库\APICloud\yuntongxunModule";
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
                    result = @"FindResourceHelper.getResIdID(""" + extract + @""")";
                    break;
                case "string":
                    result = @"FindResourceHelper.getResStringID(""mo_rlyuntongxun_string_" + extract + @""")";
                    break;
                case "dimen":
                    result = @"FindResourceHelper.getResDimenID(""mo_rlyuntongxun_dimen_" + extract + @""")";
                    break;
                case "color":
                    result = @"FindResourceHelper.getResColorID(""mo_rlyuntongxun_color_" + extract + @""")";
                    break;
                case "drawable":
                    result = @"FindResourceHelper.getResDrawableID(""mo_rlyuntongxun_drawable_" + extract + @""")";
                    break;
                case "layout":
                    result = @"FindResourceHelper.getResLayoutID(""mo_rlyuntongxun_layout_" + extract + @""")";
                    break;
                case "anim":
                    result = @"FindResourceHelper.getResAnimID(""mo_rlyuntongxun_anim_" + extract + @""")";
                    break;
                case "array":
                    result = @"FindResourceHelper.getResArrayID(""mo_rlyuntongxun_array_" + extract + @""")";
                    break;
                case "styleable":
                    result = @"FindResourceHelper.getResStyleableID(""mo_rlyuntongxun_styleable_" + extract + @""")";
                    break;
                case "style":
                    result = @"FindResourceHelper.getResStyleID(""mo_rlyuntongxun_style_" + extract + @""")";
                    break;
                case "xml":
                    result = @"FindResourceHelper.getResXmlID(""mo_rlyuntongxun_xml_" + extract + @""")";
                    break;
                case "plurals":
                    result = @"FindResourceHelper.getResPluralsID(""mo_rlyuntongxun_xml_" + extract + @""")";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 替换引用R 为资源引用包
        /// </summary>
        [TestMethod]
        public void replaceReference()
        {
            //文件根目录
            string rootDirectory = @"E:\库\APICloud\yuntongxunModule";
            //处理文件的类型
            string fileExtension = ".java,";
            //替换的目标
            string searchContent = @"import com.yuntongxun.eckitsdk.R;";
            FileHelper fh1 = new FileHelper();
            fh1.onMatchingSucceedDelegate += new MatchingSucceedDelegate(onMatchingSucceed_replaceReference);
            fh1.batchReplacement(new ReplacementOption()
            {
                directory = rootDirectory,
                filterExtensions = fileExtension,
                matchingKey = searchContent,
                sameLevel = false
            });
        }
        public string onMatchingSucceed_replaceReference(string value)
        {
            return "import com.shenglin.global.helper.FindResourceHelper;";
        }

        /// <summary>
        /// 替换引用资源
        /// </summary>
        [TestMethod]
        public void replaceReferenceResource()
        {
            //文件根目录
            string rootDirectory = @"E:\库\APICloud\yuntongxunModule\";
            //处理文件的类型
            string fileExtension = ".xml,";
            //替换的目标
            string searchContent = @"(?<=["">])@\w+/(?!mo_rlyuntongxun_)\w+";
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

        public List<string> types = new List<string>();
        public string onMatchingSucceed_replaceReferenceResource(string value)
        {
            //提取保留的内容
            var extract = value.Substring(value.IndexOf("/") + 1);
            var type = value.Substring(1, value.IndexOf("/") - 1);
            string result = string.Format(@"@{0}/mo_rlyuntongxun_{1}_{2}", type, type, extract);
            if (!types.Contains(type))
            {
                types.Add(type);
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
