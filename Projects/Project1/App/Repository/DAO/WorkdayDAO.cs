using App.Entities;

namespace App.DAO;

public class WorkdayDAO : IDAO<Workday>
{
    private readonly AppDbContext _context;

    public WorkdayDAO(AppDbContext context)
    {
        _context=context;
    }

    public void Create(Workday item)
    {
        _context.Workdays.Add(item);
        _context.SaveChanges();
    }

    public void Delete(Workday item)
    {
        _context.Workdays.Remove(item);
        _context.SaveChanges();
    }

    public ICollection<Workday> GetAll()
    {
        List<Workday> workdays = _context.Workdays.ToList();
        
        return workdays;
    }

    public Workday GetById(string ID)
    {
        Workday workday = _context.Workdays.FirstOrDefault(w => w.Date == ID);

        return workday;
    }

    public void Update(Workday newItem)
    {
        Workday originalWorkday = _context.Workdays.FirstOrDefault(w => w.Date == newItem.Date);

        if (originalWorkday != null)
        {
            originalWorkday.DayOfWeek = newItem.DayOfWeek;
            originalWorkday.Banked = newItem.Banked;
            originalWorkday.CurrentWeekBanked = newItem.CurrentWeekBanked;
            originalWorkday.EndOfWeekChange = newItem.EndOfWeekChange;
            originalWorkday.Interest = newItem.Interest;
            originalWorkday.Notes = newItem.Notes;
            _context.Workdays.Update(originalWorkday);
            _context.SaveChanges();
        }

    }
}