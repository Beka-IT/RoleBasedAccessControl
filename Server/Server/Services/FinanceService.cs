using Server.Data;
using Server.Models;

namespace Server.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly AppDbContext _context;

        public FinanceService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToArray();
        }

        public int GetEmployeeSalaryById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id).Salary;
        }

        public int GetTotalEmployeeSalaryExpensesForMonth()
        {
            return _context.Employees.Select(e => e.Salary).Sum();
        }
    }
}
