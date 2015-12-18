using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core
{
    public class FileHelper
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
        /// <summary>
        /// 过滤文件
        /// </summary>
        /// <returns></returns>
        public static IList<File> filterFile(string directory, string filterExtensions)
        {
            List<File> result = new List<File>();
            DirectoryInfo TheFolder = new DirectoryInfo(directory);
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                if (filterExtensions.IndexOf(NextFolder.Name) < 0)
                    continue;
                string subDirectory = directory + "//" + NextFolder.Name;
                result.AddRange(filterFile(subDirectory, filterExtensions));
            }
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
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

        }
        public static void batchReplacement(ReplacementOption options)
        {
            //过滤需要匹配的文件
            IList<File> files = filterFile(options.directory, options.filterExtensions);
            //匹配文件内容
        }
    }
}
