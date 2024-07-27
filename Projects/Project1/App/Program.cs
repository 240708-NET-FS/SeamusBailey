using System.Net.Quic;
using System.Security.Cryptography.X509Certificates;
using App.Controller;
using App.DAO;
using App.Entities;
using App.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace App;

public class Program
{
    public static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            WorkdayDAO workdayDao = new WorkdayDAO(context);

            Operation ops = new Operation();

            ConsoleUI ui = new ConsoleUI(ops);

            //Workday test = Operation.ReadFromDate("0082/7/28", workdayDao);

            //workdayDao.Delete(test);
            
            //Operation.DeleteManually("0082/7/28", workdayDao); This one does not work

            bool done = false;

            while (!done){
                string command = ui.Menu();
                switch(command){
                    case "quit":
                        done = true;
                        break;
                    case "update":
                        string newCommand = ConsoleUI.WhatUpdate();
                        switch(newCommand){
                            case "edit":
                                string[] updates = ConsoleUI.Update();
                                Operation.UpdateManually(updates, workdayDao);
                                break;
                            case "delete":
                                string toDelete = ConsoleUI.Delete();
                                //Operation.DeleteManually(toDelete, workdayDao);
                                Workday dayToDelete = Operation.ReadFromDate(toDelete, workdayDao);
                                workdayDao.Delete(dayToDelete);
                                break;
                            case "create":
                                string[] newWorkday = ConsoleUI.Create();
                                Operation.CreateManually(newWorkday, workdayDao);
                                break;
                        }
                        break;
                    case "advance":
                        int daysToAdvance = ConsoleUI.ConsoleAdvance();
                        context.Workdays.Order();
                        Operation.AdvanceDays(context.Workdays.OrderBy(wd => wd.Date).LastOrDefault(), daysToAdvance, workdayDao);
                        break;
                    case "read":
                        string date = ConsoleUI.Read();
                        Workday dayToDisplay = Operation.ReadFromDate(date, workdayDao);
                        if(InputValidation.ValidateDayNotNull(dayToDisplay)){
                            string display = dayToDisplay.ToString();
                            Console.WriteLine($"Here is the day: ");
                            Console.WriteLine(display);
                        } else {
                            Console.WriteLine("No such date exists. ");
                        }
                        break;
                }
                string endCommand = ConsoleUI.EndHuh();
                switch(endCommand){
                    case "quit":
                        done = true;
                        break;
                    case "menu":
                    default:
                        break;
                }
            }

        } 
    }

}