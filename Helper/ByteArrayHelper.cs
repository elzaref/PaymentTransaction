using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ByteArrayHelper
    {
        public static byte[] ObjectToByteArray(object obj, int Newsize)
        {
            //var s = SizeOf(Request);
            var size = Marshal.SizeOf(obj);
            // Both managed and unmanaged buffers required.
            var bytes = new byte[Newsize];
            var ptr = Marshal.AllocHGlobal(size);
            // Copy object byte-to-byte to unmanaged memory.
            Marshal.StructureToPtr(obj, ptr, false);
            // Copy data from unmanaged memory to managed buffer.
            Marshal.Copy(ptr, bytes, 0, size);
            // Release unmanaged memory.
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }
        public static T ByteArrayToObject<T>(byte[] bytes)
        {
            GCHandle gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var data = (T)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(T));
            gcHandle.Free();
            return data;
        }
    }
}
