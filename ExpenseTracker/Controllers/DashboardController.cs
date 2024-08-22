using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ExpenseTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context) { 
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            DateTime start = DateTime.Today.AddDays(-30);
            DateTime end = DateTime.Today;
            List<Transaction> SelectedTransactions= await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= start && y.Date <= end)
                .ToListAsync();

            int TotalIncome = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");

            int TotalExpense = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            int Balance = TotalIncome-TotalExpense;
            ViewBag.Balance = Balance.ToString("C0");

            if (TotalExpense > TotalIncome)
            {
                TotalExpense = TotalIncome; 
            }

            var chartData = new
            {
                labels = new[] { "Income", "Expense", "Balance" },
                data = new[] { TotalIncome, TotalExpense, Balance }
            };
            ViewBag.ChartData = chartData;

            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Date)
                .Take(10)
                .ToListAsync();


            return View();
        }
    }
}
