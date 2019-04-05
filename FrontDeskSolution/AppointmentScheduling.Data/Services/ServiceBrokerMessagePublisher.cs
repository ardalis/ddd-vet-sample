using AppointmentScheduling.Core.Interfaces;
using AppointmentScheduling.Data.Events;
using FrontDesk.SharedKernel.Interfaces;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;

namespace AppointmentScheduling.Data.Services
{
    public class ServiceBrokerMessagePublisher : IMessagePublisher
    {
        private readonly string MessageType = "SBMessage";
        private readonly string Contract = "SBContract";
        private readonly string SchedulerService = "SchedulerService";
        private readonly string NotifierService = "NotifierService";

        public void Publish(IApplicationEvent applicationEvent)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ServiceBroker"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    var conversationHandle = ServiceBrokerWrapper.BeginConversation(sqlTransaction, SchedulerService, NotifierService, Contract);

                    string json = JsonConvert.SerializeObject(applicationEvent, Formatting.None);

                    ServiceBrokerWrapper.Send(sqlTransaction, conversationHandle, MessageType, ServiceBrokerWrapper.GetBytes(json));

                    ServiceBrokerWrapper.EndConversation(sqlTransaction, conversationHandle);

                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }
    }
}
