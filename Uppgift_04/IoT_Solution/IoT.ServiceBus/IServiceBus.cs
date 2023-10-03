using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.ServiceBus
{
    internal interface IServiceBus
    {        
        Task PublishContent (object content, string queue_topic_name, string connectionString);
    }
}
