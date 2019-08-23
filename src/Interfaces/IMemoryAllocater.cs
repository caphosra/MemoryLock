using System;

namespace CapraLib.MemoryLock
{
    public interface IMemoryAllocater<T> where T : unmanaged
    {
        void SetResult(out T managed);
    }

    public interface IMemoryAllocater
    {
        void SetResult(out Span<byte> bytes);
    }
}