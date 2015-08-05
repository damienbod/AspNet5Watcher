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

        // {
        //  "trigger" : {
        //    "schedule" : {
        //      "interval" : "10s"
        //    }
        //  },
        //  "input": {
        //    "search": {
        //      "request": {
        //        "body": {
        //          "query": {
        //            "term": {
        //              "alarmType": {
        //                "value": "critical"
        //              }
        //            }
        //          }
        //        }
        //      }
        //    }
        //  },
        //  "condition": {
        //    "always": {}
        //  },
        //  "actions": {
        //    "webAction": {
        //      "webhook": { 
        //          "port": 5000,
        //          "host": "localhost",
        //          "path": "/api/WatcherEvents/CriticalAlarm",
        //          "method": "post",
        //          "headers": {
        //            "Content-Type": "application/json;charset=utf-8"
        //          },
        //          "body": "\"{{ctx.payload.hits.total}}\""       
        //      }
        //    }
        //  }
        //}
        public void StartElasticsearchWatcher()
        {
            var header = new Dictionary<string, string>();
            header.Add("Content-Type", "application/json;charset=utf-8");

            var response = client.PutWatch("critical-alarm-watch", p => p
                .Trigger(t => t
                    .Schedule(s => s
                        .Interval("10s")
                    )
                )
                .Input(i => i
                    .Search(s => s
                        .Request(r => r
                            .Body<AlarmMessage>(b => b
                                .Query(q => q.Term(qt => qt.AlarmType, "critical"))
                            )
                        )
                    )
                )
                .Condition(c => c.Always())
                .Actions(a => a.Add("webAction", 
                    new WebhookAction
                    {
                        Method = HttpMethod.Post,
                        Host = "localhost",
                        Port = 5000,
                        Path = "/api/WatcherEvents/CriticalAlarm",
                        Headers = header,
                        Body = "\"{{ctx.payload.hits.total}}\""
                    }
                ))
           );
        }

        /// <summary>
        /// no longer required
        /// </summary>
        private void startElasticsearchWatcherClient()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            var content = new System.Net.Http.StringContent("{\"trigger\":{\"schedule\":{\"interval\":\"10s\"}},\"input\":{\"search\":{\"request\":{\"body\":{\"query\":{\"term\":{\"alarmType\":{\"value\":\"critical\"}}}}}}},\"condition\":{\"always\":{}},\"actions\":{\"webAction\":{\"webhook\":{\"port\":5000,\"host\":\"localhost\",\"path\":\"/api/WatcherEvents/CriticalAlarm\",\"method\":\"post\",\"headers\":{\"Content-Type\":\"application/json;charset=utf-8\"},\"body\":\"{{ctx.payload.hits.total}}\"}}}}");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = httpClient.PutAsync("http://localhost:9200/_watcher/watch/critical-alarm-watch", content).Result;

        }

        public async void DeleteWatcher()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.DeleteAsync("http://localhost:9200/_watcher/watch/critical-alarm-watch");
        }
    }
}
