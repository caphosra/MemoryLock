using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Xunit;
using CapraLib.MemoryLock.Test.Samples;

namespace CapraLib.MemoryLock.Test
{
    public class GCAllocaterTest
    {
        [Theory]
        [ClassData(typeof(Int32Samples))]
        public void AllocateAndChangeInt(int sample)
        {
            int answer = sample;
            Int32Calc.SquareManaged(ref answer);

            int result = sample;

            using(var allocated = new GCAllocater<int>(out IntPtr unmanaged, result))
            {
                Int32Calc.SquareUnsafe(unmanaged);

                allocated.SetResult(out result);
            }

            Assert.Equal(answer, result);
        }

        [Theory]
        [ClassData(typeof(LineDataSamples))]
        public void AllocateAndChangeStruct(LineData sample)
        {
            var answer = sample;
            LineDataCalc.Turn180Managed(ref sample);

            var result = sample;

            using(var allocated = new GCAllocater<LineData>(out IntPtr unmanaged, result))
            {
                LineDataCalc.Turn180Unsafe(unmanaged);

                allocated.SetResult(out result);
            }

            Assert.Equal(answer, result);
        }
    }
}
