namespace CapraLib.MemoryLock
{
    public interface IMemoryAllocater<T> where T : unmanaged
    {
        void SetResult(out T managed);
    }
}