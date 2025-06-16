using System;
using System.Collections.Generic;
using System.Linq;

namespace MoodCalendar
{
    public class MoodGraph
    {
        private Dictionary<DateTime, List<DateTime>> adjacencyList = new();

        public void AddMoodConnection(DateTime from, DateTime to)
        {
            if (!adjacencyList.ContainsKey(from))
                adjacencyList[from] = new List<DateTime>();
            if (!adjacencyList.ContainsKey(to))
                adjacencyList[to] = new List<DateTime>();

            adjacencyList[from].Add(to);
            adjacencyList[to].Add(from); // двупосочна връзка
        }

        public List<DateTime> GetConnectedDates(DateTime date)
        {
            return adjacencyList.ContainsKey(date) ? adjacencyList[date] : new List<DateTime>();
        }

        public void BuildGraphFromEntries(List<MoodEntry> entries)
        {
            var sorted = entries.OrderBy(e => e.Date).ToList();
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                var current = sorted[i];
                var next = sorted[i + 1];
                if ((next.Date - current.Date).Days == 1)
                {
                    AddMoodConnection(current.Date, next.Date);
                }
            }
        }

        public List<DateTime> BFS(DateTime start)
        {
            var visited = new HashSet<DateTime>();
            var queue = new Queue<DateTime>();
            var result = new List<DateTime>();

            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in GetConnectedDates(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        public List<DateTime> FindConnectedSameMoodDates(DateTime startDate, MoodCalendar calendar)
        {
            var result = new List<DateTime>();
            var visited = new HashSet<DateTime>();
            var targetMood = calendar.GetMood(startDate);

            if (string.IsNullOrEmpty(targetMood))
                return result;

            var queue = new Queue<DateTime>();
            visited.Add(startDate);
            queue.Enqueue(startDate);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in GetConnectedDates(current))
                {
                    if (!visited.Contains(neighbor) && calendar.GetMood(neighbor) == targetMood)
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        public List<DateTime> FindLongestMoodStreak()
        {
            var longest = new List<DateTime>();
            var visited = new HashSet<DateTime>();

            foreach (var start in adjacencyList.Keys)
            {
                if (visited.Contains(start)) continue;

                var currentStreak = new List<DateTime>();
                DFS(start, visited, currentStreak);

                if (currentStreak.Count > longest.Count)
                    longest = new List<DateTime>(currentStreak);
            }

            return longest;
        }

        private void DFS(DateTime current, HashSet<DateTime> visited, List<DateTime> streak)
        {
            if (visited.Contains(current))
                return;

            visited.Add(current);
            streak.Add(current);

            foreach (var neighbor in GetConnectedDates(current))
            {
                DFS(neighbor, visited, streak);
            }
        }

        public void PrintGraphPretty(MoodCalendar calendar)
        {
            Console.WriteLine(" Граф на настроенията:");
            foreach (var kvp in adjacencyList)
            {
                string mood = calendar.GetMood(kvp.Key);
                string connections = string.Join(", ", kvp.Value.Select(d => $"{d.ToShortDateString()} ({calendar.GetMood(d)})"));
                Console.WriteLine($"{kvp.Key.ToShortDateString()} [{mood}] -> {connections}");
            }
        }
    }
}
