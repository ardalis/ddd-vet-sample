using System;
using System.Data.SqlClient;
using System.IO;

namespace AppointmentScheduling.Data.Events
{
    public class Message
    {
        public const string EventNotificationType = "http://schemas.microsoft.com/SQL/Notifications/EventNotification";
        public const string QueryNotificationType = "http://schemas.microsoft.com/SQL/Notifications/QueryNotification";
        public const string DialogTimerType = "http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer";
        public const string EndDialogType = "http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog";
        public const string ErrorType = "http://schemas.microsoft.com/SQL/ServiceBroker/Error";

        internal static Message Load(SqlDataReader reader)
        {
            var message = new Message();
            //			RECEIVE conversation_group_id, conversation_handle, 
            //				message_sequence_number, service_name, service_contract_name, 
            //				message_type_name, validation, message_body
            //			FROM Queue
            message.ConversationGroupId = reader.GetGuid(0);
            message.ConversationHandle = reader.GetGuid(1);
            message.MessageSequenceNumber = reader.GetInt64(2);
            message.ServiceName = reader.GetString(3);
            message.ServiceContractName = reader.GetString(4);
            message.MessageTypeName = reader.GetString(5);
            //m_validation = reader.GetString(6);
            if (!reader.IsDBNull(7))
            {
                //message.Body = System.Text.Encoding.UTF8.GetString(reader.GetSqlBytes(7).Buffer);                
                message.Body = reader.GetSqlBytes(7).Buffer;
            }
            else
                message.Body = new byte[0];// String.Empty;
            return message;
        }

        public Guid ConversationGroupId { get; private set; }
        public Guid ConversationHandle { get; private set; }
        public long MessageSequenceNumber { get; private set; }
        public string ServiceName { get; private set; }
        public string ServiceContractName { get; private set; }
        public string MessageTypeName { get; private set; }
        public byte[] Body { get; private set; }
        public Stream BodyStream
        {
            get
            {
                return new MemoryStream(Body);
                //return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(Body));
            }
        }

        private Message()
        {
        }
    }
}