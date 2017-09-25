using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HashHunters.MinerMonitor.Common.DTO;
using HashHunters.MinerMonitor.Common.Interfaces;

namespace HashHunters.MinerMonitor.Common
{
    public class EventHub : IEventHub
    {
        private readonly BlockingCollection<MonitorEvent> Queue = new BlockingCollection<MonitorEvent>();
        private readonly List<Action<MonitorEvent>> Actions = new List<Action<MonitorEvent>>();
        private static bool IS_STARTED;

        public void Start(CancellationToken cancelToken)
        {
            if (!IS_STARTED)
            {
                Task.Factory.StartNew(() =>
                {
                    IS_STARTED = true;
                    foreach (var e in Queue.GetConsumingEnumerable(cancelToken))
                    {
                        foreach (var action in Actions)
                        {
                            Task.Factory.StartNew(
                                () =>
                                {
                                    try { action(e); }
                                    catch (Exception ex) { Console.WriteLine(ex); }
                                }, 
                                
                                cancelToken, TaskCreationOptions.None, TaskScheduler.Default);
                        }
                    }
                    IS_STARTED = false;
                }, cancelToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }

        public void SendEvent(MonitorEvent e)
        {
            Queue.Add(e);
        }

        public void Subscribe(Action<MonitorEvent> action)
        {
            Actions.Add(action);
        }
    }
}
