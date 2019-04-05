using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AppointmentScheduling.Data.Events
{
    /// <summary>
    /// https://github.com/jdaigle/servicebroker.net
    /// </summary>
    public static class ServiceBrokerWrapper
    {
        public static Guid BeginConversation(IDbTransaction transaction, string initiatorServiceName, string targetServiceName, string messageContractName)
        {
            return BeginConversationInternal(transaction, initiatorServiceName, targetServiceName, messageContractName, null, null);
        }

        public static Guid BeginConversation(IDbTransaction transaction, string initiatorServiceName, string targetServiceName, string messageContractName, int lifetime)
        {
            return BeginConversationInternal(transaction, initiatorServiceName, targetServiceName, messageContractName, lifetime, null);
        }

        public static Guid BeginConversation(IDbTransaction transaction, string initiatorServiceName, string targetServiceName, string messageContractName, bool encryption)
        {
            return BeginConversationInternal(transaction, initiatorServiceName, targetServiceName, messageContractName, null, encryption);
        }

        public static Guid BeginConversation(IDbTransaction transaction, string initiatorServiceName, string targetServiceName, string messageContractName, int lifetime, bool encryption)
        {
            return BeginConversationInternal(transaction, initiatorServiceName, targetServiceName, messageContractName, lifetime, encryption);
        }

        public static void EndConversation(IDbTransaction transaction, Guid conversationHandle)
        {
            EndConversation(transaction, conversationHandle, false);
        }

        public static void EndConversation(IDbTransaction transaction, Guid conversationHandle, bool withCleanup)
        {
            EndConversationInternal(transaction, conversationHandle, false, null, null, withCleanup);
        }

        public static void EndConversation(IDbTransaction transaction, Guid conversationHandle, int errorCode, string errorDescription)
        {
            EndConversationInternal(transaction, conversationHandle, true, errorCode, errorDescription, false);
        }

        public static void Send(IDbTransaction transaction, Guid conversationHandle, string messageType)
        {
            Send(transaction, conversationHandle, messageType, null);
        }

        public static void Send(IDbTransaction transaction, Guid conversationHandle, string messageType, byte[] body)
        {
            SendInternal(transaction, conversationHandle, messageType, body);
        }

        public static Message Receive(IDbTransaction transaction, string queueName)
        {
            return ReceiveInternal(transaction, queueName, null, false, null);
        }

        public static Message Receive(IDbTransaction transaction, string queueName, Guid conversationHandle)
        {
            return ReceiveInternal(transaction, queueName, conversationHandle, false, null);
        }

        public static Message WaitAndReceive(IDbTransaction transaction, string queueName, int waitTimeout)
        {
            return ReceiveInternal(transaction, queueName, null, true, waitTimeout);
        }

        public static Message WaitAndReceive(IDbTransaction transaction, string queueName, Guid conversationHandle, int waitTimeout)
        {
            return ReceiveInternal(transaction, queueName, conversationHandle, true, waitTimeout);
        }

        public static int QueryMessageCount(SqlTransaction transaction, string queueName, string messageType)
        {
            return QueryMessageCountInternal(transaction, queueName, messageType);
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private static Guid BeginConversationInternal(IDbTransaction transaction, string initiatorServiceName, string targetServiceName, string messageContractName, int? lifetime, bool? encryption)
        {
            EnsureSqlTransaction(transaction);
            var cmd = transaction.Connection.CreateCommand() as SqlCommand;
            var query = new StringBuilder();

            query.Append("BEGIN DIALOG @ch FROM SERVICE " + initiatorServiceName + " TO SERVICE @ts ON CONTRACT @cn WITH ENCRYPTION = ");

            if (encryption.HasValue && encryption.Value)
                query.Append("ON ");
            else
                query.Append("OFF ");

            if (lifetime.HasValue && lifetime.Value > 0)
            {
                query.Append(", LIFETIME = ");
                query.Append(lifetime.Value);
                query.Append(' ');
            }

            var param = cmd.Parameters.Add("@ch", SqlDbType.UniqueIdentifier);
            param.Direction = ParameterDirection.Output;
            param = cmd.Parameters.Add("@ts", SqlDbType.NVarChar, 256);
            param.Value = targetServiceName;
            param = cmd.Parameters.Add("@cn", SqlDbType.NVarChar, 128);
            param.Value = messageContractName;

            cmd.CommandText = query.ToString();
            cmd.Transaction = transaction as SqlTransaction;
            var count = cmd.ExecuteNonQuery();

            var handleParam = cmd.Parameters["@ch"] as SqlParameter;
            return (Guid)handleParam.Value;
        }

        private static void EndConversationInternal(IDbTransaction transaction, Guid conversationHandle, bool withError, int? errorCode, string errorDescription, bool withCleanup)
        {
            EnsureSqlTransaction(transaction);
            var cmd = transaction.Connection.CreateCommand() as SqlCommand;

            cmd.CommandText = "END CONVERSATION @ch";
            var param = cmd.Parameters.Add("@ch", SqlDbType.UniqueIdentifier);
            param.Value = conversationHandle;

            if (withError)
            {
                cmd.CommandText += " WITH ERROR = @ec DESCRIPTION = @desc";
                param = cmd.Parameters.Add("@ec", SqlDbType.Int);
                param.Value = errorCode;
                param = cmd.Parameters.Add("@desc", SqlDbType.NVarChar, 255);
                param.Value = errorDescription;
            }
            else if (withCleanup)
            {
                cmd.CommandText += " WITH CLEANUP";
            }

            cmd.Transaction = transaction as SqlTransaction;
            var count = cmd.ExecuteNonQuery();
        }

        private static void SendInternal(IDbTransaction transaction, Guid conversationHandle, string messageType, byte[] body)
        {
            EnsureSqlTransaction(transaction);
            var cmd = transaction.Connection.CreateCommand() as SqlCommand;

            string query = "SEND ON CONVERSATION @ch MESSAGE TYPE @mt ";
            var param = cmd.Parameters.Add("@ch", SqlDbType.UniqueIdentifier);
            param.Value = conversationHandle;
            param = cmd.Parameters.Add("@mt", SqlDbType.NVarChar, 255);
            param.Value = messageType;

            if (body != null && body.Length > 0)
            {
                query += " (@msg)";
                param = cmd.Parameters.Add("@msg", SqlDbType.VarBinary, -1);
                param.Value = body;
            }

            cmd.CommandText = query;
            cmd.Transaction = transaction as SqlTransaction;
            var count = cmd.ExecuteNonQuery();
        }

        private static Message ReceiveInternal(IDbTransaction transaction, string queueName, Guid? conversationHandle, bool wait, int? waitTimeout)
        {
            EnsureSqlTransaction(transaction);
            var cmd = transaction.Connection.CreateCommand() as SqlCommand;

            var query = new StringBuilder();

            if (wait && waitTimeout.HasValue && waitTimeout.Value > 0)
                query.Append("WAITFOR(");
            query.Append("RECEIVE TOP(1) ");

            query.Append("conversation_group_id, conversation_handle, " +
                         "message_sequence_number, service_name, service_contract_name, " +
                         "message_type_name, validation, message_body " +
                         "FROM ");
            query.Append(queueName);

            if (conversationHandle.HasValue && conversationHandle.Value != Guid.Empty)
            {
                query.Append(" WHERE conversation_handle = @ch AND message_type_name IS NOT NULL");
                var param = cmd.Parameters.Add("@ch", SqlDbType.UniqueIdentifier);
                param.Value = conversationHandle.Value;
            }

            if (wait && waitTimeout.HasValue && waitTimeout.Value > 0)
            {
                query.Append("), TIMEOUT @to");
                var param = cmd.Parameters.Add("@to", SqlDbType.Int);
                param.Value = waitTimeout.Value;
                cmd.CommandTimeout = 0;
            }

            cmd.CommandText = query.ToString();
            cmd.Transaction = transaction as SqlTransaction;

            using (var dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    return Message.Load(dataReader);
                }
                dataReader.Close();
            }

            return null;
        }

        private static int QueryMessageCountInternal(SqlTransaction transaction, string queueName, string messageType)
        {
            EnsureSqlTransaction(transaction);
            var cmd = transaction.Connection.CreateCommand() as SqlCommand;

            cmd.CommandText = "SELECT COUNT(*) FROM " + queueName + " WHERE message_type_name = @messageContractName";
            var param = cmd.Parameters.Add("@messageContractName", SqlDbType.NVarChar, 128);
            param.Value = messageType;
            cmd.Transaction = transaction as SqlTransaction;

            return (int)cmd.ExecuteScalar();
        }

        private static void EnsureSqlTransaction(IDbTransaction transaction)
        {
            if (!(transaction is SqlTransaction))
                throw new ArgumentException("Only SqlClient is supported", "transaction");
        }
    }
}
