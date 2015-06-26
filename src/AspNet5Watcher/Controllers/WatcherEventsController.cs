using System;
using System.Collections.Generic;
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
        //User-Agent: Fiddler
        //Host: localhost:5000
        //Content-Length: 160
        //Content-Type: application/json;charset=utf-8
        //Accept: application/json, text/plain, */*

        //{
        //    "Id": "32e12870-9420-43fc-ac65-daf0325c58c3",
        //    "Message": "cool c",
        //    "AlarmType": "critical",
        //    "Created": "2015-06-25T14:49:13.7364123Z"
        //}

        /// <summary>
        /// http://localhost:5000/api/WatcherEvents/CriticalAlarm
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CriticalAlarm")]
        public IActionResult Post([FromBody]int countNewCriticalAlarms)
        {  
            if (countNewCriticalAlarms != _criticalAlarmsCount )
            {
                var newCriticalAlarmsCount = countNewCriticalAlarms - _criticalAlarmsCount;
                _criticalAlarmsCount = countNewCriticalAlarms;
                // do something
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
