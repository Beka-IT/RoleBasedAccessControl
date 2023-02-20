using Server.Models;

namespace Server.Services
{
    public interface IFinanceService
    {
        IEnumerable<Employee> GetAllEmployees();
        int GetEmployeeSalaryById(int id);
        int GetTotalEmployeeSalaryExpensesForMonth();
    }
}
