using System;
using System.Collections.Generic;

namespace HashHunters.MinerMonitor.Common.DTO
{
    public class FirebaseRig
    {
        public DateTime HealthCheck { get; set; }
        public DateTime LastStart { get; set; }
        public string MachineName { get; set; }
        public List<DateTime> ServiceStarts { get; set; }
    }
}
