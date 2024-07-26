using App.DAO;
using App.Entities;
using App.Service;
using Microsoft.EntityFrameworkCore;

namespace App;

public class Program
{
    public static void Main(string[] args)
    {

        int d1 = Operation.GetNumOfDays("07/30/0082", "07/23/0082");
        int d2 = Operation.GetNumOfDays("07/23/0082", "07/30/0082");

        Console.WriteLine(d1);
        Console.WriteLine(d2);

        /*
        using (var context = new AppDbContext())
        {
            WorkdayDAO workdayDao = new WorkdayDAO(context);

            workdayDao.Create(new Workday{Date="07/22/0082", DayOfWeek=2, Banked=154421, CurrentWeekBanked=300, EndOfWeekChange=0, Interest=0, Notes=""});
        } */
    }
}