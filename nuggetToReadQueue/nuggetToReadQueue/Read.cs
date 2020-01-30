using System;
using System.Activities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace nuggetToReadQueue
{
    public class Read : CodeActivity
    {

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter the queue name from which you want to Fetch the Json")]

        public InArgument<string> QueueName { get; set; }

        [Category("Output")]
        public OutArgument<string> OutResult { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            OutResult.Set(context, Reada(QueueName.Get(context)));
        }

        public static string Reada(string queue)
        {
            string resulta = string.Empty;
            try
            {
                using(IConnection connection = new ConnectionFactory().CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue, false, false, false, null);
                        var consumer = new EventingBasicConsumer(channel);
                        BasicGetResult result = channel.BasicGet(queue, true);
                        if(result != null)
                        {
                            resulta = Encoding.UTF8.GetString(result.Body);
                            Console.WriteLine(resulta);
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch(Exception EX)
            {
                resulta = EX.Message.ToString();
            }
            return resulta;
        }


    }
}
