//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MoodCalendar
//{
//    internal class MoodQueue
//    {
//            private Queue<MoodEntry> queue = new();

//            public void Enqueue(MoodEntry entry)
//            {
//                queue.Enqueue(entry);
//            }

//            public MoodEntry Dequeue()
//            {
//                return queue.Count > 0 ? queue.Dequeue() : null;
//            }

//            public void PrintQueue()
//            {
//                foreach (var entry in queue)
//                {
//                    Console.WriteLine($"{entry.Date.ToShortDateString()} - {entry.Mood}");
//                }
//            }
//        }
//}
