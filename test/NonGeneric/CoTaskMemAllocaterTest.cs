using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Xunit;
using CapraLib.MemoryLock.Test.Samples;

namespace CapraLib.MemoryLock.Test
{
    public class NonGenericCoTaskMemAllocaterTest
    {
        [Fact]
        public void AllocateMemory()
        {
            const int size = 100;

            using(var allocated = new CoTaskMemAllocater(out IntPtr unmanaged, size))
            {
                ByteArrayCalc.FillOneUnsafe(unmanaged, size);
            }
        }
    }
}
