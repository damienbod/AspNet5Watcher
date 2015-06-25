using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Microsoft.Framework.Configuration;

namespace AspNet5Watcher.SearchEngine
{
    public class SearchRepository
    {
        private ElasticClient client;
        private const string INDEX_ALARMMESSAGE = "alarms";
        private const string TYPE_ALARMMESSAGE = "alarm";

        public SearchRepository(IConfiguration configuration)
        {           
            var node = new Uri(configuration.Get("Development:ElasticsearchConnectionString"));

            var settings = new ConnectionSettings( node,  defaultIndex: "coolsearchengine");
            settings.MapDefaultTypeIndices(d => d.Add(typeof(AlarmMessage), INDEX_ALARMMESSAGE));
            settings.MapDefaultTypeNames(d => d.Add(typeof(AlarmMessage), TYPE_ALARMMESSAGE));

            client = new ElasticClient(settings);
        }

        public void AddDocument(AlarmMessage alarm)
        {
            client.Index(alarm, i => i
                .Index(INDEX_ALARMMESSAGE)
                .Type(TYPE_ALARMMESSAGE)
                .Refresh()
            );
        }

        public List<AlarmMessage> SearchForLastTenCriticalAlarms()
        {
            var results = client.Search<AlarmMessage>(i => i.Query(q => q.Term(p => p.AlarmType, "critical")).SortDescending("created"));
            return results.Documents.ToList();
        }

        public IEnumerable<AlarmMessage> SearchForLastTenAlarms()
        {
            var results = client.Search<AlarmMessage>(i => i.Query(q => q.MatchAll()).SortDescending("created"));
            return results.Documents.ToList();
        }

        public void StartElasticsearchWatcher()
        {
            // Check if the watcher exists
           
            // check and start
        }

        public void DeleteWatcher()
        {

        }
    }
}
