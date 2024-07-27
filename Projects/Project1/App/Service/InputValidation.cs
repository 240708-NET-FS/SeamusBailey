using App.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace App.Service;

public class InputValidation()
{
    public static (bool, string) ValidateInputMenu1(string str){
        if ((str.Trim().Length < 1) || (str.Trim().Length > 100)){
            return (false, "Response is either empty or too long. ");
        }
        try {
            string resp = str.Trim().ToLower();
            switch(resp){
                case "close":
                case "close menu":
                case "quit":
                case "4":
                    return (true, "quit");
                case "advance":
                case "advance tracker":
                case "advance the tracker by a number of days":
                case "1":
                    return (true, "advance");
                case "update tracker manually":
                case "manually update":
                case "update":
                case "3":
                    return (true, "update");
                case "read":
                case "read information from tracker":
                case "read information":
                case "2":
                    return (true, "read");
                default:
                    return (false, "That wasn't a valid menu option.");
            }
        } catch (Exception e) {
            return (false, e.Message);
        }
    }

    public static (bool, string) ValidateDate(string str)
    {
        try{

            string[] split = str.Trim().Split("/");
            int dayNumber = Int32.Parse(split[2]);
            int monthNumber = Int32.Parse(split[1]);
            int yearNumber = Int32.Parse(split[0]);

            if ((str.Length < 8) || (str.Length > 10)){
                return (false, "Error with Date length.");
            } else if (split.Length != 3) {
                return (false, "Wrong number of slashes.");
            } else if ((dayNumber < 1) || (dayNumber > 30)){
                return (false, "Day is out of bounds.");
            } else if ((monthNumber < 1) || (monthNumber > 12)){
                return (false, "Month out of bounds.");
            } else if ((yearNumber < 0) || (yearNumber > 9999)){
                return (false, "Year is out of bounds");
            } else {
                return (true, "");
            }
        } 
        catch(Exception e){
            return (false, e.Message);
        }
    }

    public static (bool, string) ValidateDayOfWeek(string str)
    {
        try {
            int day = Int32.Parse(str.Trim());

            if ((day < 1) || (day > 7)){
                return (false, "Day of the Week out of bounds.");
            } else {
                return (true, "");
            }
        } catch (Exception e) {
            return  (false, e.Message);
        }
    }

    public static (bool, string) ValidateBanked(string str)
    {
        try{
            int banked = Int32.Parse(str.Trim());
            return (true, "");
        } catch (Exception e){
            return (false, e.Message);
        }
    }

    public static (bool, string) ValidateCurrentWeekBanked(string str)
    {
        try{
            int currWBanked = Int32.Parse(str.Trim());
            return (true, "");
        } catch (Exception e){
            return (false, e.Message);
        }
    }

    public static (bool, string) ValidateEndOfWeekChange(string str)
    {
        try{
            int endChange = Int32.Parse(str.Trim());
            return (true, "");
        } catch (Exception e){
            return (false, e.Message);
        }
    }

    public static (bool, string) ValidateInterest(string str)
    {
        try{
            int interest = Int32.Parse(str.Trim());
            return (true, "");
        } catch (Exception e){
            return (false, e.Message);
        }
    }

    public static (bool, string) ValidateNotes(string str)
    {
        if (str.Trim().Length > 10000){
            return (false, "That note is far too long");
        } else {
            return (true, "");
        }
    }

    public static (bool, string) ValidateNumOfDays(string str){
        try{
            int num = Int32.Parse(str.Trim());
            if (num >= 0){
                return (true, str.Trim());
            } else {
                return (false, "Number is out of bounds. ");
            }
        } catch (Exception e){
            return (false, e.Message);
        }
    }
    
    public static (bool, int) ValidateInt(string str){
        try{
            int result = Int32.Parse(str.Trim());
            return (true, result);
        } catch {
            return (false, 0);
        }
    }

    public static (bool, string) ValidateCodeDate(string str)
    {
        try{

            string[] split = str.Trim().Split("/");
            int dayNumber = Int32.Parse(split[2]);
            int monthNumber = Int32.Parse(split[1]);
            int yearNumber = Int32.Parse(split[0]);

            if ((str.Length > 10)|| (str.Length < 8)){
                return (false, "Error with Date length.");
            } else if (split.Length != 3) {
                return (false, "Wrong number of slashes.");
            } else if ((dayNumber < 1) || (dayNumber > 30)){
                return (false, "Day is out of bounds.");
            } else if ((monthNumber < 1) || (monthNumber > 12)){
                return (false, "Month out of bounds.");
            } else if ((yearNumber < 0) || (yearNumber > 9999)){
                return (false, "Year is out of bounds");
            } else {
                return (true, "");
            }
        } 
        catch(Exception e){
            return (false, e.Message);
        }
    }

    public static bool ValidateDayNotNull(Workday day){
        if (day != null){
            return true;
        }
        return false;
    }

}