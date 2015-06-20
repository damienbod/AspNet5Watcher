using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace AspNet5Watcher.SearchEngine
{
    public class SearchRepository
    {
        private ElasticClient client;

        public SearchRepository()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(
                node,
                defaultIndex: "my-application"
            );

            settings.MapDefaultTypeIndices(d => d
               .Add(typeof(AlarmMessage), "alarms")
            );

            settings.MapDefaultTypeNames(d => d
               .Add(typeof(AlarmMessage), "alarm")
            );

            client = new ElasticClient(settings);

        }
        public void AddDocument(AlarmMessage alarm)
        {
            client.Index<AlarmMessage>(alarm, i => i
                .Index("alarms")
                .Type("alarm")
                .Refresh()
            );
        }

        public List<AlarmMessage> SearchForLastTenCriticalAlarms()
        {
            // TODO the search returns nothing...
            var results = client.Search<AlarmMessage>(i => i.Query(q => q.Term(p => p.AlarmType, "critical")).SortDescending("created"));
            return results.Documents.ToList();
        }
    }
}
