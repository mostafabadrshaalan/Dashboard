using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        Task<string> GetDepartmentByEmployee(int? id);
        Task<IEnumerable<Employee>> Search(string name);
    }
}
