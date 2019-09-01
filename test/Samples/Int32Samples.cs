using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock.Test.Samples
{
    public class Int32Samples : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 0 };
            yield return new object[] { 100 };
            yield return new object[] { -100 };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public static class Int32Calc
    {
        public static void SquareManaged(ref int data)
        {
            data = data * data;
        }

        public static unsafe void SquareUnsafe(IntPtr ptr)
        {
            var ptr_native = (int*)ptr;
            *ptr_native = *ptr_native * *ptr_native;
        }
    }
}