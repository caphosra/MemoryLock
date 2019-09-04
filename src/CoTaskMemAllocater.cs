using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Assign a memory and copy the managed object to there.
    /// 
    /// </summary>
    public class CoTaskMemAllocater<T> : MemoryAllocater<T> where T : unmanaged
    {
        /// <summary>
        /// 
        /// The constructor of CoTaskMemAllocater
        /// 
        /// </summary>
        public CoTaskMemAllocater(out IntPtr unmanaged, in T managed) : base(out unmanaged, managed) { }
    }
}