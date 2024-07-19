using App.Entities;

namespace App;

public class Program
{
    public static void Main(string[] args)
    {

        Console.WriteLine("Hello, please enter a date. ");
        var date = Console.ReadLine();

        using (var context = new AppDbContext())
        {
            var workday = new Workday {Date = date};

            context.Workdays.Add(workday);
            context.SaveChanges();
        }
        Console.WriteLine($"{date} has been created.");
    }
}