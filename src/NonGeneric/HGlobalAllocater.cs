using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Assign a memory on heap and copy the managed object to there.
    /// 
    /// </summary>
    public sealed class HGlobalAllocater : MemoryAllocater
    {
        /// <summary>
        /// 
        /// The constructor of HGlobalAllocater
        /// 
        /// </summary>
        public HGlobalAllocater(out IntPtr unmanaged, int size) : base(out unmanaged, size) { }

        /// <summary>
        /// 
        /// Allocate a memory and copy the managed object to unmanaged space.
        /// This function only can be called internally. 
        ///  
        /// </summary>
        protected override void AllocateMemory(out IntPtr unmanaged, int size)
        {
            //
            // Allocate a memory.
            //
            ObjectSize = size;
            unmanaged = Marshal.AllocHGlobal(ObjectSize);
            Pointer = unmanaged;
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
            Marshal.FreeHGlobal(unmanaged);
        }
    }
}
