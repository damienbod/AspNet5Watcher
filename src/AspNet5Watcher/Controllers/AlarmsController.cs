using System.Collections.Generic;
using AspNet5Watcher.SearchEngine;
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
        public IEnumerable<string> Get()
        {
            _searchRepository.AddDocument(new AlarmMessage {  Message = "test", AlarmType = "Info" });
            return new string[] { "value1", "value2" };
        }

        
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]AlarmMessage alarm)
        {
            _searchRepository.AddDocument(alarm);
        }
    }
}
