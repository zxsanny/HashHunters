namespace HashHunters.AMDAPI.Structures
{
    internal struct ADLODNCapabilities
    {
        internal int iMaximumNumberOfPerformanceLevels;
        internal ADLODNParameterRange sEngineClockRange;
        internal ADLODNParameterRange sMemoryClockRange;
        internal ADLODNParameterRange svddcRange;
        internal ADLODNParameterRange power;
        internal ADLODNParameterRange powerTuneTemperature;
        internal ADLODNParameterRange fanTemperature;
        internal ADLODNParameterRange fanSpeed;
        internal ADLODNParameterRange minimumPerformanceClock;
    }

    internal struct ADLODNParameterRange
    {
        internal int iMode;
        internal int iMin;
        internal int iMax;
        internal int iStep;
        internal int iDefault;
    }
}
