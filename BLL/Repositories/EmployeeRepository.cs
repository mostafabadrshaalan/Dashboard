using BLL.Interfaces;
using DAL.Contexts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class EmployeeRepository:GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly RevisionDbContext _context;

        public EmployeeRepository(RevisionDbContext context):base(context) 
        {
            _context = context;
        }


        public async Task<string> GetDepartmentByEmployee(int? id)
        {
            var employee = await _context.Employees
                             .Where(e => e.Id == id)
                             .Include(e => e.Department)
                             .FirstOrDefaultAsync();

            return employee?.Department?.Name;
        }

        public async Task<IEnumerable<Employee>> Search(string name)
         => await _context.Employees.Where(e => e.Name.Contains(name)).ToListAsync();
    }

}
