using App.Entities;
using App.Service;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace App.Controller;

public class ConsoleUI(Operation operations)
{
    Operation ops = operations;
    public string Menu()
    {
        Console.WriteLine("Hello");
        // I am not certain if I should have a menu'ing while loop here and Main calls it, or if I should have it just in Main, and it calls stuff here.

        // I also need to add Feedback, so when generating stuff, maybe start saying "staring generate" run the method, then write "Generated number". Stuff like that.
        Console.WriteLine("What would you like to do? (enter the number or first word of an option from the below menu.) ");
        Console.WriteLine("1. Advance the tracker by a number of days. ");
        Console.WriteLine("2. Read information from tracker. ");
        Console.WriteLine("3. Update tracker manually. ");
        Console.WriteLine("4. Close Menu");

        string response = Console.ReadLine();
        (bool,string) validation = InputValidation.ValidateInputMenu1(response);
        if (!validation.Item1){
            Console.WriteLine($"Response was invalid: {validation.Item2} ");
        }
        return validation.Item2;

    }

    public static string WhatUpdate(){
        string output;
        Console.WriteLine("Would you like to edit an existing day, create a new one, or delete one? (please enter edit, create, or delete): ");
        string input = Console.ReadLine().ToLower().Trim();
        switch (input){
            case "edit":
                return "edit";
            case "create":
                return "create";
            case "delete":
                return "delete";
            default:
                Console.WriteLine("Error taking input, please try again. ");
                output = WhatUpdate();
                return output;
        }
    }

    public static string[] Update(){
        string[] day = new string[7];
        day[0] = UpdateGetDate();
        day[1] = UpdateGetDayOfWeek();
        day[2] = UpdateGetBanked();
        day[3] = UpdateGetCurrentWeekBanked();
        day[4] = UpdateGetEndOfWeekChange();
        day[5] = UpdateGetInterest();
        day[6] = UpdateGetNotes();
        return day;
    }

    public static string UpdateGetNotes(){
        Console.WriteLine("Enter any notes for this day: ");
        string input = Console.ReadLine().Trim();
        (bool, string) check = InputValidation.ValidateNotes(input);
        if (!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string x = UpdateGetNotes();
            return x;
        }
        return input;
    }

    public static string UpdateGetInterest(){
        Console.WriteLine("If it is the end of the month, enter the interest for that month, otherwise put 0: ");
        string input = Console.ReadLine().Trim();
        (bool, string) check = InputValidation.ValidateInterest(input);
        if (!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string x = UpdateGetInterest();
            return x;
        }
        return input;
    }

    public static string UpdateGetEndOfWeekChange(){
        Console.WriteLine("If it is the end of the week, enter the amount of random change for the week, otherwise put 0: ");
        string input = Console.ReadLine().Trim();
        (bool, string) check = InputValidation.ValidateEndOfWeekChange(input);
        if (!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string x = UpdateGetEndOfWeekChange();
            return x;
        }
        return input;
    }

    public static string UpdateGetCurrentWeekBanked(){
        Console.WriteLine("Enter amount the shop has yet to deposit this week: ");
        string input = Console.ReadLine().Trim();
        (bool, string) check = InputValidation.ValidateCurrentWeekBanked(input);
        if (!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string x = UpdateGetCurrentWeekBanked();
            return x;
        }
        return input;
    }

    public static string UpdateGetBanked(){
        Console.WriteLine("Enter amount in bank account: ");
        string input = Console.ReadLine().Trim();
        (bool, string) check = InputValidation.ValidateBanked(input);
        if (!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string bank = UpdateGetBanked();
            return bank;
        }
        return input;
    }

    public static string UpdateGetDayOfWeek(){
        Console.WriteLine("Input day of the week for update (as a number, 1-7, where Sunday is 1): ");
        string input = Console.ReadLine().Trim();
        (bool, string) check = InputValidation.ValidateDayOfWeek(input);
        if (!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string dayOfWeek = UpdateGetDayOfWeek();
            return dayOfWeek;
        }
        return input;
    }

    public static string UpdateGetDate(){
        Console.WriteLine("Input date to update (should be in yyyy/mm/dd format): ");
        string input = Console.ReadLine();
        (bool, string) check = InputValidation.ValidateDate(input.Trim());
        if(!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string date = UpdateGetDate();
            return date;
        }
        return input;
    }

    public static string Read(){
        Console.WriteLine("Input date to read (should be in yyyy/mm/dd format): ");
        string input = Console.ReadLine();
        (bool, string) check = InputValidation.ValidateCodeDate(input.Trim());
        if(!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string date = Read();
            return date;
        }
        return input;
    }

    public static string[] Create(){
        string[] day = new string[7];
        day[0] = CreateGetDate();
        day[1] = UpdateGetDayOfWeek();
        day[2] = UpdateGetBanked();
        day[3] = UpdateGetCurrentWeekBanked();
        day[4] = UpdateGetEndOfWeekChange();
        day[5] = UpdateGetInterest();
        day[6] = UpdateGetNotes();
        return day;
    }

    public static string CreateGetDate(){
        Console.WriteLine("Input date to create (should be in yyyy/mm/dd format): ");
        string input = Console.ReadLine();
        (bool, string) check = InputValidation.ValidateDate(input.Trim());
        if(!check.Item1){
            Console.WriteLine($"Error: {check.Item2}; try again.");
            string date = CreateGetDate();
            return date;
        }
        return input;
    }

    public static string Delete(){
        Console.WriteLine("What is the date you'd like to delete? (dates should be in yyyy/mm/dd format) ");
        string input = Console.ReadLine();
        (bool, string) validation = InputValidation.ValidateCodeDate(input.Trim());
        if(!validation.Item1){
            Console.WriteLine($"Error: {validation.Item2}, please try again. ");
            string reply = Delete();
            return reply;
        }
        return input;
    }

    public static int ConsoleAdvance(){
        Console.WriteLine("How many days would you like to advance the tracker? ");
        string input = Console.ReadLine().Trim();
        (bool, int) validation = InputValidation.ValidateInt(input);
        if (!validation.Item1){
            Console.WriteLine($"Error: {validation.Item2}, please try again. ");
            int reply = ConsoleAdvance();
            return reply;
        }
        return validation.Item2;
    }

    public static string EndHuh(){
        Console.WriteLine("Would you like to return to the menu or quit? (please enter 'menu' or 'quit') ");
        string input = Console.ReadLine().Trim().ToLower();
        switch (input) {
            case "menu":
                return "menu";
            case "quit":
                return "quit";
            default:
                Console.WriteLine("Error reading reply, please try again. ");
                string reply = EndHuh();
                return reply;
        }
    }

}