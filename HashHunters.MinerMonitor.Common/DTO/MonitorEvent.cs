using System;
using HashHunters.MinerMonitor.Common.DTO.Enums;

namespace HashHunters.MinerMonitor.Common.DTO
{
    public abstract class MonitorEvent
    {
        public EventType Type { get; set; }
        public DateTime Time { get; set; }
        
        protected MonitorEvent(EventType type)
        {
            Time = DateTime.UtcNow;
            Type = type;
        }
    }


}
