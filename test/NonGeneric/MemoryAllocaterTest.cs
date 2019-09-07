using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Xunit;
using CapraLib.MemoryLock.Test.Samples;

namespace CapraLib.MemoryLock.Test
{
    public class NonGenericMemoryAllocaterTest
    {
        [Fact]
        public void AllocateMemory()
        {
            const int size = 100;

            using(var allocated = new MemoryAllocater(out IntPtr unmanaged, size))
            {
                ByteArrayCalc.FillOneUnsafe(unmanaged, size);
            }
        }

        [Fact]
        public unsafe void AllocateAndCopyWithSpan()
        {
            int sample = 100;

            using(var allocated = new MemoryAllocater<int>(out IntPtr unmanaged, sample))
            {
                allocated.CopyTo(out Span<byte> span);
            }
        }
    }
}
