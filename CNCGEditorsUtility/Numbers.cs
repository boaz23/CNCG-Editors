namespace CNCGEditors {
    internal static class Numbers {
        public static uint ConvertDecimalToUInt32(decimal value) {
            return unchecked((uint)value);
        }
    }
}