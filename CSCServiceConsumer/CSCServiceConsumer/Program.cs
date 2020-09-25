using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace CSCServiceConsumer
{
    public class Program
    {
        static IConfiguration _config;
        private static void Main(string[] args)
        {
            _config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            var config = new ConsumerConfig
            {
                GroupId = "csc_service",
                BootstrapServers = _config["KafkaServer"],
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
