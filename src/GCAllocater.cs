using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    public class GCAllocater<T> : IDisposable, IMemoryAllocater<T> where T : unmanaged
    {
        private GCHandle _handle;

        public GCAllocater(out IntPtr unmanaged, in T managed)
        {
            _handle = GCHandle.Alloc(managed, GCHandleType.Pinned);
            unmanaged = _handle.AddrOfPinnedObject();
        }

        public void SetResult(out T managed)
        {
            managed = (T)_handle.Target;
        }

        public void Dispose()
        {
            _handle.Free();
        }
    }
}