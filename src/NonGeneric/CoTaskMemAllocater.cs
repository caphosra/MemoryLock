using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Assign a memory and copy the managed object to there.
    /// 
    /// </summary>
    public sealed class CoTaskMemAllocater : MemoryAllocater
    {
        /// <summary>
        /// 
        /// The constructor of CoTaskMemAllocater
        /// 
        /// </summary>
        public CoTaskMemAllocater(out IntPtr unmanaged, int size) : base(out unmanaged, size) { }
    }
}
