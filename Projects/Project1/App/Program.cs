using App.Entities;

namespace App;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter a date please. ");
        string date = Console.ReadLine();

        using (var context = new AppDbContext())
        {
            var workday = new Workday {Date = date};

            context.Workdays.Add(workday);
            context.SaveChanges();
        }
    }
}