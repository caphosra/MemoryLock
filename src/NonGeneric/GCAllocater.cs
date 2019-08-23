using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    public class GCAllocater : IMemoryAllocater, IDisposable
    {
        private GCHandle _handle;

        public GCAllocater(out IntPtr unmanaged, object managed)
        {
            _handle = GCHandle.Alloc(managed, GCHandleType.Pinned);
            unmanaged = _handle.AddrOfPinnedObject();
        }

        public void SetResult(out object managed)
        {
            managed = _handle.Target;
        }

        public void SetResult(out Span<byte> bytes)
        {
            unsafe
            {
                var size =  Marshal.SizeOf(_handle.Target.GetType());
                bytes = new Span<byte>((void*)_handle.AddrOfPinnedObject(), size);
            }
        }

        public void Dispose()
        {
            _handle.Free();
        }
    }
}