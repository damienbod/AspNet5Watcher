using AspNet5Watcher.Hubs;
using AspNet5Watcher.SearchEngine;
using Microsoft.AspNet.Mvc;using Microsoft.AspNet.SignalR;

namespace AspNet5Watcher.Controllers
{
    [Route("api/[controller]")]
    public class WatcherEventsController
    {
        private SearchRepository _searchRepository;
        private static long _criticalAlarmsCount = 0;

        private AlarmsHub _alarmsHub;

        public WatcherEventsController(SearchRepository searchRepository, AlarmsHub alarmsHub)
        {
            _alarmsHub = alarmsHub;
            _searchRepository = searchRepository;
        }

        //POST http://localhost:5000/api/WatcherEvents/CriticalAlarm HTTP/1.1
        [HttpPost]
        [Route("CriticalAlarm")]
        public IActionResult Post([FromBody]int countNewCriticalAlarms)
        {  
            if (countNewCriticalAlarms != _criticalAlarmsCount )
            {
                var newCriticalAlarmsCount = countNewCriticalAlarms - _criticalAlarmsCount;
                _criticalAlarmsCount = countNewCriticalAlarms;

                _alarmsHub.SendTotalAlarmsCount(countNewCriticalAlarms);

                // send last x alarms to the ui
            }

            return new HttpStatusCodeResult(200);
        }

        [Route("Start")]
        [HttpPost]
        public void StartElasticsearchWatcher()
        {
            _searchRepository.StartElasticsearchWatcher();
        }

        [Route("Delete")]
        [HttpPost]
        public void DeleteWatcher()
        {
            _searchRepository.DeleteWatcher();
        }
    }
}
