using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Xunit;
using CapraLib.MemoryLock.Test.Samples;

namespace CapraLib.MemoryLock.Test
{
    public class MemoryLockTest
    {
        [Theory]
        [ClassData(typeof(Int32Samples))]
        public unsafe void AllocateTest(int sample)
        {
            var answer = sample;
            Int32Calc.SquareManaged(ref answer);

            var result = sample;

            MemoryLock.Allocate(4, (ptr) =>
            {
                var native_ptr = (int*)ptr;
                *native_ptr = sample;
                Int32Calc.SquareUnsafe(ptr);
                result = *native_ptr;
            });

            Assert.Equal(answer, result);
        }

        [Theory]
        [ClassData(typeof(LineDataSamples))]
        public void AsNativeTest(LineData sample)
        {
            var answer = sample;
            LineDataCalc.Turn180Managed(ref answer);

            var result = sample;

            MemoryLock.AsNative(ref result, (ptr) => 
            {
                LineDataCalc.Turn180Unsafe(ptr);
            });

            Assert.Equal(answer, result);
        }
    }
}
