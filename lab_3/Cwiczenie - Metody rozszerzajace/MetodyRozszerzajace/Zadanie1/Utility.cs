namespace Extensions
{
    public static class Utility
    {
        public static string BezSamoglosek(this string napis)
        {
            var bezSamoglosek = default(string);
            string[] samogloski = new string[]
            {
                "a",
                "A",
                "e",
                "E",
                "i",
                "I",
                "o",
                "O",
                "u",
                "U",
                "ó",
                "Ó",
                "y",
                "Y",
                "ą",
                "Ą",
                "ę",
                "Ę"
            };
            foreach (var samogloska in samogloski)
            {
                bezSamoglosek = bezSamoglosek.Replace(samogloska, "");
            }
            return bezSamoglosek;
        }
    }
}