namespace CapraLib.MemoryAllocater
{
    public interface IMemoryAllocater<T> where T : unmanaged
    {
        void SetResult(out T managed);
    }
}