using AspNet5Watcher.SearchEngine;
using Microsoft.AspNet.Mvc;

namespace AspNet5Watcher.Controllers
{
    [Route("api/[controller]")]
    public class WatcherEventsController
    {
        private SearchRepository _searchRepository;
        private static long _criticalAlarmsCount = 0;

        public WatcherEventsController(SearchRepository searchRepository)
        {
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
                // TODO use
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
