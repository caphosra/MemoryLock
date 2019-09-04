using System;
using System.Runtime.InteropServices;

namespace CapraLib.MemoryLock
{
    /// <summary>
    /// 
    /// Assign a memory and copy the managed object to there.
    /// 
    /// </summary>
    public class MemoryAllocater<T> : IMemoryAllocater<T> where T : unmanaged
    {
        /// <summary>
        /// 
        /// The size of T on memory.
        /// 
        /// </summary>
        protected int ObjectSize => objectSize ?? Marshal.SizeOf(typeof(T));
        private int? objectSize = null;

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
        public MemoryAllocater(out IntPtr unmanaged, in T managed)
        {
            AllocateMemory(out unmanaged, managed);
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
        public void CopyTo(out T managed)
        {
            managed = Marshal.PtrToStructure<T>(Pointer);
        }

        /// <summary>
        /// 
        /// Update the managed object by using unmanaged byte code.
        /// 
        /// </summary>
        public void CopyTo(out Span<byte> managed)
        {
#if !WITHOUT_UNSAFE
            unsafe
            {
                managed = new Span<byte>(Pointer.ToPointer(), ObjectSize);
            }
#else
            throw new NotSupportedException("You can't call CopyTo(out Span<byte>) without allowing unsafe code.");
#endif
        }

        /// <summary>
        /// 
        /// Allocate a memory and copy the managed object to unmanaged space.
        /// This function only can be called internally. 
        ///  
        /// </summary>
        protected virtual void AllocateMemory(out IntPtr unmanaged, in T managed)
        {
            //
            // Allocate a memory.
            //
            unmanaged = Marshal.AllocCoTaskMem(ObjectSize);
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
        protected virtual void FreeMemory(in IntPtr unmanaged)
        {
            //
            // Release the memory.
            //
            Marshal.FreeCoTaskMem(unmanaged);
        }
    }
}