using System;

namespace AspNet5Watcher.SearchEngine
{
    public class AlarmMessage
    {
        public Guid? Id { get; set;}

        public string Message { get; set; }

        public string AlarmType { get; set; }

        public DateTime Created { get; set; }
    }
}