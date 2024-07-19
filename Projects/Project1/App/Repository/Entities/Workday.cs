namespace App.Entities;

public class Workday
{
    public string Date {get;set;}
    public int DayOfWeek {get;set;}
    public int Banked {get;set;}
    public int CurrentWeekBanked {get;set;}
    public int EndOfWeekChange {get;set;}
    public int Interest {get;set;}
    public string Notes {get;set;}

    public override string ToString()
    {
        return $"{Date} {DayOfWeek} {Banked} {CurrentWeekBanked} {EndOfWeekChange} {Interest} {Notes}";
    }
}