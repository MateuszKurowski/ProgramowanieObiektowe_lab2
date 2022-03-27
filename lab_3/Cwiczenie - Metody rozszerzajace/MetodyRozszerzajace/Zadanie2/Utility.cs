namespace Extensions
{
    public static class Utility
    {
        public static bool IsNumeric(this string napis)
        {
            return int.TryParse(napis, out _);
        }
    }
}