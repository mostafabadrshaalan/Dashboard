using AutoMapper;
using DAL.Entities;
using PL.Models;
using System.Collections.Generic;

namespace PL.Mappers
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
