using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoodCalendar
{
    public class MoodCalendar
    {
        private Dictionary<DateTime, MoodEntry> entries = new();

        public void AddOrUpdateEntry(DateTime date, string mood, string comment)
        {
            entries[date] = new MoodEntry(date, mood, comment);
        }

        public bool HasEntry(DateTime date)
        {
            return entries.ContainsKey(date);
        }

        public List<MoodEntry> GetAllEntries()
        {
            return new List<MoodEntry>(entries.Values);
        }

        public void SaveToFile(string path)
        {
            var json = JsonSerializer.Serialize(entries);
            File.WriteAllText(path, json);
        }

        public void LoadFromFile(string path)
        {
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            entries = JsonSerializer.Deserialize<Dictionary<DateTime, MoodEntry>>(json)
                      ?? new Dictionary<DateTime, MoodEntry>();
        }
        public MoodEntry GetEntry(DateTime date)
        {
            return entries.TryGetValue(date, out var entry) ? entry : null;
        }
        public void RemoveEntry(DateTime date)
        {
            if (entries.ContainsKey(date))
                entries.Remove(date);
        }
        public string GetMood(DateTime date)
        {
            return entries.ContainsKey(date) ? entries[date].Mood : string.Empty;
        }

    }
}