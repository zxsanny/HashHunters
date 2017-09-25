using System.Runtime.InteropServices;

namespace HashHunters.AMDAPI.Structures
{
    public struct AdapterInfo
    {
        internal int iSize;
        internal int iAdapterIndex; //The ADL index handle. One GPU may be associated with one or two index handles.
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL.ADL_MAX_PATH)]
        internal string strUDID;
        internal int iBusNumber;
        internal int iDeviceNumber;
        internal int iFunctionNumber;
        internal int iVendorID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL.ADL_MAX_PATH)]
        internal string strAdapterName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL.ADL_MAX_PATH)]
        internal string strDisplayName;
        internal int iPresent;
        internal int iExist;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL.ADL_MAX_PATH)]
        internal string strDriverPath; //Driver registry path.
    }
}
