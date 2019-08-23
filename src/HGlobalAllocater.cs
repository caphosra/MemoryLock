using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    public class HGlobalAllocater<T> : IDisposable, IMemoryAllocater<T> where T : unmanaged
    {
        private IntPtr _allocated;

        public HGlobalAllocater(out IntPtr unmanaged, in T managed)
        {
            _allocated = unmanaged = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));

            Marshal.StructureToPtr(managed, _allocated, fDeleteOld: false);
        }

        public void SetResult(out T managed)
        {
            managed = Marshal.PtrToStructure<T>(_allocated);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(_allocated);
        }
    }
}
