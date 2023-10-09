
public static class StringExtensions 
{
    public static string SetColor(this string inputText, string color)
    {
        return "<color=" + color + ">" + inputText + "</color>";
    }
}
