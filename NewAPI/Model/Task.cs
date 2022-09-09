using System;
using System.Collections.Generic;

namespace NewAPI.Model
{
    public class Task
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public IEnumerable<UserTask> Users { get; set; }
        public Task()
        {
            Users = new List<UserTask>();
        }

    }
}
