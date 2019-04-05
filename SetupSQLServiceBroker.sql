-- Run this to set up the necessary database and service broker components to run the code in the last module
-- Modified from Pinal Dave's article: http://blog.sqlauthority.com/2009/09/21/sql-server-intorduction-to-service-broker-and-sample-script/
-- You can run the queries at the bottom to check the status of your queues
-- Clear out the test messages before trying to run the demo since they won't be parsed correctly
USE master
CREATE DATABASE ServiceBrokerTest
GO
USE ServiceBrokerTest
GO
-- Enable Service Broker
ALTER DATABASE ServiceBrokerTest SET ENABLE_BROKER
GO
-- Create Message Type
CREATE MESSAGE TYPE SBMessage
VALIDATION = NONE
GO
-- Create Contract
CREATE CONTRACT SBContract
(SBMessage SENT BY INITIATOR)
GO

-- Create Scheduler Queue
CREATE QUEUE SchedulerQueue
GO
-- Create Notifier Queue
CREATE QUEUE NotifierQueue
GO
-- Create Scheduler Service on Scheduler Queue
CREATE SERVICE SchedulerService
ON QUEUE SchedulerQueue (SBContract)
GO
-- Create Notifier Service on Notifier Queue
CREATE SERVICE NotifierService
ON QUEUE NotifierQueue (SBContract)
GO

------------------------------------------------------------------
-- Everything you need is created above
-- The rest of the script below tests that it's working correctly
------------------------------------------------------------------

-- Begin Dialog to send from Scheduler to Notifier
DECLARE @SchedulerToNotifier uniqueidentifier
DECLARE @Message1 NVARCHAR(128)
BEGIN DIALOG CONVERSATION @SchedulerToNotifier
FROM SERVICE SchedulerService
TO SERVICE 'NotifierService'
ON CONTRACT SBContract
WITH ENCRYPTION = OFF

-- Send messages on Dialog1
SET @Message1 = N'First Message From Scheduler to Notifier';
SEND ON CONVERSATION @SchedulerToNotifier
MESSAGE TYPE SBMessage (@Message1)
SET @Message1 = N'Second Message From Scheduler to Notifier';
SEND ON CONVERSATION @SchedulerToNotifier
MESSAGE TYPE SBMessage (@Message1)
SET @Message1 = N'Third Message';
SEND ON CONVERSATION @SchedulerToNotifier
MESSAGE TYPE SBMessage (@Message1)
GO

-- Begin Dialog to send from Notifier to Scheduler
DECLARE @NotifierToScheduler uniqueidentifier
DECLARE @Message2 NVARCHAR(128)
BEGIN DIALOG CONVERSATION @NotifierToScheduler
FROM SERVICE NotifierService
TO SERVICE 'SchedulerService'
ON CONTRACT SBContract
WITH ENCRYPTION = OFF

-- Send messages on Dialog2
SET @Message2 = N'First Message From Notifier to Scheduler';
SEND ON CONVERSATION @NotifierToScheduler
MESSAGE TYPE SBMessage (@Message2)
SET @Message2 = N'Second Message From Notifier to Scheduler';
SEND ON CONVERSATION @NotifierToScheduler
MESSAGE TYPE SBMessage (@Message2)
SET @Message2 = N'Third Message From Notifier to Scheduler';
SEND ON CONVERSATION @NotifierToScheduler
MESSAGE TYPE SBMessage (@Message2)
GO

-- View messages from Notifier Queue (does not remove them)
SELECT CONVERT(NVARCHAR(MAX), message_body) AS Message
FROM NotifierQueue
GO
-- Receive messages from Notifier Queue (pulls one message)
RECEIVE TOP(1) CONVERT(NVARCHAR(MAX), message_body) AS Message
FROM NotifierQueue
GO
-- Receive messages from Notifier Queue (pulls all messages)
RECEIVE CONVERT(NVARCHAR(MAX), message_body) AS Message
FROM NotifierQueue
GO

-- View messages from Scheduler Queue (does not remove them)
SELECT CONVERT(NVARCHAR(MAX), message_body) AS Message
FROM SchedulerQueue
GO
-- Receive messages from Scheduler Queue (pulls one message)
RECEIVE TOP(1) CONVERT(NVARCHAR(MAX), message_body) AS Message
FROM SchedulerQueue
GO
-- Receive messages from Scheduler Queue (pulls all messages)
RECEIVE CONVERT(NVARCHAR(MAX), message_body) AS Message
FROM SchedulerQueue
GO