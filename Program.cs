using System;
using PersonalExpenseTracker;
namespace ExpenseTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Personal Expense Tracker!");
            ExpenseManager tracker = new ExpenseManager();
            while (true)
            {
                Console.WriteLine("\n Menu:");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. Add a Transaction");
                Console.WriteLine("2. View All Expenses");
                Console.WriteLine("3. View All Incomes");
                Console.WriteLine("4. Set Budget for a Category");
                Console.WriteLine("5. Get the Monthly Report");
                Console.WriteLine("6. Exit");
                Console.WriteLine("==============================================");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        tracker.AddTransaction();
                        break;
                    case "2":
                        tracker.ViewExpenses();
                        break;
                    case "3":
                        tracker.ViewIncomes();
                        break;
                    case "4":
                        tracker.SetBudget();
                        break;
                    case "5":
                        tracker.viewReport();
                        break;
                    case "6":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
