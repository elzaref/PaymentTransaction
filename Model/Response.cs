using System.Runtime.InteropServices;

namespace Model
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class Response
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] ResponseCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Message;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] ApprovalCode;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] DateTime;
    }
}
