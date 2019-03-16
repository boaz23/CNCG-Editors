namespace CNCGEditors {
    internal static class Strings {
        public static string Reverse(this string str) {
            char[] strReversedCharsArray;
            int strLength;

            strLength = str.Length;
            strReversedCharsArray = new char[strLength];
            for (int i = 0; i < strLength; i++) {
                strReversedCharsArray[i] = str[strLength - i - 1];
            }
            return new string(strReversedCharsArray);
        }
    }
}