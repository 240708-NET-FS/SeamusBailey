using App.DAO;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App;

public class Program
{
    public static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            WorkdayDAO workdayDao = new WorkdayDAO(context);

            workdayDao.Create(new Workday{Date="07/17/0082", DayOfWeek=4, Banked=152321, CurrentWeekBanked=1500, EndOfWeekChange=0, Interest=0, Notes=""});
        }
    }
}