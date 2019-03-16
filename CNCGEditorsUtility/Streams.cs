using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace CNCGEditors.IO {
    internal static class Streams {
        public static byte[] Read(this Stream stream, int count) {
            byte[] buffer;

            if (count == 0) {
                buffer = EmptyArray<byte>.Value;
            }
            else {
                buffer = new byte[count];
                if (stream.Read(buffer, 0, count) < count) {
                    throw new EndOfStreamException();
                }
            }
            return buffer;
        }
        [HandleProcessCorruptedStateExceptions]
        public static T ReadStructure<T>(this Stream inputStream) where T : struct {
            int size;

            size = Marshal.SizeOf(typeof(T));
            {
                byte[] buffer;

                buffer = inputStream.Read(size);
                {
                    IntPtr ptr;

                    ptr = Marshal.AllocHGlobal(size);
                    try {
                        Marshal.Copy(buffer, 0, ptr, size);
                        {
                            T structure;

                            structure = (T)Marshal.PtrToStructure(ptr, typeof(T));
                            Marshal.FreeHGlobal(ptr);
                            return structure;
                        }
                    }
                    catch {
                        Marshal.FreeHGlobal(ptr);
                        throw;
                    }
                }
            }
        }
        public static void Write(this Stream stream, byte[] buffer) {
            int length;

            length = buffer.Length;
            if (length == 0) {
                return;
            }
            stream.Write(buffer, 0, length);
        }
        [HandleProcessCorruptedStateExceptions]
        public static void WriteStructure<T>(this Stream outputStream, T structure) where T : struct {
            int size;

            size = Marshal.SizeOf(typeof(T));
            {
                byte[] buffer;

                buffer = new byte[size];
                {
                    IntPtr ptr;

                    ptr = Marshal.AllocHGlobal(size);
                    try {
                        Marshal.StructureToPtr(structure, ptr, true);
                        Marshal.Copy(ptr, buffer, 0, size);
                        Marshal.FreeHGlobal(ptr);
                        outputStream.Write(buffer, 0, size);
                    }
                    catch {
                        Marshal.FreeHGlobal(ptr);
                        throw;
                    }
                }
            }
        }
    }
}