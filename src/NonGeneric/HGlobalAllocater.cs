using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    public class HGlobalAllocater : IMemoryAllocater, IDisposable
    {
        private IntPtr _allocated;
        private int _size;

        public HGlobalAllocater(out IntPtr unmanaged, int size)
        {
            _allocated = unmanaged = Marshal.AllocHGlobal(size);
            _size = size;
        }

        public void SetResult(out Span<byte> bytes)
        {
            unsafe
            {
                bytes = new Span<byte>((void*)_allocated, _size);
            }
        }

        public void Dispose()
        {
            Marshal.FreeCoTaskMem(_allocated);
        }
    }
}
