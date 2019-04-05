using AppointmentScheduling.Data.Events;
using NUnit.Framework;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace AppointmentScheduling.IntegrationTests.Events
{
    [TestFixture]
    public class ServiceBrokerTester
    {
        private readonly string ConnectionString = "Data Source=localhost;Initial Catalog=ServiceBrokerTest;Integrated Security=True;MultipleActiveResultSets=True";
        private readonly string MessageType = "SBMessage";
        private readonly string Contract = "SBContract";
        private readonly string SchedulerQueue = "SchedulerQueue";
        private readonly string NotifierQueue = "NotifierQueue";
        private readonly string SchedulerService = "SchedulerService";
        private readonly string NotifierService = "NotifierService";

        private class TestMessage
        {
            public Guid ID { get; private set; }
            public string Message { get; private set; }
            public DateTime TimeSent { get; set; }

            public TestMessage(string messageContents)
            {
                ID = Guid.NewGuid();
                Message = messageContents;
            }
        }

        [Test]
        public void SendMessageFromSchedulerToNotifier()
        {
            var message = new TestMessage("Test: From Scheduler to Notifier.");

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    // Always begin and end a conversation 
                    var conversationHandle = ServiceBrokerWrapper.BeginConversation(sqlTransaction, SchedulerService, NotifierService, Contract);

                    // Set the time from the source machine when the message was sent
                    message.TimeSent = DateTime.UtcNow;

                    // Serialize the transport message
                    string json = JsonConvert.SerializeObject(message, Formatting.None);

                    ServiceBrokerWrapper.Send(sqlTransaction, conversationHandle, MessageType, ServiceBrokerWrapper.GetBytes(json));

                    ServiceBrokerWrapper.EndConversation(sqlTransaction, conversationHandle);

                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }

        [Test]
        public void SendMessageFromNotifierToScheduler()
        {
            var message = new TestMessage("Test: From Notifier to Scheduler.");

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    // Always begin and end a conversation 
                    var conversationHandle = ServiceBrokerWrapper.BeginConversation(sqlTransaction, NotifierService, SchedulerService, Contract);

                    // Set the time from the source machine when the message was sent
                    message.TimeSent = DateTime.UtcNow;

                    // Serialize the transport message
                    string json = JsonConvert.SerializeObject(message, Formatting.None);

                    ServiceBrokerWrapper.Send(sqlTransaction, conversationHandle, MessageType, ServiceBrokerWrapper.GetBytes(json));

                    ServiceBrokerWrapper.EndConversation(sqlTransaction, conversationHandle);

                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }

        [Test]
        public void CountMessagesInNotifierQueue()
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    int messageCount = ServiceBrokerWrapper.QueryMessageCount(sqlTransaction, NotifierQueue, MessageType);

                    Console.WriteLine("Message Count: " + messageCount);
                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }

        [Test]
        public void CountMessagesInSchedulerQueue()
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    int messageCount = ServiceBrokerWrapper.QueryMessageCount(sqlTransaction, SchedulerQueue, MessageType);

                    Console.WriteLine("Message Count: " + messageCount);
                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }

        [Test]
        public void ReceiveMessageOnNotifierQueue()
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    var rawMessage = ServiceBrokerWrapper.WaitAndReceive(sqlTransaction, NotifierQueue, 10 * 1000);

                    if (rawMessage != null && rawMessage.Body.Length > 0)
                    {
                        Console.WriteLine("Raw Message: " + ServiceBrokerWrapper.GetString(rawMessage.Body));

                        TestMessage message = JsonConvert.DeserializeObject<TestMessage>(ServiceBrokerWrapper.GetString(rawMessage.Body));

                        Console.WriteLine("Message: " + message.Message);
                    }
                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }

        [Test]
        public void ReceiveMessageOnSchedulerQueue()
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    var rawMessage = ServiceBrokerWrapper.WaitAndReceive(sqlTransaction, SchedulerQueue, 10 * 1000);

                    if (rawMessage != null && rawMessage.Body.Length > 0)
                    {
                        Console.WriteLine("Raw Message: " + ServiceBrokerWrapper.GetString(rawMessage.Body));

                        TestMessage message = JsonConvert.DeserializeObject<TestMessage>(ServiceBrokerWrapper.GetString(rawMessage.Body));

                        Console.WriteLine("Message: " + message.Message);
                    }
                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }
    }
}
