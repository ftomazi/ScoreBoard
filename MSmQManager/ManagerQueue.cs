using System;
using System.Messaging;

namespace MsmqManager
{
    public class ManagerQueue : BaseManager
    {
        protected override string BasePath 
        {
            get { return @".\private$\newFila"; }
        }

        public void AddItem(MessageModel msg)
        {
            using (MessageQueueTransaction = new MessageQueueTransaction())
            {
                try
                {
                    MessageQueueTransaction.Begin();
                    AddNexItem(msg);
                    MessageQueueTransaction.Commit();
                }
                catch (Exception)
                {
                    MessageQueueTransaction.Abort();
                }
            }
        }

        public override MessageModel Process()
        {
            MessageModel message = null;

            using (MessageQueueTransaction = new MessageQueueTransaction())
            {
                try
                {
                    MessageQueueTransaction.Begin();
                    message = RemoveNextElement<MessageModel>();                     
                    MessageQueueTransaction.Commit();
                }
                catch (Exception)
                {
                    MessageQueueTransaction.Abort();
                }
            }
            return message;
        }
    }

}

