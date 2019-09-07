using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Support memory allocation.
    /// 
    /// </summary>
    public static class MemoryLock
    {
        /// <summary>
        /// 
        /// Run the funtion with memory allocated.
        /// 
        /// </summary>
        public static void Allocate(int size, MemoryAllocationHandle handle)
        {
            var allocated = Marshal.AllocCoTaskMem(size);
            
            handle.Invoke(allocated);

            Marshal.FreeCoTaskMem(allocated);
        }

        /// <summary>
        /// 
        /// Run the function with the native object and reflect its changes to the managed object.
        /// 
        /// </summary>
        public static void AsNative<T>(ref T managed, MemoryAllocationHandle handle) where T : unmanaged
        {
            var allocated = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(T)));
            Marshal.StructureToPtr(managed, allocated, fDeleteOld: false);

            handle.Invoke(allocated);

            managed = Marshal.PtrToStructure<T>(allocated);
            Marshal.FreeCoTaskMem(allocated);
        }
    }

    public delegate void MemoryAllocationHandle(IntPtr ptr);
}