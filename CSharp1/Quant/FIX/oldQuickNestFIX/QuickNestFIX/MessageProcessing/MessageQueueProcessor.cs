using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using QuickFix;

namespace QuickNestFIX
{
    public class MessageQueueProcessor: MessageCracker
    {
        private Semaphore QueueSemaphore = new Semaphore(0, int.MaxValue);
        private Queue<SessionMessage> MessageQueue = new Queue<SessionMessage>();
        private Mutex QueueMutex = new Mutex();
        private Thread QueueThread;
        private bool StopProcessing = false;

        public MessageQueueProcessor()
        {
            QueueThread = new Thread(new ThreadStart(ProcessQueue));
            QueueThread.Start();
        }

        public void EnqueueMessage(SessionID session, SessionType sessionType, Message message)
        {
            SessionMessage newMessage = new SessionMessage();
            newMessage.session = session;
            newMessage.message = message;
            newMessage.sessionType = sessionType;

            EnqueueMessage(newMessage);
        }
        public void EnqueueMessage(SessionMessage message)
        {
            QueueMutex.WaitOne();

            MessageQueue.Enqueue(message);
            QueueSemaphore.Release();

            QueueMutex.ReleaseMutex();
        }

        private void ProcessQueue()
        {
            while (!StopProcessing)
            {
                QueueSemaphore.WaitOne();

                bool nothingToProcess = true;
                SessionMessage message = new SessionMessage();

                QueueMutex.WaitOne();

                if (MessageQueue.Count > 0)
                {
                    message = MessageQueue.Dequeue();
                    nothingToProcess = false;
                }
                else
                {
                    nothingToProcess = true;
                }

                QueueMutex.ReleaseMutex();

                if (!nothingToProcess)
                {
                    ProcessMessage(message);
                }
            }
        }

        public virtual void ProcessMessage(SessionMessage message)
        {
        }

        public virtual void Stop()
        {
            StopProcessing = true;

            while (QueueThread.IsAlive)
            {
                if (MessageQueue.Count == 0)
                {
                    QueueSemaphore.Release();
                    QueueThread.Join();
                }

                Thread.Sleep(100);
            }
        }
    }
}
