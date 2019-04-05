using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using VetClinicPublic.Web.Interfaces;
using VetClinicPublic.Web.Models;

namespace VetClinicPublic.Web.AppStart
{
    public class MessagingConfig
    {
        internal static void StartCheckingMessages()
        {
            var thread = new Thread(new ThreadStart(StartJob));
            thread.IsBackground = true;
            thread.Name = "ThreadFunc";
            thread.Start();
        }

        private static void StartJob()
        {
            var messageBroker = new MessageBroker();
            var timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(messageBroker.CheckMessages);
            timer.Interval = 5000;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        private class MessageBroker
        {
            // Copy Data Source from web.config
            // Keep Initial Catalog as ServiceBrokerTest
            // Make sure this matches MessagingConfig in FrontDeskSolution
            private readonly string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ServiceBrokerTest;";
            private readonly string MessageType = "SBMessage";
            private readonly string Contract = "SBContract";
            private readonly string NotifierQueue = "NotifierQueue";
            private readonly string SchedulerService = "SchedulerService";
            private readonly string NotifierService = "NotifierService";

            public void CheckMessages(object sender, System.Timers.ElapsedEventArgs e)
            {
                Debug.Print("PW: Checking notifier queue for messages");
                try
                {
                    using (var sqlConnection = new SqlConnection(ConnectionString))
                    {
                        sqlConnection.Open();
                        using (var sqlTransaction = sqlConnection.BeginTransaction())
                        {
                            var rawMessage = ServiceBrokerWrapper.WaitAndReceive(sqlTransaction, NotifierQueue, 10 * 1000);

                            if (rawMessage != null && rawMessage.Body.Length > 0)
                            {
                                Debug.Print("Raw Message: " + ServiceBrokerWrapper.GetString(rawMessage.Body));

                                var emailSender = StructureMap.ObjectFactory.GetInstance<ISendConfirmationEmails>();
                                AppointmentScheduledEvent appointmentScheduledEvent = JsonConvert.DeserializeObject<AppointmentScheduledEvent>(ServiceBrokerWrapper.GetString(rawMessage.Body));

                                emailSender.SendConfirmationEmail(appointmentScheduledEvent.AppointmentScheduled);
                            }
                            sqlTransaction.Commit();
                        }
                        sqlConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print("PW: Error checking notifier queue: " + ex.ToString());
                }
                //Debug.Print("Done checking queue for messages");
            }

            public void SendConfirmationMessageToScheduler(AppointmentConfirmedEvent confirmationEvent)
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        var conversationHandle = ServiceBrokerWrapper.BeginConversation(sqlTransaction, NotifierService, SchedulerService, Contract);

                        string json = JsonConvert.SerializeObject(confirmationEvent, Formatting.None);

                        ServiceBrokerWrapper.Send(sqlTransaction, conversationHandle, MessageType, ServiceBrokerWrapper.GetBytes(json));

                        ServiceBrokerWrapper.EndConversation(sqlTransaction, conversationHandle);

                        sqlTransaction.Commit();
                    }
                    sqlConnection.Close();
                }
            }
        }

        public void SendConfirmationMessageToScheduler(AppointmentConfirmedEvent confirmationEvent)
        {
            var messageBroker = new MessageBroker();
            messageBroker.SendConfirmationMessageToScheduler(confirmationEvent);
        }
    }
}