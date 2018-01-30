using System;

namespace VisionuseTransliteration
{
    public static class Transliteration
    {
        public static string Transliterate(string InputString)
        {
            InputString = InputString.ToLower();

            InputString = InputString.Replace("а", "a");
            InputString = InputString.Replace("б", "b");
            InputString = InputString.Replace("в", "v");
            InputString = InputString.Replace("г", "g");
            InputString = InputString.Replace("д", "d");
            InputString = InputString.Replace("е", "e");
            InputString = InputString.Replace("ё", "yo");
            InputString = InputString.Replace("ж", "zh");
            InputString = InputString.Replace("з", "z");
            InputString = InputString.Replace("и", "i");
            InputString = InputString.Replace("й", "j");
            InputString = InputString.Replace("к", "k");
            InputString = InputString.Replace("л", "l");
            InputString = InputString.Replace("м", "m");
            InputString = InputString.Replace("н", "n");
            InputString = InputString.Replace("о", "o");
            InputString = InputString.Replace("п", "p");
            InputString = InputString.Replace("р", "r");
            InputString = InputString.Replace("с", "s");
            InputString = InputString.Replace("т", "t");
            InputString = InputString.Replace("у", "u");
            InputString = InputString.Replace("ф", "f");
            InputString = InputString.Replace("х", "kh");
            InputString = InputString.Replace("ц", "ts");
            InputString = InputString.Replace("ч", "ch");
            InputString = InputString.Replace("ш", "sh");
            InputString = InputString.Replace("щ", "shch");
            InputString = InputString.Replace("ъ", "");
            InputString = InputString.Replace("Ъ", "");
            InputString = InputString.Replace("ы", "y");
            InputString = InputString.Replace("ь", "");
            InputString = InputString.Replace("э", "e");
            InputString = InputString.Replace("ю", "yu");
            InputString = InputString.Replace("я", "ya");

            InputString = InputString.Replace(" ", "-");
            InputString = InputString.Replace("/", "-");

            InputString = InputString.Replace("(", "");
            InputString = InputString.Replace(")", "");
            InputString = InputString.Replace("[", "");
            InputString = InputString.Replace("]", "");
            InputString = InputString.Replace("<", "");
            InputString = InputString.Replace(">", "");
            InputString = InputString.Replace("?", "");
            InputString = InputString.Replace("=", "");

            return InputString;
        }
    }
}
