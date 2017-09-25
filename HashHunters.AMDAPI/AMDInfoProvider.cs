using System;
using System.Collections.Generic;
using HashHunters.AMDAPI.Structures;

namespace HashHunters.AMDAPI
{
    internal static class AMDInfoProvider
    {
        internal static List<GPUInfo> Get()
        {
            IntPtr adlContext;

            if (ADL.ADL2_Main_Control_Create(ADL.ADL_Main_Memory_Alloc, 1, out adlContext) !=
                (int) ADLResult.ADLSuccess)
                throw new Exception("Can't create main context!");

            AdapterInfo[] adapterInfo;
            if (ADL.ADL2_Adapter_AdapterInfoX2_Get(adlContext, out adapterInfo) != (int) ADLResult.ADLSuccess)
                throw new Exception("Can't retrieve adapter infos!");

            foreach (var adapter in adapterInfo)
            {
                int status;
                if (ADL.ADL2_Adapter_Active_Get(adlContext, adapter.iAdapterIndex, out status) !=
                    (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't retrieve state!");
                if (status == 0)
                    continue;

                ADLBiosInfo lpBiosInfo;
                if (ADL.ADL2_Adapter_VideoBiosInfo_Get(adlContext, adapter.iAdapterIndex, out lpBiosInfo) !=
                    (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't retrieve BIOS info!");

                ADLODNCapabilities lpODCapabilities;
                if (ADL.ADL2_OverdriveN_CapabilitiesX2_Get(adlContext, adapter.iAdapterIndex, out lpODCapabilities) !=
                    (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't retrieve capabilities!");

                int temp;
                if (ADL.ADL2_OverdriveN_Temperature_Get(adlContext, adapter.iAdapterIndex, 1, out temp) !=
                    (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't retrieve temperature!");

                ADLODNPowerLimitSetting lpODPowerLimit;
                if (ADL.ADL2_OverdriveN_PowerLimit_Get(adlContext, adapter.iAdapterIndex, out lpODPowerLimit) !=
                    (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't retrieve power limit!");

                ADLODNFanControl lpODFanSpeed;
                if (ADL.ADL2_OverdriveN_FanControl_Get(adlContext, adapter.iAdapterIndex, out lpODFanSpeed) !=
                    (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't retrieve fan info!");

                lpODFanSpeed.iMode = 1;
                lpODFanSpeed.iTargetTemperature = 58;
                lpODFanSpeed.iCurrentFanSpeedMode = 3;
                if (ADL.ADL2_OverdriveN_FanControl_Set(adlContext, adapter.iAdapterIndex, lpODFanSpeed) !=
                    (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't set fan info!");

                ADLODNPerformanceStatus lpODPerformanceStatus;
                if (ADL.ADL2_OverdriveN_PerformanceStatus_Get(adlContext, adapter.iAdapterIndex,
                        out lpODPerformanceStatus) != (int) ADLResult.ADLSuccess)
                    throw new Exception("Can't retrieve performance status!");
            }

            ADL.ADL2_Main_Control_Destroy(adlContext);
            
            return new List<GPUInfo>();
        }
    }
}
