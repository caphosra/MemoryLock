using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryAllocater
{
    public class CoTaskMemAllocater<T> : IDisposable, IMemoryAllocater<T> where T : unmanaged
    {
        private IntPtr _allocated;

        public CoTaskMemAllocater(out IntPtr unmanaged, in T managed)
        {
            _allocated = unmanaged = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(T)));
            
            Marshal.StructureToPtr(managed, _allocated, fDeleteOld: false);
        }
        
        public void SetResult(out T managed)
        {
            managed = Marshal.PtrToStructure<T>(_allocated);
        }

        public void Dispose()
        {
            Marshal.FreeCoTaskMem(_allocated);
        }
    }
}
