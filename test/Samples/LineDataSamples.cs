using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock.Test.Samples
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LineData
    {
        public float x;
        public float y;
        public float z;
        public float dx;
        public float dy;
        public float dz;
    }

    public class LineDataSamples : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new LineData() };
            yield return new object[] { new LineData() { x = 10f, y = 15f, z = 9f } };
            yield return new object[] { new LineData() 
            {
                x = MathF.E, y = MathF.E, z = MathF.E,
                dx = MathF.PI, dy = MathF.PI, dz = MathF.PI
            }};
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public static class LineDataCalc
    {
        public static void Turn180Managed(ref LineData data)
        {
            data.dx = - data.dx;
            data.dy = - data.dy;
            data.dz = - data.dz;
        }

        public static unsafe void Turn180Unsafe(IntPtr ptr)
        {
            LineData* native_ptr = (LineData*)ptr;
            float* dx_ptr = (float*)((native_ptr + sizeof(float) * 3));
            float* dy_ptr = dx_ptr + sizeof(float);
            float* dz_ptr = dy_ptr + sizeof(float);
            *dx_ptr = - *dx_ptr;
            *dy_ptr = - *dy_ptr;
            *dz_ptr = - *dz_ptr;
        }
    }
}