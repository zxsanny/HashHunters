namespace HashHunters.AMDAPI.Structures
{
    internal struct ADLODNPerformanceStatus
    {
        internal int iCoreClock;
        internal int iMemoryClock;
        internal int iDCEFClock;
        internal int iGFXClock;
        internal int iUVDClock;
        internal int iVCEClock;
        internal int iGPUActivityPercent;
        internal int iCurrentCorePerformanceLevel;
        internal int iCurrentMemoryPerformanceLevel;
        internal int iCurrentDCEFPerformanceLevel;
        internal int iCurrentGFXPerformanceLevel;
        internal int iUVDPerformanceLevel;
        internal int iVCEPerformanceLevel;
        internal int iCurrentBusSpeed;
        internal int iCurrentBusLanes;
        internal int iMaximumBusLanes;
        internal int iVDDC;
        internal int iVDDCI;
    }
}
