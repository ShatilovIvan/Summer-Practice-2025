namespace task01;
public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (String.IsNullOrEmpty(input))
            return false;

        string lowerStr = input.ToLower();
        string compareStr = string.Empty;

        foreach (char c in lowerStr)
        {
            if (char.IsPunctuation(c) || char.IsWhiteSpace(c))
                continue;

            compareStr += c;
        }

        char[] chars = compareStr.ToArray();
        Array.Reverse(chars);
        string compareStrReverse = new string(chars);

        return compareStr == compareStrReverse;
    }
}
