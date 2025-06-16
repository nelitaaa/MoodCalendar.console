using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodCalendar
{
    public class MoodStatistics
    {
        private List<MoodEntry> entries;

        public MoodStatistics(List<MoodEntry> allEntries)
        {
            entries = allEntries;
        }

        public void PrintSummary()
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("Няма въведени настроения.");
                return;
            }

            var grouped = entries
                .GroupBy(e => e.Mood)
                .OrderByDescending(g => g.Count());

            Console.WriteLine(" Статистика на настроенията:\n");
            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key}: {group.Count()} дни");
            }
        }
    }
}