namespace MedicineReminderProj;

public static class Utils
{
    public static string ToUpperCase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return "";
        return text.ToUpper();
    }
}