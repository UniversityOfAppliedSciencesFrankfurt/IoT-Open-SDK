using IotHub.OpenSdk.Core;
// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace ServiceBus.OpenSdk
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization;
  
    public class Message
    {
        // readonly MessagingClient client;
        private object Body { get; set; }
        private Stream BodyStream { get; set; }
        public object amqpMessage { get; set; }
        public string ContentType { get; set; }
        public string CorrelationId { get; set; }
        public int DeliveryCount { get; set; }
        public long EnqueuedSequenceNumber { get; set; }
        public DateTime EnqueuedTimeUtc { get; set; }
        public DateTime ExpiresAtUtc { get; internal set; }
        public bool ForcePersistence { get; set; }
        public bool IsBodyConsumed { get; set; }
        public string Label { get; set; }
        public DateTime LockedUntilUtc { get; internal set; }
        public Guid LockToken { get; internal set; }
        public string MessageId { get; set; }
        public string PartitionKey { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public Dictionary<object,object> BrokerProperties { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToSessionId { get; set; }
        public DateTime ScheduledEnqueueTimeUtc { get; set; }
        public long SequenceNumber { get; set; }
        public string SessionId { get; set; }
        public long Size { get; internal set; }
        public MessageState State { get; internal set; }
        public TimeSpan TimeToLive { get; set; }
        public string To { get; set; }
        public string ViaPartitionKey { get; set; }

        public Message()
        {
            initializeMessage();
        }

        public Message(object objInstance, XmlObjectSerializer serializer = null)
        {
            initializeMessage();
            this.Body = objInstance;
            
            if (serializer == null)
                serializer = new DataContractSerializer(objInstance.GetType());

            this.BodyStream = new MemoryStream();
            serializer.WriteObject(this.BodyStream, objInstance);
            this.BodyStream.Position = 0;
        }

        public Message(Stream BodyStream)
        {
            initializeMessage();
            this.BodyStream = BodyStream;
            if (this.Body == null)
                this.Body = this.GetBody<String>(null);
        }

        private void initializeMessage()
        {
            Body = null;
            BodyStream = null;
            ContentType = null;
            CorrelationId = null;
            DeliveryCount = 0;
            EnqueuedSequenceNumber = 0;
            EnqueuedTimeUtc = new DateTime();
            ExpiresAtUtc = new DateTime();
            ForcePersistence = false;
            IsBodyConsumed = false;
            Label = null;
            LockedUntilUtc = DateTime.UtcNow.AddSeconds(120);
            //LockToken = new Guid();
            MessageId = Guid.NewGuid().ToString();
            PartitionKey = null;
            Properties = new Dictionary<string, object>();
            ReplyTo = null;
            ReplyToSessionId = null;
            ScheduledEnqueueTimeUtc = new DateTime();
            SequenceNumber = 0;
            SessionId = null;
            Size = 0;
            State = MessageState.Active;
            TimeToLive = new TimeSpan();
            To = null;
            ViaPartitionKey = null;

        }

        public Stream GetBodyStream()
        {
            return this.BodyStream;
        }

        public T GetBody<T>(XmlObjectSerializer serializer = null)
        {
            if (this.Body != null)
            {
                return (T)this.Body;
            }
            else
            {
                if (this.BodyStream == null)
                    return default(T);
                else
                {
                    this.BodyStream.Position = 0;
                    if (serializer == null)
                        serializer = new DataContractSerializer(typeof(T));

                    T t = (T)serializer.ReadObject(this.BodyStream);

                    return t;
                }
            }

        }
    }
}