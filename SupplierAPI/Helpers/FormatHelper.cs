using System.Text.RegularExpressions;

namespace SupplierAPI.Helpers;

public static class FormatHelper
{
    private static readonly Regex CnpjRegex = new(@"^\d{14}$", RegexOptions.Compiled);

    public static string FormatCnpj(string cnpj)
    {
        if (!CnpjRegex.IsMatch(cnpj))
        {
           return cnpj;
        }

        return Regex.Replace(cnpj, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
    }
}