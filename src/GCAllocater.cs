using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Assign a memory by GC and copy the managed object to there.
    /// 
    /// </summary>
    public class GCAllocater<T> : MemoryAllocater<T> where T : unmanaged
    {
        /// <summary>
        /// 
        /// The handle of the memory allocated.
        /// 
        /// </summary>
        private GCHandle _GCHandle;

        /// <summary>
        /// 
        /// The constructor of GCAllocater
        /// 
        /// </summary>
        public GCAllocater(out IntPtr unmanaged, in T managed) : base(out unmanaged, managed) { }

        /// <summary>
        /// 
        /// Allocate a memory and copy the managed object to unmanaged space.
        /// This function only can be called internally. 
        ///  
        /// </summary>
        protected override void AllocateMemory(out IntPtr unmanaged, in T managed)
        {
            //
            // Allocate a memory.
            //
            _GCHandle = GCHandle.Alloc(managed, GCHandleType.Pinned);
            Pointer = unmanaged = _GCHandle.AddrOfPinnedObject();
        }

        /// <summary>
        /// 
        /// Free the memory which is allocated by this.
        /// This function only can be called internally. 
        ///  
        /// </summary>
        protected override void FreeMemory(in IntPtr unmanaged)
        {
            //
            // Release the memory.
            //
            _GCHandle.Free();
        }
    }
}