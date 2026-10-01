using System.Text.RegularExpressions;

namespace Learning_ASP.NET_MVC_.Helper;

public static class RemoveHtmlTag
{
    public static string RemoveHtmlTags(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        return Regex.Replace(input, "<.*?>|&.*?;|&nbsp;", string.Empty);
    }
}