using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Xunit;

namespace CapraLib.MemoryAllocater.Test
{
    public class ObjectPinnerTest
    {
        #region Allocate Tests

        [Fact]
        public void AllocateInt()
        {
            using(var allocated = new ObjectPinner<int>(out IntPtr unmanaged, 100))
            {
                
            }
        }

        [Fact]
        public void AllocateDouble()
        {
            using(var allocated = new ObjectPinner<double>(out IntPtr unmanaged, MathF.PI))
            {
                
            }
        }

        [Fact]
        public void AllocateStruct()
        {
            using(var allocated = new ObjectPinner<SampleStrcut>(out IntPtr unmanaged, new SampleStrcut()))
            {
                
            }
        }

        #endregion
    
        #region Control Tests

        [Theory]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void AllocateAndChangeInt(int changeTo)
        {
            int value = 100;

            using(var allocated = new ObjectPinner<int>(out IntPtr unmanaged, value))
            {
                Marshal.WriteInt32(unmanaged, changeTo);

                allocated.SetResult(out value);
            }

            Assert.Equal(changeTo, value);
        }

        public static IEnumerable<object[]> SampleStructGenerator()
        {
            yield return new object[] { new SampleStrcut() };
            yield return new object[] { new SampleStrcut() { x = 10f, y = 15f, z = 9f } };
            yield return new object[] { new SampleStrcut() 
            {
                x = MathF.E, y = MathF.E, z = MathF.E,
                dx = MathF.PI, dy = MathF.PI, dz = MathF.PI
            }};
        }

        [Theory]
        [MemberData(nameof(SampleStructGenerator))]
        public void AllocateAndChangeStruct(SampleStrcut changeTo)
        {
            var sample = new SampleStrcut();

            using(var allocated = new ObjectPinner<SampleStrcut>(out IntPtr unmanaged, sample))
            {
                Marshal.StructureToPtr(changeTo, unmanaged, fDeleteOld: false);

                allocated.SetResult(out sample);
            }

            Assert.Equal(changeTo, sample);
        }

        #endregion
    
        [StructLayout(LayoutKind.Sequential)]
        public struct SampleStrcut
        {
            public float x;
            public float y;
            public float z;
            public float dx;
            public float dy;
            public float dz;
        }
    }
}
