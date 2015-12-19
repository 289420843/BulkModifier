using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    /// 替换选项
    /// </summary>
    public class ReplacementOption
    {
        /// <summary>
        /// 根目录
        /// </summary>
        public string directory { get; set; }
        /// <summary>
        /// 匹配关键字
        /// </summary>
        public string matchingKey { get; set; }
        /// <summary>
        /// 同级
        /// </summary>
        public bool sameLevel { get; set; }
        /// <summary>
        /// Filter by file type 过滤文件类型
        /// </summary>
        public string filterExtensions { get; set; }
    }
    /// <summary>
    /// 过滤结果
    /// </summary>
    public class File
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileExtension { get; set; }
    }
    public class FileHelper
    {

        /// <summary>
        /// 过滤文件
        /// </summary>
        /// <returns></returns>
        public static IList<File> filterFile(string directory, string filterExtensions, bool sameLevel = true)
        {
            List<File> result = new List<File>();
            DirectoryInfo TheFolder = new DirectoryInfo(directory);
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                if (sameLevel)
                    continue;
                string subDirectory = directory + "//" + NextFolder.Name;
                result.AddRange(filterFile(subDirectory, filterExtensions));
            }
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                if (filterExtensions.IndexOf(NextFile.Extension) < 0)
                    continue;
                result.Add(new File()
                {
                    fileName = NextFile.Name,
                    filePath = NextFile.FullName,
                    fileExtension = NextFile.Extension
                });
            }
            return result;
        }
        public static void matchingContent(string filePath, string matchingKey)
        {
            bool replaced = false;
            MatchCollection mc;
            string mcStr;
            FileStream readFile = new FileStream(filePath, FileMode.Open);
            StreamReader re = new StreamReader(readFile);
            string lineStr = "";
            List<string> lineStrs = new List<string>();
            while ((lineStr = re.ReadLine()) != null)
            {
                Regex reg = new Regex(matchingKey);
                if (reg.IsMatch(lineStr))
                {
                    mc = reg.Matches(lineStr);
                    for (var i = 0; i < mc.Count; i++)
                    {
                        mcStr = mc[i].Value;
                        if (FileHelper.onMatchingSucceedDelegate != null)
                        {
                            string replaceStr = onMatchingSucceedDelegate(mcStr);
                            if (replaceStr != mcStr)
                            {
                                replaced = true;
                                lineStr = lineStr.Replace(mcStr, replaceStr);
                            }
                        }
                        Console.WriteLine(mcStr);
                    }
                }
                lineStrs.Add(lineStr);
            }

            //替换后的内容写回文件
            if (replaced)
            {
                StreamWriter sw = new StreamWriter(readFile);
                readFile.SetLength(0);
                foreach (string line in lineStrs)
                {
                    sw.WriteLine(line);
                }
                sw.Close();
            }
            re.Close();
            readFile.Close();
        }
        public static event MatchingSucceedDelegate onMatchingSucceedDelegate;
        public static void batchReplacement(ReplacementOption options)
        {
            //过滤需要匹配的文件
            IList<File> files = filterFile(options.directory, options.filterExtensions, options.sameLevel);
            //匹配文件内容
            foreach (File f in files)
            {
                matchingContent(f.filePath, options.matchingKey);
            }
        }

    }

    /// <summary>
    /// 委托 匹配成功时
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns>返回处理结果</returns>
    public delegate string MatchingSucceedDelegate(string value);

}
