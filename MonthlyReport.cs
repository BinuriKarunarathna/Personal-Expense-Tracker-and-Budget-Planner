using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker
{
    public class MonthlyReport
    {
        public void GenerateMonthlyReport(List<Transaction> incomeList, List<Transaction> expenseList, List<Budget> budgetCategories)
        {
            double totalIncome = incomeList.Sum(t => t.Amount);
            double totalExpenses = expenseList.Sum(t => t.Amount);
            Console.WriteLine("\n Monthly Report: " + DateTime.Now.ToString("MMMM yyyy"));
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Total Income: ${totalIncome}");
            Console.WriteLine($"Total Expenses: ${totalExpenses}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("\nCategory-wise Spending:");
            foreach (var category in expenseList.GroupBy(t => t.Category))
            {
                double categoryTotal = category.Sum(t => t.Amount);
                int barLength = (int)(categoryTotal / totalExpenses * 10); // Scale bar to 10 blocks max
                string bar = new string('█', barLength);
                Console.WriteLine($"{category.Key,-12}: {bar,-10} ${categoryTotal}");
            }
            Console.WriteLine("----------------------------------------");
            // Check for over-budget categories
            foreach (var budget in budgetCategories)
            {
                double totalSpent = expenseList
                .Where(expense => expense.Category == budget.Name)
                .Sum(expense => expense.Amount);
                if (totalSpent > budget.AllocatedAmount)
                {
                    Console.WriteLine($" Warning! You have exceeded the budget for {budget.Name}.");
                }
            }
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(" Tip: Reduce expenses on categories that exceed budget!");
        }
    }
}
