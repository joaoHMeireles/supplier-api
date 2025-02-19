namespace SupplierAPI.Helpers;

public static class DataMaskHelper
{
    public const int NUMBER_OF_CHARACTERS_TO_SHOW = 6;

    public static string MaskInformation(string value)
    {
        var valueLength = value.Length;
        var maskLength = valueLength - NUMBER_OF_CHARACTERS_TO_SHOW;
        
        return string.Concat(
            new string('*', maskLength), 
            value.AsSpan(maskLength, NUMBER_OF_CHARACTERS_TO_SHOW)
        );
    }
}