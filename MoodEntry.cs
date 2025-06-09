using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class MoodEntry
{
    public DateTime Date { get; set; }
    public string Mood { get; set; }
    public string Comment { get; set; }

    public MoodEntry() { } 
    public MoodEntry(DateTime date, string mood, string comment)
    {
        Date = date;
        Mood = mood;
        Comment = comment;
    }
}
