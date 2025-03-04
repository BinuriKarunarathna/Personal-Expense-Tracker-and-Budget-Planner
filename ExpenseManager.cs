using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonalExpenseTracker
{
    public class ExpenseManager
    {
        public List <Transaction> expenseList = new List <Transaction>();
        private List<Budget> budgetCategories = new List<Budget>();
        public List <Transaction> incomeList = new List<Transaction>();
        //Add a transaction
        public void AddTransaction()
        {
            Console.Write("Is this income? (yes/no): ");
            bool isIncome = ReadYesNo();
            if (isIncome)
            {
                StoreIncome(); 
                return;
            }
            Console.Write("Enter Description: ");
            string description = ReadNonEmptyInput("Description can not be empty!");
            Console.Write("Enter Amount: ");
            double amount = ReadPositiveDouble("Amount must be a valid positive number!");
            Console.Write("Enter a Category (Food,Travel,Medicine,HouseHold,Clothing): ");
            string category = ReadAlphabeticInput("Category should have only letters!");
            Transaction newTransaction = new Transaction
            {
                Description = description,
                Amount = amount,
                Category = category,
                IsIncome = isIncome,
                Date = DateTime.Now
            };
            expenseList.Add(newTransaction);
            Console.WriteLine(" Transaction added successfully!");
        }

        //Set a budget
        public void SetBudget()
        {
            Console.Write("Enter Category: ");
            string categoryName = ReadNonEmptyInput("Category name cannot be empty!");
            Console.Write("Enter Budget Amount: ");
            double allocatedAmount = ReadPositiveDouble("Budget must be a valid positive number!");
            Budget budget = FindBudget(categoryName);
            if (budget == null)
            {
                Budget newBudget = new Budget
                {
                    Name = categoryName,
                    AllocatedAmount = allocatedAmount,
                };
                budgetCategories.Add(newBudget);
                Console.WriteLine($" Budget set for {categoryName}.");
            }
            else
            {
                budget.AllocatedAmount = allocatedAmount;
                Console.WriteLine($" Budget updated for {categoryName}.");
            }
        }

        //View All Expenses
        public void ViewExpenses()
        {
            Stopwatch stopwatch = new Stopwatch(); // Create a stopwatch instance
            stopwatch.Start(); // Start timing
            if (!expenseList.Any())
            {
                Console.WriteLine(" No expenses available.");
                return;
            }
            List<Transaction> sortedExpenses = expenseList.ToList();
            Console.WriteLine("\nChoose a sorting method:");
            Console.WriteLine("1. Sort by Date (Newest First)");
            Console.WriteLine("2. Sort by Amount (Highest First)");
            Console.WriteLine("3. Sort by Category (Alphabetical)");
            Console.Write("Enter your choice: ");
            string choice = ReadNonEmptyInput("Choice cannot be empty!");
            MergeSortTransactions mergeSort = new MergeSortTransactions();
            QuickSortTransactions quickSort = new QuickSortTransactions();
            if (choice == "1")
                //sortedExpenses = BubbleSortDate(sortedExpenses);
                sortedExpenses = mergeSort.MergeSortDate(sortedExpenses);
                //sortedExpenses = quickSort.QuickSortDate(sortedExpenses, 0, sortedExpenses.Count - 1);
            else if (choice == "2")
                //sortedExpenses = BubbleSortAmount(sortedExpenses);
                sortedExpenses = mergeSort.MergeSortAmount(sortedExpenses);
                //sortedExpenses = quickSort.QuickSortAmount(sortedExpenses, 0, sortedExpenses.Count - 1);
            else if (choice == "3")
                //sortedExpenses = BubbleSortCategory(sortedExpenses);
                sortedExpenses = mergeSort.MergeSortCategory(sortedExpenses);
                //sortedExpenses = quickSort.QuickSortCategory(sortedExpenses, 0, sortedExpenses.Count - 1);
            else
                Console.WriteLine(" Invalid choice. Displaying unsorted transactions.");
            Console.WriteLine("\nTransactions:");
            Console.WriteLine("--------------------------------------------------");
            for (int i = 0; i < sortedExpenses.Count; i++)
            {
                Transaction transaction = sortedExpenses[i];
                Console.WriteLine($"{transaction.Date:yyyy-MM-dd} | {transaction.Category} | {transaction.Description} | ${transaction.Amount}");
            }
            Console.WriteLine("--------------------------------------------------");
            stopwatch.Stop(); // Stop timing
            Console.WriteLine($"\n Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        //View All Incomes
        public void ViewIncomes()
        {
            if (!incomeList.Any())
            {
                Console.WriteLine(" No incomes available.");
                return;
            }
            List<Transaction> sortedIncomes = incomeList.ToList();
            Console.WriteLine("\nChoose a sorting method:");
            Console.WriteLine("1. Sort by Date (Newest First)");
            Console.WriteLine("2. Sort by Amount (Highest First)");
            Console.WriteLine("3. Sort by Category (Alphabetical)");
            Console.Write("Enter your choice: ");
            string choice = ReadNonEmptyInput("Choice cannot be empty!");
            MergeSortTransactions mergeSort = new MergeSortTransactions();
            QuickSortTransactions quickSort = new QuickSortTransactions();
            if (choice == "1")
                sortedIncomes = quickSort.QuickSortDate(sortedIncomes, 0, sortedIncomes.Count - 1);
            else if (choice == "2")
                sortedIncomes = quickSort.QuickSortAmount(sortedIncomes, 0, sortedIncomes.Count - 1);
            else if (choice == "3")
                sortedIncomes = quickSort.QuickSortCategory(sortedIncomes, 0, sortedIncomes.Count - 1);
            else
                Console.WriteLine(" Invalid choice. Displaying unsorted incomes.");
            Console.WriteLine("\nIncome Transactions:");
            Console.WriteLine("--------------------------------------------------");
            for (int i = 0; i < sortedIncomes.Count; i++)
            {
                Transaction transaction = sortedIncomes[i];
                Console.WriteLine($"{transaction.Date:yyyy-MM-dd} | {transaction.Category} | {transaction.Description} | ${transaction.Amount}");
            }
            Console.WriteLine("--------------------------------------------------");
        }

        //Find a budget for a category
        private Budget FindBudget(string categoryName)
        {
            for (int i = 0; i < budgetCategories.Count; i++)
            {
                if (AreStringsEqual(budgetCategories[i].Name, categoryName))
                {
                    return budgetCategories[i];
                }
            }
            return null;
        }

        //View monthly report
        public void viewReport()
        {
            MonthlyReport report = new MonthlyReport();
            if (incomeList.Any() && expenseList.Any())
            {
                Console.WriteLine(" No transactions available.");
                return;
            }
            report.GenerateMonthlyReport(incomeList.ToList(), expenseList.ToList(), budgetCategories);
        }

        //Store an income
        public void StoreIncome()
        {
            Console.Write("Enter Income Description: ");
            string description = ReadNonEmptyInput("Description cannot be empty!");
            Console.Write("Enter Income Amount: ");
            double amount = ReadPositiveDouble("Amount must be a valid positive number!");
            Console.Write("Enter Income Category (e.g., Salary, Investment, Bonus): ");
            string category = ReadAlphabeticInput("Category should have only letters!");
            Transaction incomeTransaction = new Transaction
            {
                Description = description,
                Amount = amount,
                Category = category,
                IsIncome = true,
                Date = DateTime.Now
            };
            incomeList.Add(incomeTransaction);
            Console.WriteLine(" Income transaction added successfully!");
        }

        // Helper Methods for Input Validation Without Built-in Functions
        private string ReadNonEmptyInput(string errorMessage)
            {
                string input;
                while (true)
                {
                    input = Console.ReadLine();
                    if (input != null && HasValidCharacters(input))
                    {
                        return input;
                    }
                    Console.WriteLine($"⚠ {errorMessage}");
                    Console.Write("Please enter again: ");
                }
            }

        // Read a Positive Double Value
        private double ReadPositiveDouble(string errorMessage)
            {
                string input;
                double number;

                while (true)
                {
                    input = Console.ReadLine();
                    if (IsValidDouble(input, out number) && number > 0)
                    {
                        return number;
                    }

                    Console.WriteLine($"⚠ {errorMessage}");
                    Console.Write("Please enter a valid amount: ");
                }
            }

        // Read a Yes/No Answer
        private bool ReadYesNo()
            {
                while (true)
                {
                    string input = Console.ReadLine();
                    if (AreStringsEqual(input, "yes"))
                        return true;
                    if (AreStringsEqual(input, "no"))
                        return false;

                    Console.WriteLine("⚠ Invalid input. Please enter 'yes' or 'no'.");
                    Console.Write("Try again: ");
                }
            }

        // Manual String Comparison (Case-Insensitive)
        private bool AreStringsEqual(string str1, string str2)
            {
                if (str1 == null || str2 == null)
                    return false;

                if (str1.Length != str2.Length)
                    return false;

                for (int i = 0; i < str1.Length; i++)
                {
                    if (ToLowerCase(str1[i]) != ToLowerCase(str2[i]))
                        return false;
                }
                return true;
            }

        // Convert Character to Lowercase Manually
        private char ToLowerCase(char ch)
            {
                if (ch >= 'A' && ch <= 'Z')
                    return (char)(ch + 32); // Convert uppercase to lowercase
                return ch;
            }

        // Validate If String Contains Only Allowed Characters
        private bool HasValidCharacters(string input)
            {
                if (input == null || input.Length == 0)
                    return false;

                for (int i = 0; i < input.Length; i++)
                {
                    char ch = input[i];
                    if (!((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9') || ch == ' '))
                    {
                        return false; // Invalid character found
                    }
                }
                return true;
            }

        // Validate If String Can Be Converted to Double
        private bool IsValidDouble(string input, out double result)
            {
                result = 0;
                if (input == null || input.Length == 0)
                    return false;

                int dotCount = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    char ch = input[i];
                    if (ch == '.')
                    {
                        dotCount++;
                        if (dotCount > 1)
                            return false; // More than one decimal point
                    }
                    else if (ch < '0' || ch > '9')
                    {
                        return false; // Invalid non-numeric character
                    }
                }

                try
                {
                    result = Convert.ToDouble(input);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        // Read Alphabetic Input
        private string ReadAlphabeticInput(string errorMessage)
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (IsOnlyLettersAndSpaces(input))
                {
                    return input;
                }
                Console.WriteLine($"⚠ {errorMessage}");
                Console.Write("Please enter again: ");
            }
        }

        // Validate If String Contains Only Letters and Spaces
        private bool IsOnlyLettersAndSpaces(string input)
        {
            if (input == null || input.Length == 0)
                return false;

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (!((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch == ' '))
                {
                    return false; // Invalid character found
                }
            }
            return true;
        }

        // Bubble Sort Methods
        private List<Transaction> BubbleSortDate(List<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count - 1; i++)
            {
                for (int j = 0; j < transactions.Count - i - 1; j++)
                {
                    if (transactions[j].Date < transactions[j + 1].Date)
                    {
                        var temp = transactions[j];
                        transactions[j] = transactions[j + 1];
                        transactions[j + 1] = temp;
                    }
                }
            }
            return transactions;
        }

        private List<Transaction> BubbleSortAmount(List<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count - 1; i++)
            {
                for (int j = 0; j < transactions.Count - i - 1; j++)
                {
                    if (transactions[j].Amount < transactions[j + 1].Amount)
                    {
                        var temp = transactions[j];
                        transactions[j] = transactions[j + 1];
                        transactions[j + 1] = temp;
                    }
                }
            }
            return transactions;
        }

        private List<Transaction> BubbleSortCategory(List<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count - 1; i++)
            {
                for (int j = 0; j < transactions.Count - i - 1; j++)
                {
                    if (string.Compare(transactions[j].Category, transactions[j + 1].Category, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        var temp = transactions[j];
                        transactions[j] = transactions[j + 1];
                        transactions[j + 1] = temp;
                    }
                }
            }
            return transactions;
        }
     
    }
}

