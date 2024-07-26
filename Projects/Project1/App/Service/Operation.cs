using System.Net.NetworkInformation;
using App.DAO;
using App.Entities;
using App.Migrations;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace App.Service;

// AdvanceDay has the spot where I add 300. 300 is how much the shop makes in total. 
// If I wanted to make it count employees and money per employee, I'd probably need to add employees and the income per employee as class fields 
public class Operation
{
    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // This function will take two dates, and return the number of days between them
    // Expects dates as a string in MM/DD/YYYY format
    public static int GetNumOfDays(string laterDate, string startingDate)
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

        return Math.Abs(endSumOfDays - startSumOfDays);
    }

    // This function will generate a random roll between 1 and 20, that can be passed to the GenerateRandomEndOfWeekChange
    public static int GenerateRandomRoll()
    {
        Random rnd = new Random();

        int roll = rnd.Next(1,21);
        return roll;
    }

    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // WRITE A TEST FOR THIS
    // This method alters the amount of money that is stored for the week
    public static Workday GenerateRandomEndOfWeekChange(Workday day, int seed)
    {
        // I don't generate random in here because it's easier to test when I can control the seed
        if (seed == 10)
        {
            return day;
        } 
        else if (seed == 20)
        {
            day.EndOfWeekChange = 0;
            day.Notes += "FLAG: Good end of week edit; ";
            return day;
        } 
        else if (seed == 1) 
        {
            day.EndOfWeekChange = 0;
            day.Notes += "FLAG: Bad end of week edit; ";
            return day;
        } 
        else if ((seed < 20) && (seed > 10))
        {
            int change = (int)(day.CurrentWeekBanked * ((seed-10)*0.1));
            day.EndOfWeekChange += change;
            return day;
        } 
        else if ((seed > 1) && (seed < 10))
        {
            int change = (int)(day.CurrentWeekBanked * ((10-seed)*0.1));
            day.EndOfWeekChange -= change;
            return day;
        }

        day.Notes += "ERROR: Error in EndOfWeekChange; ";
        return day;

    }

    // This is simply moving the weekly banked amount to the total banked amount.
    public static Workday Deposit(Workday day)
    {
        day.Banked += day.CurrentWeekBanked;
        day.CurrentWeekBanked = 0;
        return day;
    }

    public static Workday AdvanceDate(Workday day){
        string[] split =  day.Date.Split("/");
        string newDate = "";

        int dayOfMonth = Int32.Parse(split[1]);
        int month = Int32.Parse(split[0]);
        int year = Int32.Parse(split[2]);

        if (dayOfMonth  == 30 && month == 12){
            year += 1;
            month = 1;
            dayOfMonth = 1;
            // The following string constructions would need a function to force the year to be 4 digits, but I already wrote it in this format and don't want to change it.
            newDate += (month.ToString() + "/" + dayOfMonth.ToString() + "/00" + year.ToString());
        } else if (dayOfMonth == 30) {
            month += 1;
            dayOfMonth = 1;
            newDate += (month.ToString() + "/" + dayOfMonth.ToString() + "/00" + year.ToString());
        } else {
            dayOfMonth += 1;
            newDate += (month.ToString() + "/" + dayOfMonth.ToString() + "/00" + year.ToString());
        }

        if (dayOfMonth > 30){
            day.Notes += "ERROR: Day over 30; ";
        }

        day.Date = newDate;
        return day;
    }

    public static Workday AdvanceInterest(Workday day){
        int interest = (int)(day.Banked * 0.001);
        day.Interest = interest;
        day.Banked += interest;
        return day;
    }

    public static Workday AdvanceDay(Workday day){
        int dOW = day.DayOfWeek;

        if (dOW == 1){
            if (Int32.Parse(day.Date.Split("/")[1]) == 30){
                day = AdvanceInterest(day);
            } 
            day = AdvanceDate(day);
            day.DayOfWeek += 1;
            return day;
        } 
        else if (dOW == 7){
            if (Int32.Parse(day.Date.Split("/")[1]) == 30){
                day = AdvanceInterest(day);
            } 
            day = AdvanceDate(day);
            day.DayOfWeek = 1;
            return day;
        }
        else if (dOW == 6){
            if (Int32.Parse(day.Date.Split("/")[1]) == 30){
                day = AdvanceInterest(day);
            } 
            day = AdvanceDate(day);
            day.DayOfWeek += 1;
            int roll = GenerateRandomRoll();
            day = GenerateRandomEndOfWeekChange(day, roll);
            day = Deposit(day);
            return day;
        }
        else  if (dOW > 1 && dOW < 6){
            if (Int32.Parse(day.Date.Split("/")[1]) == 30){
                day = AdvanceInterest(day);
            }
            day = AdvanceDate(day);
            day.DayOfWeek += 1;
            // This is where I'd edit to add employees and amount each employee makes
            day.CurrentWeekBanked += 300;
            return day;
        } 
        else {
            if (!day.Notes.Contains("error with day of week")){
                day.Notes += "ERROR: Advancing day had error with day of week; ";
            }
            return day;
        }
    }
}