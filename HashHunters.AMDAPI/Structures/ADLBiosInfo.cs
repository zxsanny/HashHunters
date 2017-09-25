using System.Runtime.InteropServices;

namespace HashHunters.AMDAPI.Structures
{
    internal struct ADLBiosInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL.ADL_MAX_PATH)]
        internal string strPartNumber;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL.ADL_MAX_PATH)]
        internal string strVersion;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL.ADL_MAX_PATH)]
        internal string strDate; // BIOS date in yyyy/mm/dd hh:mm format.
    }
}
