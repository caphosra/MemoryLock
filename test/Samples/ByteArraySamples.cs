using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock.Test.Samples
{
    public static class ByteArrayCalc
    {
        public static unsafe void FillOneUnsafe(IntPtr ptr, int size)
        {
            byte* native_ptr = (byte*)ptr;
            for(int i = 0; i < size; i++)
            {
                *(native_ptr + i) = 0xFF;
            }
        }
    }
}