﻿using System;
using System.Collections.Generic;
using AspNet5Watcher.SearchEngine;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc;

namespace AspNet5Watcher.Controllers
{
    [Route("api/[controller]")]
    public class AlarmsController : Controller
    {
        private SearchRepository _searchRepository;

        public AlarmsController(SearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<AlarmMessage> Get()
        {
            return _searchRepository.SearchForLastTenCriticalAlarms();
        }

        [HttpPost]
        [Route("AddAlarm")]
        public IActionResult Post([FromBody]AlarmMessage alarm)
        {
            if(alarm == null)
            {
                return new HttpStatusCodeResult(400);
            }

            alarm.Id = Guid.NewGuid();
            alarm.Created = DateTime.UtcNow;
            _searchRepository.AddDocument(alarm);
            return new HttpStatusCodeResult(200);

        }
    }
}
