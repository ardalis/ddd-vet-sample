using AppointmentScheduling.Core.Model.ApplicationEvents;
using AppointmentScheduling.Data.Events;
using FrontDesk.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;

namespace FrontDesk.Web.App_Start
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
            private readonly string SchedulerQueue = "SchedulerQueue";

            public void CheckMessages(object sender, System.Timers.ElapsedEventArgs e)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ServiceBroker"].ConnectionString;

                Debug.Print("FD: Checking scheduler queue for messages");
                try
                {
                    using (var sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        using (var sqlTransaction = sqlConnection.BeginTransaction())
                        {
                            var rawMessage = ServiceBrokerWrapper.WaitAndReceive(sqlTransaction, SchedulerQueue, 10 * 1000);

                            if (rawMessage != null && rawMessage.Body.Length > 0)
                            {
                                Debug.Print("Raw Message: " + ServiceBrokerWrapper.GetString(rawMessage.Body));

                                AppointmentConfirmedEvent appointmentConfirmedEvent = JsonConvert.DeserializeObject<AppointmentConfirmedEvent>(ServiceBrokerWrapper.GetString(rawMessage.Body));

                                DomainEvents.Raise(appointmentConfirmedEvent);
                            }
                            sqlTransaction.Commit();
                        }
                        sqlConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print("FD: Error checking scheduler queue: " + ex.ToString());
                }
                //Debug.Print("Done checking scheduler queue for messages");
            }
        }
    }
}