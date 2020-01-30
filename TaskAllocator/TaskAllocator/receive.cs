using System;
using System.Activities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TaskAllocator
{
    public class Read 
    {
        string queue;
        public static string Reada(string queue)
        {
            
        string resulta = string.Empty;
            try
            {
                using (IConnection connection = new ConnectionFactory().CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue, false, false, false, null);
                        var consumer = new EventingBasicConsumer(channel);
                        BasicGetResult result = channel.BasicGet(queue, true);
                        if (result != null)
                        {
                             resulta = Encoding.UTF8.GetString(result.Body);
                            // Console.WriteLine(result.Body);
                            Console.WriteLine(resulta);                            
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                resulta = EX.Message.ToString();
            }
            
            return resulta;
        }


    }
}
