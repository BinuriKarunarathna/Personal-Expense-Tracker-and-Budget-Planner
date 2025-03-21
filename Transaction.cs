﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker
{  
    public class Transaction
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } 
        public bool IsIncome { get; set; } 
    }
}
