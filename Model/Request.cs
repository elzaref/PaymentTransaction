using System.Runtime.InteropServices;

namespace Model
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class Request
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] ProcessingCode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] SystemTraceNr;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] FunctionCode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] CardNo;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string CardHolder;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] AmountTrxn;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] CurrencyCode;
    }
}
