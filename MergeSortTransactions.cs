using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker
{
    public class MergeSortTransactions
    {
        public List<Transaction> MergeSortDate(List<Transaction> transactions)
        {
            if (transactions.Count <= 1)
                return transactions;

            int mid = transactions.Count / 2;
            List<Transaction> left = new List<Transaction>();
            List<Transaction> right = new List<Transaction>();

            for (int i = 0; i < mid; i++)
                left.Add(transactions[i]);
            for (int i = mid; i < transactions.Count; i++)
                right.Add(transactions[i]);

            left = MergeSortDate(left);
            right = MergeSortDate(right);

            return MergeDate(left, right);
        }

        private List<Transaction> MergeDate(List<Transaction> left, List<Transaction> right)
        {
            List<Transaction> result = new List<Transaction>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i].Date >= right[j].Date)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            while (i < left.Count)
                result.Add(left[i++]);
            while (j < right.Count)
                result.Add(right[j++]);
            return result;
        }

        public List<Transaction> MergeSortAmount(List<Transaction> transactions)
        {
            if (transactions.Count <= 1)
                return transactions;

            int mid = transactions.Count / 2;
            List<Transaction> left = new List<Transaction>();
            List<Transaction> right = new List<Transaction>();

            for (int i = 0; i < mid; i++)
                left.Add(transactions[i]);
            for (int i = mid; i < transactions.Count; i++)
                right.Add(transactions[i]);

            left = MergeSortAmount(left);
            right = MergeSortAmount(right);

            return MergeAmount(left, right);
        }

        private List<Transaction> MergeAmount(List<Transaction> left, List<Transaction> right)
        {
            List<Transaction> result = new List<Transaction>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i].Amount >= right[j].Amount)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            while (i < left.Count)
                result.Add(left[i++]);
            while (j < right.Count)
                result.Add(right[j++]);
            return result;
        }

        public List<Transaction> MergeSortCategory(List<Transaction> transactions)
        {
            if (transactions.Count <= 1)
                return transactions;

            int mid = transactions.Count / 2;
            List<Transaction> left = new List<Transaction>();
            List<Transaction> right = new List<Transaction>();

            for (int i = 0; i < mid; i++)
                left.Add(transactions[i]);
            for (int i = mid; i < transactions.Count; i++)
                right.Add(transactions[i]);

            left = MergeSortCategory(left);
            right = MergeSortCategory(right);

            return MergeCategory(left, right);
        }

        private List<Transaction> MergeCategory(List<Transaction> left, List<Transaction> right)
        {
            List<Transaction> result = new List<Transaction>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (string.Compare(left[i].Category, right[j].Category, StringComparison.OrdinalIgnoreCase) <= 0)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            while (i < left.Count)
                result.Add(left[i++]);
            while (j < right.Count)
                result.Add(right[j++]);
            return result;
        }
    }
}

