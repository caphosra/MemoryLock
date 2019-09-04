using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Xunit;
using CapraLib.MemoryLock.Test.Samples;

namespace CapraLib.MemoryLock.Test
{
    public class CoTaskMemAllocaterTest
    {
        [Theory]
        [ClassData(typeof(Int32Samples))]
        public void AllocateAndChangeInt(int sample)
        {
            int answer = sample;
            Int32Calc.SquareManaged(ref answer);

            int result = sample;

            using(var allocated = new CoTaskMemAllocater<int>(out IntPtr unmanaged, result))
            {
                Int32Calc.SquareUnsafe(unmanaged);

                allocated.CopyTo(out result);
            }

            Assert.Equal(answer, result);
        }

        [Theory]
        [ClassData(typeof(LineDataSamples))]
        public void AllocateAndChangeStruct(LineData sample)
        {
            var answer = sample;
            LineDataCalc.Turn180Managed(ref answer);

            var result = sample;

            using(var allocated = new CoTaskMemAllocater<LineData>(out IntPtr unmanaged, result))
            {
                LineDataCalc.Turn180Unsafe(unmanaged);

                allocated.CopyTo(out result);
            }

            Assert.Equal(answer, result);
        }
    }
}
