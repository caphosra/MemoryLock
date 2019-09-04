using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Assign a memory on heap and copy the managed object to there.
    /// 
    /// </summary>
    public class HGlobalAllocater<T> : MemoryAllocater<T> where T : unmanaged
    {
        /// <summary>
        /// 
        /// The constructor of HGlobalAllocater
        /// 
        /// </summary>
        public HGlobalAllocater(out IntPtr unmanaged, in T managed) : base(out unmanaged, managed) { }

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
            unmanaged = Marshal.AllocHGlobal(ObjectSize);
            Pointer = unmanaged;
            
            //
            // Copy the managed object to unmanaged space.
            //
            Marshal.StructureToPtr(managed, unmanaged, fDeleteOld: false);
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