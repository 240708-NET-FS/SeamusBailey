using App.Entities;

namespace App.Service;

public class Operation
{
    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // This function will take two dates, and return the number of days between them
    // Expects dates as a string in MM/DD/YYYY format
    public static int GetNumOfDays(string startingDate, string laterDate)
    {
        string[] start = startingDate.Split("/");
        string[] later = laterDate.Split("/");

        int startMonth = Int32.Parse(start[0]);
        int startDay = Int32.Parse(start[1]);
        int startYear = Int32.Parse(start[2]);

        int endMonth = Int32.Parse(later[0]);
        int endDay = Int32.Parse(later[1]);
        int endYear = Int32.Parse(later[2]);

        int startSumOfDays = (startYear*360) + (startMonth*30) + startDay;
        int endSumOfDays = (endYear*360) + (endMonth*30) + endDay;

        return endDay - startDay;
    }

    // This function will generate a random roll between 1 and 20, that can be passed to the GenerateRandomEndOfWeekChange
    public int GenerateRandomRoll()
    {
        Random rnd = new Random();

        int roll = rnd.Next(1,21);
        return roll;
    }

    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // This method alters the amount of money that is stored for the week
    public Workday GenerateRandomEndOfWeekChange(Workday day, int seed)
    {
        // I don't generate random in here because it's easier to test when I can control the seed
        if (seed == 10)
        {
            return day;
        } 
        else if (seed == 20)
        {
            day.EndOfWeekChange = 0;
            day.Notes += "GOOD END OF WEEK EDIT; ";
            return day;
        } 
        else if (seed == 1) 
        {
            day.EndOfWeekChange = 0;
            day.Notes += "BAD END OF WEEK EDIT; ";
            return day;
        } 
        else if ((seed < 19) && (seed > 10))
        {
            int change = (int)(day.CurrentWeekBanked * (1+((seed-10)*0.1)));
            day.EndOfWeekChange += change;
            return day;
        } 
        else if ((seed > 1) && (seed < 10))
        {
            int change = (int)(day.CurrentWeekBanked * ((10-seed)*0.1));
            day.EndOfWeekChange -= change;
            return day;
        }

        day.Notes += "Error in EndOfWeekChange; ";
        return day;

    }

    // This is simply moving the weekly banked amount to the total banked amount.
    public Workday Deposit(Workday day)
    {
        day.Banked += day.CurrentWeekBanked;
        day.CurrentWeekBanked = 0;
        return day;
    }

    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // This method will advance through days, storing money in a week banked, before ending the week by depositing in the bank
    public Workday AdvanceDays(Workday day, int numOfDays)
    {
        day.Notes += "Advancing Days has not been implemented yet; ";
        return day;
    }

}