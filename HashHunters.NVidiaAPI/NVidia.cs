using System;
using System.Runtime.InteropServices;

namespace HashHunters.NVidiaAPI
{
    internal delegate IntPtr ADL_MAIN_MALLOC_CALLBACK (int size);

    internal static class NVidia
    {
        internal const int ADL_MAX_PATH = 256;
        internal const int ADL_MAX_ADAPTERS = 40 /* 150 */;
        

        internal static ADL_MAIN_MALLOC_CALLBACK ADL_Main_Memory_Alloc = Marshal.AllocCoTaskMem;

        internal const string AtiLib = "atiadlxx.dll";
        internal const string Kernel32Lib = "kernel32.dll";

        [DllImport(AtiLib)]
        internal static extern int ADL2_Main_Control_Create(ADL_MAIN_MALLOC_CALLBACK callback, int iEnumConnectedAdapters, out IntPtr adlContext);

        [DllImport(AtiLib)]
        internal static extern int ADL2_Adapter_NumberOfAdapters_Get(IntPtr context, out int lpNumAdapters);

        [DllImport(AtiLib)]
        internal static extern int ADL2_Adapter_Active_Get(IntPtr context, int iAdapterIndex, out int lpStatus);

        //[DllImport(AtiLib)]
        //internal static extern int ADL2_Adapter_AdapterInfoX2_Get(IntPtr context, out AdapterInfo[] lppAdapterInfo);

        //[DllImport(AtiLib)]
        //internal static extern int ADL2_Adapter_VideoBiosInfo_Get(IntPtr context, int iAdapterIndex, out ADLBiosInfo lpBiosInfo);


        //[DllImport(AtiLib)]
        //internal static extern int ADL2_OverdriveN_CapabilitiesX2_Get(IntPtr context, int iAdapterIndex, out ADLODNCapabilities lpODCapabilities);

        //[DllImport(AtiLib)]
        //internal static extern int ADL2_OverdriveN_Temperature_Get(IntPtr context, int iAdapterIndex, int iTemperatureType, out int iTemperature);

        //[DllImport(AtiLib)]
        //internal static extern int ADL2_OverdriveN_PowerLimit_Get(IntPtr context, int iAdapterIndex, out ADLODNPowerLimitSetting lpODPowerLimit);

        //[DllImport(AtiLib)]
        //internal static extern int ADL2_OverdriveN_FanControl_Get(IntPtr context, int iAdapterIndex, out ADLODNFanControl lpODFanSpeed);

        //[DllImport(AtiLib)]
        //internal static extern int ADL2_OverdriveN_FanControl_Set(IntPtr context, int iAdapterIndex, ADLODNFanControl lpODFanControl);

        //[DllImport(AtiLib)]
        //internal static extern int ADL2_OverdriveN_PerformanceStatus_Get(IntPtr context, int iAdapterIndex, out ADLODNPerformanceStatus lpODPerformanceStatus);

        [DllImport(AtiLib)]
        internal static extern int ADL2_Main_Control_Destroy(IntPtr context);
    }
}
