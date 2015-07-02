using System.Collections.Generic;
using AspNet5Watcher.SearchEngine;

namespace AspNet5Watcher.Hubs
{
    public interface IAlarmHub
    {
        void SendAlarms(List<AlarmMessage> alarmsmessages);

        void SendTotalAlarmsCount(int count);
    }
}