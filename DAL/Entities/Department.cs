using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Department
    {
        public Department()
        {
            CreationDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Department Name is Required")]
        [MinLength(2,ErrorMessage ="MinLength is 2 character")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Department code is Required")]
        public string Code { get; set; }

        public DateTime CreationDate { get; set; }
        public ICollection<Employee> Employees { get; set; }=new List<Employee>();
    }
}
