using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace AspNet5Watcher.Hubs
{
    [HubName("alarms")]
    public class AlarmsHub  : Hub
    {
        

        //public override Task OnConnected()
        //{
        //    return (base.OnConnected());
        //}

        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    return (base.OnDisconnected(stopCalled));
        //}

        //public override Task OnReconnected()
        //{
        //    return (base.OnReconnected());
        //}
    }

}
