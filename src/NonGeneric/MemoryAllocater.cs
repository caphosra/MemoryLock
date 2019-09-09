using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Assign a memory and copy the managed object to there.
    /// 
    /// </summary>
    public class MemoryAllocater : IMemoryAllocater
    {
        /// <summary>
        /// 
        /// The size of a memory.
        /// 
        /// </summary>
        protected int ObjectSize { get; set; }

        /// <summary>
        /// 
        /// The pointer which points the head of the space allocated.
        /// 
        /// </summary>
        public IntPtr Pointer { get; protected set; }

        /// <summary>
        /// 
        /// The constructor of MemoryAllocater
        /// 
        /// </summary>
        public MemoryAllocater(out IntPtr unmanaged, int size)
        {
            AllocateMemory(out unmanaged, size);
        }

        /// <summary>
        /// 
        /// A implementation for IDisposable.
        /// You never call this funtion twice.
        /// 
        /// </summary>
        public void Dispose()
        {
            FreeMemory(Pointer);
        }

        /// <summary>
        /// 
        /// Update the managed object by using unmanaged byte code.
        /// 
        /// </summary>
        public void CopyTo(out byte[] managed)
        {
            managed = new byte[ObjectSize];
            Marshal.Copy(Pointer, managed, 0, ObjectSize);
        }

        /// <summary>
        /// 
        /// Allocate a memory and copy the managed object to unmanaged space.
        /// This function only can be called internally. 
        ///  
        /// </summary>
        protected virtual void AllocateMemory(out IntPtr unmanaged, int size)
        {
            //
            // Allocate a memory.
            //
            ObjectSize = size;
            unmanaged = Marshal.AllocCoTaskMem(ObjectSize);
            Pointer = unmanaged;
        }

        /// <summary>
        /// 
        /// Free the memory which is allocated by this.
        /// This function only can be called internally. 
        ///  
        /// </summary>
        protected virtual void FreeMemory(in IntPtr unmanaged)
        {
            //
            // Release the memory.
            //
            Marshal.FreeCoTaskMem(unmanaged);
        }
    }
}
