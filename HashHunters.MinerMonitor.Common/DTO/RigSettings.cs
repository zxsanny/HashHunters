using System;
using System.Collections.Generic;
using System.Linq;
using HashHunters.MinerMonitor.Common.Extensions;

namespace HashHunters.MinerMonitor.Common.DTO
{
    public class RigSettings
    {
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }

        public List<RigConfig> Rigs { get; set; }
    }

    public class RigConfig
    {
        public string MachineName { get; set; }
        public Dictionary<string, List<MinerConfig>> MinerConfigs { get; set; }
    }

    public class MinerConfig : IEquatable<MinerConfig>
    {
        public string ProgramFolder { get; set; }
        public string Parameters { get; set; }
        public List<TimeInterval> Intervals { get; set; }

        public MinerConfig(string parameters = "", string programFolder = "", List<TimeInterval> intervals = null)
        {
            ProgramFolder = programFolder;
            Parameters = parameters;
            Intervals = intervals ?? new List<TimeInterval> {new TimeInterval()};
        }

        public TimeSpan? GetCurrentMinimumInterval()
        {
            var activeIntervals = Intervals.Where(i => i.GetIsActiveNow()).ToList();
            if (!activeIntervals.Any())
                return null;
            return activeIntervals.Min(x => x.GetInterval());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MinerConfig))
                return false;
            return Equals((MinerConfig) obj);

        }

        public bool Equals(MinerConfig m)
        {
            var res = m.ProgramFolder.Equals(ProgramFolder) &&
                      m.Parameters.Equals(Parameters) &&
                      m.Intervals.Count == Intervals.Count;
            var intervalsRes = Intervals
                .Select((x, i) => x.Start == m.Intervals[i].Start || x.End == m.Intervals[i].End).All(x => x);
            return res && intervalsRes;
        }

        public static bool operator ==(MinerConfig obj1, MinerConfig obj2)
        {
            if (ReferenceEquals(obj1, null))
                return ReferenceEquals(obj2, null);
            if (ReferenceEquals(obj2, null))
                return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(MinerConfig obj1, MinerConfig obj2)
        {
            if (ReferenceEquals(obj1, null))
                return !ReferenceEquals(obj2, null);
            if (ReferenceEquals(obj2, null))
                return true;

            return !obj1.Equals(obj2);
        }
    }

    public class TimeInterval
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public TimeInterval()
        {
            Start = TimeSpan.Zero;
            End = TimeSpan.FromDays(1).Subtract(TimeSpan.FromMilliseconds(1));
        }

        public TimeInterval(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }

        public TimeSpan GetInterval() => End.Subtract(Start);

        public bool GetIsActiveNow() => DateTime.Now.TimeOfDay > Start && DateTime.Now.TimeOfDay < End;
    }
}
