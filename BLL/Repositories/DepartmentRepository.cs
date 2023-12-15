using BLL.Interfaces;
using DAL.Contexts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository
    {
        private readonly RevisionDbContext _context;

        public DepartmentRepository(RevisionDbContext context):base(context)
        {
            _context = context;
        }

        //public int Add(Department department)
        //{
        //    _context.Departments.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _context.Departments.Remove(department);
        //    return _context.SaveChanges();
        //}

        //public Department Get(int? id)
        // => _context.Departments.FirstOrDefault(d => d.Id == id);

        //public IEnumerable<Department> GetAll()
        //=> _context.Departments.ToList();

        //public int Update(Department department)
        //{
        //    _context.Departments.Update(department);
        //    return _context.SaveChanges();
        //}
    }
}
