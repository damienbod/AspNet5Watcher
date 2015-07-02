//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using AspNet5Watcher.SearchEngine;
//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Hubs;

//namespace AspNet5Watcher.Hubs
//{
//    public class AlarmsHub  : Hub<IAlarmHub>
//    {
//        public void SendAlarms(List<AlarmMessage> alarmsmessages)
//        {
//            Clients.All.SendAlarms(alarmsmessages);
//        }

//        public void SendTotalAlarmsCount(int count)
//        {
//            Clients.All.SendTotalAlarmsCount(count);
//        }

//        //public override Task OnConnected()
//        //{
//        //    return (base.OnConnected());
//        //}

//        //public override Task OnDisconnected(bool stopCalled)
//        //{
//        //    return (base.OnDisconnected(stopCalled));
//        //}

//        //public override Task OnReconnected()
//        //{
//        //    return (base.OnReconnected());
//        //}
//    }

//}
