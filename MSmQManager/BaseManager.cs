using System.Messaging;

namespace MsmqManager
{
    public abstract class BaseManager
    {
        protected abstract string BasePath { get; }
        protected MessageQueueTransaction MessageQueueTransaction { get; set; }
        private MessageQueue MsmQBase()
        {
            var msmq = new MessageQueue(BasePath);

            if (MessageQueue.Exists(BasePath))
            {
                msmq.Formatter = new BinaryMessageFormatter();
                return msmq;
            }

            msmq = MessageQueue.Create(BasePath, true);
            return msmq;
        }
        protected void AddNexItem(object obj)
        {
            MsmQBase().Send(obj, MessageQueueTransaction);
        }
        protected T RemoveNextElement<T>()
        {
            var nexElement = MsmQBase().Receive(MessageQueueTransaction);
            return (T)nexElement.Body;
        }

        public abstract MessageModel Process();
    }

}
