using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodCalendar
{
    internal class Program
    {
        static MoodCalendar calendar = new MoodCalendar();
        static string dataFile = "data.json";
        static void Main(string[] args)
        {
            calendar.LoadFromFile(dataFile);

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            while (true)
            {
                Console.Clear();
                PrintCalendar(year, month);
                Console.WriteLine("\nN - Следващ месец | P - Предишен месец | S - Статистика | E - Изход");
                Console.Write("Избери дата (1-31) или команда: ");
                string input = Console.ReadLine()?.ToUpper();

                if (input == "N")
                {
                    month++;
                    if (month > 12) { month = 1; year++; }
                }
                else if (input == "P")
                {
                    month--;
                    if (month < 1) { month = 12; year--; }
                }
                else if (input == "S")
                {
                    Console.Clear();
                    var stats = new MoodStatistics(calendar.GetAllEntries());
                    stats.PrintSummary();
                    Console.WriteLine("\nНатисни клавиш за връщане...");
                    Console.ReadKey();
                }
                else if (input == "E")
                {
                    calendar.SaveToFile(dataFile);
                    break;
                }
                else if (int.TryParse(input, out int day))
                {
                    if (day >= 1 && day <= DateTime.DaysInMonth(year, month))
                    {
                        DateTime selectedDate = new DateTime(year, month, day);
                        Console.Write($"Настроение за {selectedDate.ToShortDateString()}: ");
                        string mood = Console.ReadLine();
                        Console.Write("Коментар (по избор): ");
                        string comment = Console.ReadLine();

                        calendar.AddOrUpdateEntry(selectedDate, mood, comment);
                        calendar.SaveToFile(dataFile);
                    }
                }

            }



            static void PrintCalendar(int year, int month)
            {
                DateTime firstDay = new DateTime(year, month, 1);
                int daysInMonth = DateTime.DaysInMonth(year, month);
                int indent = ((int)firstDay.DayOfWeek + 6) % 7;

                Console.WriteLine($"🗓️ {firstDay:MMMM yyyy}\n");
                Console.WriteLine("Пн Вт Ср Чт Пт Сб Нд");

                for (int i = 0; i < indent; i++)
                    Console.Write("   ");

                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime current = new DateTime(year, month, day);
                    if (calendar.HasEntry(current))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write($"{day,2} ");

                    Console.ResetColor();
                    if ((day + indent) % 7 == 0)
                        Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}

dotnet run;
