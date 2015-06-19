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

            client = new ElasticClient(settings);

        }
        public void AddDocument(AlarmMessage alarm)
        {
            client.Index<AlarmMessage>(alarm, i => i
                .Index("alarms")
                .Type("alarm")
                .Id(alarm.Id)
                .Refresh()
            );

        }
    }
}
