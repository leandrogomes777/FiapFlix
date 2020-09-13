using Confluent.Kafka;
using System;
using System.Threading;

namespace CSCConsumerService
{
    class Program
    {
        private static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "csc_service",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("csc_new_support");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var cr = consumer.Consume(cts.Token);

                        Console.WriteLine("Got new_support {0}", cr.Message.Value);
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine("Error {0}", ex.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
