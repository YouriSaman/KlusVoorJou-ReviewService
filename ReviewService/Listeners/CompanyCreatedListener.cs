using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReviewService.DAL;
using ReviewService.Logic;

namespace ReviewService.Listeners
{
    public class CompanyCreatedListener : IDisposable
    {
        private static string _queueName = "company-review-queue";
        private readonly ReviewLogic _reviewLogic;

        private IConnection _connection;
        private IModel _channel;

        public CompanyCreatedListener(IServiceScopeFactory scopeFactory)
        {
            var scope = scopeFactory.CreateScope();
            _reviewLogic = new ReviewLogic(scope.ServiceProvider.GetRequiredService<ReviewDbContext>());
            InitRabbitMQ();
        }


        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://user:BHAQDuK!%3@localhost:5672")
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_queueName, true, false, false, null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            Connect();
        }

        protected bool Connect()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var newCompanyMessage = _reviewLogic.ConvertMessage(message);
                await _reviewLogic.AddCompanyReviewsOfMessage(newCompanyMessage);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, true, consumer);
            return true;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
