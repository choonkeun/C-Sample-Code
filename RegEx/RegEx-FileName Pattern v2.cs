#define LOG
//#define CHECK

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace COFolderHasFile
{
    public class FolderHasFile
    {
        public String LogData { get; set; }

        public String WatchFolder { get; set; }
        public String Filter { get; set; }
        public Boolean IgnoreCase { get; set; }
        public Int32 Count { get; set; }

#if LOG
        public String LogText { get; set; }
#else
        //public String LogText { get; set; }        //for debug
#endif
        public FolderHasFile()
        {
            sessionNo = Guid.NewGuid();
            LogData = string.Empty;

            WatchFolder = string.Empty;
            Filter = string.Empty;
            IgnoreCase = true;
        }

#region FileCount
        private Boolean isSuccess = false;
        private String logging = String.Empty;

        private List<string> fileList = new List<string>();
        private List<string> regexFilterList = new List<string>();
        private String searchPattern = String.Empty;

        private MatchCollection matches;
        private RegexOptions options = RegexOptions.IgnorePatternWhitespace;

        public bool FileCount()
        {
            if (IgnoreCase)
                options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;   //default is case-sensitive

            try
            {
                //build target search pattern array
                string[] filters = Regex.Replace(Filter, @"\s+", string.Empty).Split(',');
                for (int i = 0; i < filters.Length; i++)
                {
                    searchPattern = Build_RegEx_Pattern(filters[i]);   //convert input Filter to RegEx format
                    regexFilterList.Add(searchPattern);
                }

                //build directory search pattern array
                for (int i = 0; i < filters.Length; i++)
                {
                    filters[i] = "*." + filters[i].Split('.').Last();
                }

                //get all files that in the folder
                string[] filePaths = filters.SelectMany(f => Directory.GetFiles(WatchFolder, f)).Distinct().ToArray();
                string fname = string.Empty;
                foreach (string f in filePaths)
                {
                    fname = Path.GetFileName(f);
                    isSuccess = isMatchedFile(fname);
                    if (isSuccess)
                        fileList.Add(f);
                }
            }
            catch (Exception ex)
            {
                string loggingData = String.Format("Error: {0}", ex.Message);
                if (ex.StackTrace != null)
                    loggingData += String.Format("Error: {0}", ex.StackTrace);
                LogData = loggingData;

            }

            Count = fileList.Count;
            isSuccess = Count > 0 ? true : false;
#if LOG
            if (isSuccess)
                LogText = string.Join(Environment.NewLine, fileList.ToArray());
#endif
            GC.Collect();
            return isSuccess;
        }

#endregion

#region isMatchedFile
        public Boolean isMatchedFile(String fileName)
        {
            Boolean returnValue = false;
            try
            {
                foreach (var filter in regexFilterList)
                {
                    matches = Regex.Matches(fileName, filter, options);

                    //check Match results
                    if (matches.Count > 0)
                    {
                        returnValue = true;
                        //returnValue = matches[0].Groups[0].Captures[0].Length == fileName.Length ? true : false;
                        return returnValue;
                    }
                    else
                        returnValue = false;
                }

#if CHECK
                matches = Regex.Matches(fileName, searchPattern, options);
                foreach (Match match in matches)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        Console.WriteLine("Index={0}, Value={1}", capture.Index, capture.Value);
                    }
                }
#endif
                return returnValue;
            }
            catch (Exception ex)
            {
                //String message = String.Format(" Error checking file {1} with pattern {2}, Message={0}", ex.Message, fileName, searchPattern);
                //if (ex.StackTrace != null)
                //    message += String.Format(", {0}", ex.StackTrace);
                
                throw new Exception(message);
            }
        }
        /// <summary>
        /// ---inside bracket use
        /// [YYYYMMDD] [YYYY-MM-DD] [YYMMDD] [YY-MM-DD] [MMDDYYYY] [MM-DD-YYYY] [MMDDYY] [MM-DD-YY]
        /// 
        /// ---outside bracket use
        /// @: Alpha Only [a-zA-Z]
        /// #: Numeric Only [0-9]
        /// +: AlphaNumeric [0-9a-zA-Z]
        /// .: Any character include special character
        /// *: Any character and Any length
        /// |: or 
        /// (): group            (m|M|o)(c|C) same as [mMo][cC] but [] is not a group
        /// (|): group or group  (mc|oc|xx) --> 'mc' or 'oc' or 'xx'
        /// ---
        /// \/:*?"<>| is not allowed for filename
        /// sample: txtFilter.Text = @"*[YYYYMMDD]*.txt, *.zip, *.docx, h*.jpg, *setting.jpg";
        /// sample: txtFilter.Text = @"*[YYYYMMDD]*.txt, *.zip, *.docx, h*.jpg, *dbc*.*, *setting.jpg, *.*";
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        
        //------------------------------------------------------------------------------------------
        //This program can not cover 'RegEx Group'
        //sample searchPattern: (mc|oc|xx)#.(ad|ADM|loc|LOC|svc|SVC)*[YYYYMMDD]*.(txt|csv)
        //------------------------------------------------------------------------------------------
       
        private static string Build_RegEx_Pattern(String searchPattern)
        {
            Boolean hasDate = searchPattern.Contains("[");
            if (hasDate)
            {
                string datePattern = Regex.Match(searchPattern, @"\[(.+)\]").Groups[1].Value.ToLower();

                string[] seperator = { "-", "/", "." };
                bool hasSeperator = seperator.Any(x => datePattern.Contains(x));

                string tmpPattern = Regex.Replace(datePattern, @"[-/.]", "[-/.]");
                tmpPattern = Regex.Replace(tmpPattern, @"yyyy", "(19|20)[0-9][0-9]");
                tmpPattern = Regex.Replace(tmpPattern, @"yy", "[0-9][0-9]");
                tmpPattern = Regex.Replace(tmpPattern, @"mm", "(0[1-9]|1[012])");
                tmpPattern = Regex.Replace(tmpPattern, @"dd", "(0[0-9]|[12][0-9]|3[01])");

                searchPattern = searchPattern.ToLower().Replace("[" + datePattern + "]", tmpPattern);
            }

            //translate
            searchPattern = searchPattern.Replace("@", "[a-zA-Z]");
            searchPattern = searchPattern.Replace("#", "[0-9]");
            searchPattern = searchPattern.Replace("+", "[0-9a-zA-Z]");
            //searchPattern = searchPattern.Replace(".", "Any character (except \n newline)");

            //change '*' to '.*' for Regex use
            searchPattern = searchPattern.Replace("*", ".*");
            return searchPattern;
        }
#endregion


    }
}
