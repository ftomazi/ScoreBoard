using DbRepository;
using MsmqManager;
using System;
using System.Threading;

namespace ListeningQueue
{
    class Program
    {
        static void Main(string[] args)
        {

            Timer t = new Timer(TimerCallback, null, 0, 10000);
            Console.ReadLine();

        }

        private static void TimerCallback(Object o)
        {
            ManagerQueue queue = new ManagerQueue();
            var message = queue.Process();

            try
            {
                Console.WriteLine(string.Format("Player: {0}, Points: {1} ", message.PayerId, message.Win));
                ScoreRepository repository = new ScoreRepository();
                repository.UpdateScorePlayer(message.PayerId, message.Win, message.TimeStamp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Erro: {0}", ex.Message));
                //throw;
            }


            GC.Collect();
        }
    }
}
