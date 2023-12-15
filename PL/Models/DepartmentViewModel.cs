using DAL.Entities;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        [MinLength(2, ErrorMessage = "MinLength is 2 character")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Department code is Required")]
        public string Code { get; set; }

        public DateTime CreationDate { get; set; }= DateTime.Now;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
