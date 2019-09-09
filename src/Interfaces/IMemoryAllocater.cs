using System;

namespace CapraLib.MemoryLock
{
    public interface IMemoryAllocater<T> : IMemoryAllocater where T : unmanaged
    {
        void CopyTo(out T managed);
    }

    public interface IMemoryAllocater : IDisposable
    {
        void CopyTo(out byte[] bytes);
    }
}