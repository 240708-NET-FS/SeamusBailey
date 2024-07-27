using App.Service;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Update;

namespace App.Entities;

public class Workday
{
    //public int WorkdayID {get;set;}
    public string Date {get;set;}
    public int DayOfWeek {get;set;}
    public int Banked {get;set;}
    public int CurrentWeekBanked {get;set;}
    public int EndOfWeekChange {get;set;}
    public int Interest {get;set;}
    public string Notes {get;set;}

    public Workday(string date, int dayOfWeek, int banked, int currentWeekBanked, int endOfWeekChange, int interest, string notes){
        //WorkdayID = DateToInt(date);
        Date = date;
        DayOfWeek = dayOfWeek;
        Banked = banked;
        CurrentWeekBanked = currentWeekBanked;
        EndOfWeekChange = endOfWeekChange;
        Interest = interest;
        Notes = notes;
    }

    public static int DateToInt(string date){
        string[] split = date.Split("/");

        int month = Int32.Parse(split[0]);
        int day = Int32.Parse(split[1]);
        int year = Int32.Parse(split[2]);

        int sum = (month + day + year);
        return sum;
    }

    public override string ToString()
    {
        return $"Date: {this.Date}, Day of Week: {this.DayOfWeek}, Banked: {this.Banked}, Not yet banked: {this.CurrentWeekBanked}, Random change from week: {this.EndOfWeekChange}, Change from interest this month: {this.Interest}, Notes: {this.Notes}";
    }
}