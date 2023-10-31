using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using QuickFix;

namespace FIX_BMF
{
    public class DataRecorder
    {
        private static volatile object syncRoot = new object();
        private static DataRecorder _Instance;
        public static DataRecorder Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new DataRecorder();
                        }
                    }
                }

                return _Instance;
            }
        }
        private DataRecorder() { }

        private Queue<Message> ReceivedMessages = new Queue<Message>();
        private volatile object queueLock = new object();
        
        private volatile object loopLock = new object();
        private bool _StopProcessing = false;
        public bool StopProcessing
        {
            get
            {
                lock (loopLock)
                {
                    return _StopProcessing;
                }
            }
            set
            {
                lock (loopLock)
                {
                    _StopProcessing = value;
                }
            }
        }
                
        private Thread recorderThread;

        private StreamWriter fs;        

        public void StartRecorder(string LogFilePath)
        {
            fs = new StreamWriter(LogFilePath, true);

            StopProcessing = false;

            recorderThread = new Thread(new ThreadStart(ProcessQueue));
            recorderThread.Start();

        }

        public void EnqueueMessage(QuickFix.Message msgToEnqueue)
        {
            lock (queueLock)
            {
                ReceivedMessages.Enqueue(msgToEnqueue);
            }
        }

        private void ProcessQueue() 
        {
            int remainingMessages = 0;

            while (!(StopProcessing && remainingMessages == 0))
            {
                QuickFix.Message dequeuedMsg = new Message();
                bool nothingToProcess = true;

                lock (queueLock)
                {                    
                    if (ReceivedMessages.Count != 0)
                    {
                        dequeuedMsg = ReceivedMessages.Dequeue();
                        nothingToProcess = false;
                        remainingMessages = ReceivedMessages.Count;
                    }               
                }

                if (nothingToProcess)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    RecordData(dequeuedMsg);
                }
            }                
        }

        private void RecordData(QuickFix.Message msgToRecord)
        {
            string msg = msgToRecord.ToString() + (char)17;
            fs.Write(msg);
        }
    }
}
