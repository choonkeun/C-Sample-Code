#region isWatchableFile
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
/// </summary>
/// <param name="fileName"></param>
/// <param name="searchPattern"></param>
/// <param name="ignoreCase"></param>
/// <returns></returns>
public static Boolean isWatchableFile(String fileName, String searchPattern, Boolean ignoreCase)
{
    Boolean returnValue = false;

    try
    {
        //searchPattern: (mc|oc|xx)#.(ad|ADM|loc|LOC|svc|SVC)*[YYYYMMDD]*.(txt|csv)
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

        //Regex.Matches
        MatchCollection matches;
        RegexOptions options = RegexOptions.IgnorePatternWhitespace;    //only apply to wild card like '*'
        if (ignoreCase)
            options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

        matches = Regex.Matches(fileName, searchPattern, options);
        //foreach (Match match in matches)
        //{
        //    foreach (Capture capture in match.Captures)
        //    {
        //        Console.WriteLine("Index={0}, Value={1}", capture.Index, capture.Value);
        //    }
        //}

        //check Match results
        if (matches.Count > 0)
            returnValue = matches[0].Groups[0].Captures[0].Length == fileName.Length ? true : false;
        else
            returnValue = false;

        return returnValue;
    }
    catch (Exception ex)
    {
        String message = String.Format(" Error checking file {1} with pattern {2}, Message={0}", ex.Message, fileName, searchPattern);
        if (ex.StackTrace != null)
            message += String.Format(", {0}", ex.StackTrace);

        throw new Exception(message);
    }
}
#endregion