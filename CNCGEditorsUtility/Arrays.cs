namespace CNCGEditors {
    internal static class Arrays {
        public static byte[] InvertBytesInByteArray(byte[] bytes) {
            int bytesLength;

            bytesLength = bytes.Length;
            for (int i = 0; i < bytesLength; i++) {
                bytes[i] = (byte)~bytes[i];
            }
            return bytes;
        }
    }
    internal static class EmptyArray<T> {
        public static readonly T[] Value = new T[0];
    }
}