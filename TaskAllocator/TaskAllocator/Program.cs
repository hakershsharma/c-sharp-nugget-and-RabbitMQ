using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TaskAllocator
{
    public class Program
    {
        
        string text = File.ReadAllText(@"D:\visual Studio\L1Json.txt");  
         void send()
         {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("Command4", false, false, false, null);
                        var body = Encoding.UTF8.GetBytes(text);

                        channel.BasicPublish("","Command4", null, body);
                        Console.WriteLine("[x] sent {0}", text);

                    }
                }
                   
            }           
         }

     
        
        static void Main(string[] args)
        {
            string queue ="Command2";
            Program pr = new Program();
            pr.send();

          /*  Read r = new Read();
            Read.Reada(queue);
           */
        }
    }
}
