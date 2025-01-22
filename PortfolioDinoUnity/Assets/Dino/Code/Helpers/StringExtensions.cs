
using UnityEngine;

public static class StringExtensions 
{
    public static string SetColor(this string inputText, string color)
    {
        return "<color=" + color + ">" + inputText + "</color>";
    } 
    
    public static string SetColor(this string inputText, ColorString color)
    {
        return "<color=" + GetColor(color) + ">" + inputText + "</color>";
    }
    
    private static string GetColor(ColorString color)
    {
        string colorString = "";
        switch (color)
        {
            case ColorString.Red:
                colorString = "#ff5274";
                break;
            case ColorString.Green:
                colorString = "#98dd67";
                break;
            case ColorString.Blue:
                colorString = "##6ec8f8";
                break;
            case ColorString.Yellow:
                colorString = "#f1ef60";
                break;
            case ColorString.Orange:
                colorString = "#fb9f36";
                break;
            case ColorString.Purple:
                colorString = "#7d6efe";
                break;
                
        }


        return colorString;
    }
    
}

public enum ColorString
{
    Red,
    Green,
    Blue,
    Yellow,
    Orange,
    Purple,
}