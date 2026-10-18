using System.Text.RegularExpressions;

namespace Ticketing.Domain.Entities.Users;

public static class NormalizePhone
{

    public static string NormalizeIranPhoneNumber(this string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return string.Empty;

        string cleaned = Regex.Replace(phone, @"[^\d]", "");

        if (cleaned.Length == 0)
            return string.Empty;

        if (cleaned.StartsWith("0098"))
            cleaned = cleaned.Substring(4);

        else if (cleaned.StartsWith("98") && cleaned.Length > 10)
            cleaned = cleaned.Substring(2);

        if (cleaned.StartsWith("0"))
        {
            cleaned = "98" + cleaned.Substring(1);
        }
        else if (cleaned.StartsWith("9") && cleaned.Length == 10)
        {
            cleaned = "98" + cleaned;
        }
        else if (!cleaned.StartsWith("98"))
        {
            cleaned = "98" + cleaned;
        }

        return cleaned;
    }
}
