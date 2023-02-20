using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public FinanceController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet]
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _financeService.GetAllEmployees();
        }

        [HttpGet]
        public int GetEmployeeSalaryById(int id)
        {
            return _financeService.GetEmployeeSalaryById(id);
        }

        [HttpGet]
        public int GetTotalEmployeeSalaryExpensesForMonth()
        {
            return _financeService.GetTotalEmployeeSalaryExpensesForMonth();
        }
    }
}

