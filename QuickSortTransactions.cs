using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker
{
    public class QuickSortTransactions
    {

        public List<Transaction> QuickSortDate(List<Transaction> transactions, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = PartitionDate(transactions, low, high);
                QuickSortDate(transactions, low, pivotIndex - 1);
                QuickSortDate(transactions, pivotIndex + 1, high);
            }
            return transactions;
        }

        private int PartitionDate(List<Transaction> transactions, int low, int high)
        {
            Transaction pivot = transactions[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (transactions[j].Date >= pivot.Date)
                {
                    i++;
                    Swap(transactions, i, j);
                }
            }
            Swap(transactions, i + 1, high);
            return i + 1;
        }

        public List<Transaction> QuickSortAmount(List<Transaction> transactions, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = PartitionAmount(transactions, low, high);
                QuickSortAmount(transactions, low, pivotIndex - 1);
                QuickSortAmount(transactions, pivotIndex + 1, high);
            }
            return transactions;
        }

        private int PartitionAmount(List<Transaction> transactions, int low, int high)
        {
            Transaction pivot = transactions[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (transactions[j].Amount >= pivot.Amount)
                {
                    i++;
                    Swap(transactions, i, j);
                }
            }
            Swap(transactions, i + 1, high);
            return i + 1;
        }

        public List<Transaction> QuickSortCategory(List<Transaction> transactions, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = PartitionCategory(transactions, low, high);
                QuickSortCategory(transactions, low, pivotIndex - 1);
                QuickSortCategory(transactions, pivotIndex + 1, high);
            }
            return transactions;
        }

        private int PartitionCategory(List<Transaction> transactions, int low, int high)
        {
            Transaction pivot = transactions[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (string.Compare(transactions[j].Category, pivot.Category, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    i++;
                    Swap(transactions, i, j);
                }
            }
            Swap(transactions, i + 1, high);
            return i + 1;
        }

        private void Swap(List<Transaction> transactions, int i, int j)
        {
            Transaction temp = transactions[i];
            transactions[i] = transactions[j];
            transactions[j] = temp;
        }
    }
}
